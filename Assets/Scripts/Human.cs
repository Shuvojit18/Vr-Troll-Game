using UnityEngine;

public class Human : MonoBehaviour
{
    public float speed = 5.0f;
    public float attackRange = 1.5f;
    public int attackDamage = 1;
    int health = 100;

    private Transform trollTransform;
    private bool isPlayerInRange = false;

    Troll troll;

    public float minimumImpactVelocity = 3.0f; // Requires a moderate throw speed to cause damage
    public float damageMultiplier = 1.5f; // Makes the damage somewhat higher than the impact speed


    void Start()
    {
        // player has a tag called "Player"
        troll = FindObjectOfType<Troll>();
        trollTransform = troll.transform;
        
    }

    void Update()
    {
        MoveTowardsPlayer();
        if (isPlayerInRange)
        {
            AttackPlayer();
        }

        if (health == 0){
            Die();
        }
    }

    void MoveTowardsPlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, trollTransform.position, step);
        
        // Check if the player is within the attack range
        if (Vector3.Distance(transform.position, trollTransform.position) < attackRange)
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
        //Debug.Log("Attacking player with damage: " + attackDamage);
        // Here add the logic to decrease the player's health
        // For example, playerHealth.TakeDamage(attackDamage);
        troll.TakeDamage(attackDamage);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player's weapon
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            Die();
        }

    
        // Ensure this collision is from a throw and not just any collision
        if (collision.relativeVelocity.magnitude > minimumImpactVelocity)
        {
            int damage = Mathf.FloorToInt(collision.relativeVelocity.magnitude * damageMultiplier);
            TakeDamage(damage);
        }


    }

    void Die() {
        Debug.Log("Enemy died.");
        // Destroy the enemy GameObject
        Destroy(gameObject);
    }

    public void TakeDamage (int damage){
        if (health > 0) health = health - damage;
        else Die();
        Debug.Log("Enemy health " + health);
    }
}
