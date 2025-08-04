using Unity.Collections;
using Unity.Jobs;

public struct BattleDamageJob : IJob
{
    public NativeArray<EnemyModel> enemies;
    public NativeArray<TowerAttackData> towerAttacks;
    public PlayerModel playerModel;

    public void Execute()
    {
        var enemyGoalHandler = new EnemyGoalDamageHandler
        {
            enemies = enemies,
            playerModel = playerModel
        };
        enemyGoalHandler.Execute();

        var towerDamageHandler = new TowerDamageHandler
        {
            enemies = enemies,
            towerAttacks = towerAttacks
        };
        towerDamageHandler.Execute();
    }
}
