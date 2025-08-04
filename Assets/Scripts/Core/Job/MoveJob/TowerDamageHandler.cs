using Unity.Collections;

public struct TowerAttackData
{
    public int enemyIndex;
    public float damage;
}

public struct TowerDamageHandler
{
    public NativeArray<EnemyModel> enemies;
    public NativeArray<TowerAttackData> towerAttacks;

    public void Execute()
    {
        for (int i = 0; i < towerAttacks.Length; i++)
        {
            var attack = towerAttacks[i];
            if (attack.enemyIndex < 0 || attack.enemyIndex >= enemies.Length)
                continue;

            var enemy = enemies[attack.enemyIndex];
            if (!enemy.isAlive)
                continue;

            enemy.currentHealth -= attack.damage;
            if (enemy.currentHealth <= 0)
                enemy.isAlive = false;

            enemies[attack.enemyIndex] = enemy;
        }
    }
}
