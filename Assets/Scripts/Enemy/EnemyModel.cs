using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;
using System;
[Serializable]
public class EnemyModel
{
    [Header("Movement")]
    [SerializeField] private float speed = 2.0f; // Speed of the enemy movement
    [SerializeField] private float minTime = 3.0f;
    [SerializeField] private float maxTime = 6.0f;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100; // Health points of the enemy
    private float currentHealth;
    public float2 position;

    public List<float2> Waypoints;
    private int currentWaypointIndex;

    public bool IsAlive => currentHealth > 0;
    public bool ReachedGoal => currentWaypointIndex >= Waypoints.Count;

    public void Initialize(List<float2> waypoints)
    {
        Waypoints = waypoints;
        position = waypoints[0];
        currentWaypointIndex = 0;
        currentHealth = maxHealth;
    }

    public void Move(float deltaTime)
    {
        if (ReachedGoal || !IsAlive) return;

        float2 target = Waypoints[currentWaypointIndex];
        float2 direction = math.normalize(target - position);
        float distanceToTarget = math.distance(position, target);
        float moveStep = speed * deltaTime;

        if (moveStep >= distanceToTarget)
        {
            position = target;
            currentWaypointIndex++;
        }
        else
        {
            position += direction * moveStep;
        }
    }
}
