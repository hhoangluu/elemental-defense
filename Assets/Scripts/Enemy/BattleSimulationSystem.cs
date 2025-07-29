using System;
using System.Collections.Generic;
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

        JobHandle handle = moveJob.Schedule(enemyModels.Length, 64);
        handle.Complete();

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
        Debug.LogError("LENGHT" + enemyModels.Length);

        return model.id;
    }

}
