using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    public Collider2D swordCollider;
    Vector2 rightAttackOffset;
    public float damage = 3f;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        rightAttackOffset = transform.position;
    }
    
    //  Transform collider position to right when attack
    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
        Debug.Log("right swing");
    }
    
    // Transform collider position to left when attack
    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
        Debug.Log("left swing");
    }

    public void StopAttack()
    {
        swordCollider.enabled = false; 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.hp -= damage;
                
            }
        }
        
    }
}
