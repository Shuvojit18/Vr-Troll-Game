using UnityEngine;

public class Troll : MonoBehaviour
{
    int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the troll's health when the game starts.
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Example: Constantly check for certain conditions, like if health drops to 0.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // apply damage to the troll.
    public void TakeDamage(int damage)
    {
        if(currentHealth > 0) currentHealth -= damage;
    }

    // damage to another character.
    public void GiveDamage(GameObject target)
    {
        target.GetComponent<Human>().TakeDamage(20); //damage value

        Debug.Log("Troll gives damage to " + target.name);
    }

    private void Die()
    {
        Debug.Log("Troll died.");
    }

    //  detecting collision with another object (e.g., player's weapon).
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has a tag "Weapon" to apply damage.
        if (collision.gameObject.tag == "Weapon")
        {
            TakeDamage(2); // damage value
        }
    }
}

