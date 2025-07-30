using Unity.Collections;

public struct EnemyGoalDamageHandler
{
    public NativeArray<EnemyModel> enemies;
    public PlayerModel playerModel;

    public void Execute()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i];
            if (enemy.isAlive && enemy.reachedGoal)
            {
                float damage = enemy.damageToPlayer;
                playerModel.hp -= damage;
                enemy.isAlive = false;
                enemies[i] = enemy;
            }
        }
    }
}
