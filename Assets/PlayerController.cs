using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Sets the player's movement speed (adjustable in the Unity editor).
    public float moveSpeed = 1f;
    
    //A small offset to prevent getting stuck on walls.
    public float collisionOffset = 0.05f;
    
    //Filter for determining which collisions to consider during movement.
    public ContactFilter2D movementFilter;
    
    //Stores the player's movement input from the input system.
    Vector2 movementInput;
    
    //Reference to the player's Rigidbody2D component, used for physics-based movement.
    public Rigidbody2D rb;
    
    //List to store collision information from raycasts.
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    //Animation reference
    private Animator _animator;
    
    private SpriteRenderer _spriteRenderer;
    
    public SwordAttack swordAttack;
    
    bool _canMove = true;
    
    // Start is called before the first frame update 
    void Start() 
    {
        //Gets a reference to the player's Rigidbody2D component.
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        if (_canMove)
        {
            //edges sliding on collision
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
            
                if (!success && movementInput.x > 0)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success && movementInput.y > 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                _animator.SetBool("isMoving", success);
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }
        
            //set direction of sprite according to direction
            if (movementInput.x < 0)
            {
                _spriteRenderer.flipX = true;
                
            } 
            else if (movementInput.x > 0)
            {
                _spriteRenderer.flipX = false;
               
            }
        }
    }



    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            //check potential collisions
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
            //move only if no collisions        
            if (count == 0)
            {
                //move the player
                //calculates the new position based on current position, movement direction, speed, and time step.
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        else
        {
            return false;
        }
    }
    
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        _animator.SetTrigger("swordAttack");
        Debug.Log("Attack triggered");
    }

    public void SwordAttack()
    {
        LockMovement();
        if (_spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else 
        {
            swordAttack.AttackRight(); 
        }
    }

    public void EndAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        _canMove = false;
    }

    public void UnlockMovement()
    {
        _canMove = true;
    }
}

 
/*
    The code handles object movement in a physics-based context, ensuring smooth movement and collision detection.
    It performs movement within the FixedUpdate() function for consistent physics behavior.
    It uses raycasting to detect collisions before moving the object.
    It only moves the object if no collisions are detected in the intended movement direction.
*/