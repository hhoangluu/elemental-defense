using System;
using System.Collections.Generic;
using _ElementalDefense;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class BattleSimulationSystem
{
    private NativeList<EnemyModel> enemyModels;
    Dictionary<int, int> idToIndex;
    Dictionary<int, EnemyController> idToController;
    int nextEnemyId = 0;
    private Map map;
    private NativeArray<float2> waypoints;
    // private List<(EnemyModel, EnemyController)> pendingEnemyModels;

    public BattleSimulationSystem()
    {
        enemyModels = new(Allocator.Persistent);
        idToIndex = new();
        idToController = new();
        // pendingEnemyModels = new();
    }

    public void SetMap(Map map)
    {
        this.map = map;

        var navPoints = this.map.mapNavigation.navPoints;
        var wpCount = navPoints.Count;

        var waypoints = new NativeArray<float2>(wpCount, Allocator.Persistent);

        for (int i = 0; i < wpCount; i++)
        {
            Vector3 pos = navPoints[i].transform.position;
            waypoints[i] = new float2(pos.x, pos.y);
        }
        this.waypoints = waypoints;
    }

    // public void Initialize(EnemyController[] enemyControllers)
    // {
    //     controllers = enemyControllers;
    //     models = new NativeList<EnemyModel>(controllers.Length, Allocator.Persistent);

    //     for (int i = 0; i < controllers.Length; i++)
    //     {
    //         models[i] = controllers[i].ToModel();
    //     }
    // }

    public void Simulate(float deltaTime)
    {
        if (enemyModels.Length == 0)
            return;

        var moveJob = new EnemyMoveJob
        {
            enemies = enemyModels.AsDeferredJobArray(),
            waypoints = waypoints,
            deltaTime = deltaTime
        };

        JobHandle moveHandle = moveJob.Schedule(enemyModels.Length, 64);
        moveHandle.Complete();
        var towerAttackTempData = new NativeArray<TowerAttackData>(0, Allocator.TempJob);
        var damageJob = new BattleDamageJob
        {
            enemies = enemyModels.AsDeferredJobArray(),
            towerAttacks = towerAttackTempData,
            playerModel = GameManager.Instance.playerModel
        };

        var damageHandle = damageJob.Schedule(moveHandle);
        damageHandle.Complete();
        towerAttackTempData.Dispose();

    }

    public void SyncBackToControllers()
    {
        for (int i = 0; i < enemyModels.Length; i++)
        {
            var model = enemyModels[i];
            if (!idToController.TryGetValue(model.id, out var controller))
                continue;
            controller.ApplyFromModel(model);
        }
    }

    public void Dispose()
    {
        if (enemyModels.IsCreated)
            enemyModels.Dispose();
    }

    public int AddEnemyModel(EnemyModel model, EnemyController controller)
    {
        model.id = nextEnemyId++;
        enemyModels.Add(model);
        idToIndex[model.id] = enemyModels.Length - 1;
        idToController[model.id] = controller;
        return model.id;
    }

}
