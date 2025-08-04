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
    [SerializeField] private float speed = 2;
    [SerializeField] private float heal = 100;
    private Map map;
    bool isInitialized;
    private int enemyId;

    public void SetId(int id)
    {
        this.enemyId = id;
    }

    private void Awake()
    {
        // Get components
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!isInitialized)
            return;
        anim.SetBool("IsMoving", moveDirection != Vector2.zero);
    }

    public void ApplyFromModel(EnemyModel enemyModel)
    {
        transform.position = new Vector2(enemyModel.position.x, enemyModel.position.y);
    }

    public EnemyModel ToModel()
    {
        return new EnemyModel()
        {
            maxHealth = this.heal,
            currentHealth = this.heal,
            speed = this.speed,
            isAlive = true,
        };
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
