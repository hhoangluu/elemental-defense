using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2.0f; // Speed of the enemy movement
    [SerializeField] private float minTime = 3.0f;
    [SerializeField] private float maxTime = 6.0f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 100; // Health points of the enemy
    private int currentHealth;

    // Components
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // State variables
    private float timer;
    private float timeToChangeDirection;
    private Vector2 moveDirection;


    private void Awake()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        //Todo: init timeToChangeDirection ( random between min and max)
        timeToChangeDirection = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToChangeDirection)
        {
            // Doi huong
            timer = 0f;
            ChangeDirectionRandom();

            // Random lai time giua min va max
            timeToChangeDirection = Random.Range(minTime, maxTime);
        }

        anim.SetBool("IsMoving", moveDirection != Vector2.zero);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    public void ChangeDirectionRandom()
    {
        Vector2 newDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        if (newDirection != Vector2.zero)
        {
            moveDirection = newDirection;
        }

        if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage, current health: {currentHealth}");

        // TODO: hieu ung an dmg.

        if (currentHealth <= 0)
        {
            //Die();
        }
    }
}
