using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    // Components
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveDirection;
    [SerializeField] private EnemyModel enemyModel;
    bool isInitialized;

    private void Awake()
    {
        enemyModel = new();
        // Get components
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(List<Transform> waypoints)
    {
        List<float2> float2Waypoints = waypoints.Select(w => new float2(x: w.position.x, y: w.position.y)).ToList();
        enemyModel.Initialize(float2Waypoints);
        isInitialized = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitialized)
            return;
        Move();
        anim.SetBool("IsMoving", moveDirection != Vector2.zero);
    }

    // private void FixedUpdate()
    // {
    //     rb.linearVelocity = moveDirection * speed;
    // }

    // public void ChangeDirectionRandom()
    // {
    //     Vector2 newDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    //     if (newDirection != Vector2.zero)
    //     {
    //         moveDirection = newDirection;
    //     }

    //     if (moveDirection.x > 0)
    //     {
    //         spriteRenderer.flipX = false;
    //     }
    //     else if (moveDirection.x < 0)
    //     {
    //         spriteRenderer.flipX = true;
    //     }
    // }

    public void Move()
    {
        enemyModel.Move(Time.deltaTime);
        transform.position = new Vector2(enemyModel.position.x, enemyModel.position.y);
    }

    public void TakeDamage(int damage)
    {
        // currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage, current health: ");

        // // TODO: hieu ung an dmg.

        // if (currentHealth <= 0)
        // {
        //     //Die();
        // }

    }
}
