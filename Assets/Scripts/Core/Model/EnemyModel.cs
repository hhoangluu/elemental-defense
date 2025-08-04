// using UnityEngine;
// using Unity.Mathematics;
// using System.Collections.Generic;
// using System;
// [Serializable]
// public struct EnemyModel
// {
//     [Header("Movement")]
//     [SerializeField] private float speed; // Speed of the enemy movement

//     [Header("Health")]
//     [SerializeField] private float maxHealth; // Health points of the enemy
//     private float currentHealth;
//     public float2 position;

//     public List<float2> Waypoints;
//     private int currentWaypointIndex;

//     public bool IsAlive => currentHealth > 0;
//     public bool ReachedGoal => currentWaypointIndex >= Waypoints.Count;

//     public EnemyModel(float speed, float maxHealth, List<float2> waypoints)
//     {
//         this.speed = speed;
//         this.maxHealth = maxHealth;
//         Waypoints = waypoints;
//         position = waypoints[0];
//         currentWaypointIndex = 0;
//         currentHealth = maxHealth;
//     }

//     public void Move(float deltaTime)
//     {
//         if (ReachedGoal || !IsAlive) return;

//         float2 target = Waypoints[currentWaypointIndex];
//         float2 direction = math.normalize(target - position);
//         float distanceToTarget = math.distance(position, target);
//         float moveStep = speed * deltaTime;

//         if (moveStep >= distanceToTarget)
//         {
//             position = target;
//             currentWaypointIndex++;
//         }
//         else
//         {
//             position += direction * moveStep;
//         }
//     }
// }
using System;
using Unity.Mathematics;

[Serializable]
public struct EnemyModel
{
    public int id;
    public float2 position;
    public float speed;
    public float maxHealth;
    public float currentHealth;
    public int currentWaypointIndex;
    public bool isAlive;
    public bool reachedGoal;
    internal float damageToPlayer;

    public bool ReachedGoal(int waypointCount) => currentWaypointIndex >= waypointCount;
}
