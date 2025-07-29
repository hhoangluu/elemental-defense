using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

[BurstCompile]
public struct EnemyMoveJob : IJobParallelFor
{
    public NativeArray<EnemyModel> enemies;
    [ReadOnly]
    public NativeArray<float2> waypoints;
    public float deltaTime;

    public void Execute(int index)
    {
        EnemyModel enemy = enemies[index];

        if (!enemy.isAlive || enemy.ReachedGoal(waypoints.Length))
            return;
        if (enemy.currentWaypointIndex == 0)
        {
            enemy.position = waypoints[enemy.currentWaypointIndex];
            enemy.currentWaypointIndex++;
            enemies[index] = enemy;
            return;
        }

        float2 target = waypoints[enemy.currentWaypointIndex];
        float2 direction = math.normalize(target - enemy.position);
        float distanceToTarget = math.distance(enemy.position, target);
        float moveStep = enemy.speed * deltaTime;

        if (moveStep >= distanceToTarget)
        {
            enemy.position = target;
            enemy.currentWaypointIndex++;
        }
        else
        {
            enemy.position += direction * moveStep;
        }

        enemies[index] = enemy;
    }
}
