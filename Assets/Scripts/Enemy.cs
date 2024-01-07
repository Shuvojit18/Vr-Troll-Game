using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;

    private Transform playerTransform;
    private bool isPlayerInRange = false;

    void Start()
    {
        // player has a tag called "Player"
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
        if (isPlayerInRange)
        {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);
        
        // Check if the player is within the attack range
        if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking player with damage: " + attackDamage);
        // Here add the logic to decrease the player's health
        // For example, playerHealth.TakeDamage(attackDamage);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player's weapon
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
