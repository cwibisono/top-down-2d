using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1;
    public float moveSpeed = 1f;
    public EnemySpawner enemy;
    private Animator _animator;
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer _spriteRenderer;
    private bool isDefeated = false;
    private bool isHit = true;
    public ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        enemy = GameObject.FindObjectOfType<EnemySpawner>();
        _animator = GetComponent<Animator>();
        target = GameObject.Find("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target && !isDefeated)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
            
            if (moveDirection.x < 0)
            {
                _spriteRenderer.flipX = true;
            }

            else if (moveDirection.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            
        }
    }
    //TODO check potential collision
    
    public float hp
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
            
        }
        get
        {
            return health;
            
        }
        
    }

    public void Defeated()
    {
        _animator.SetTrigger("Dead");
        if (health <= 0)
        {
            moveSpeed = 0;
        }
        
    }
    
    
    public void RemoveEnemy()
    {
        enemy.currentSpawned--;
        Destroy(gameObject);
    }
}
 