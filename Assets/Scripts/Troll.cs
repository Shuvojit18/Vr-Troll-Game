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
    void Update()
    {
        // Example: Constantly check for certain conditions, like if health drops to 0.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Call this method to apply damage to the troll.
    public void TakeDamage(int damage)
    {
        if(currentHealth > 0) currentHealth -= damage;
        //Debug.Log("Troll takes " + damage + " damage. Current health: " + currentHealth);

        // Optionally, trigger a damage animation or effect here.
    }

    // Call this method for the troll to deal damage to another character.
    public void GiveDamage(GameObject target)
    {
        // Assume the target object has a script component that can take damage.
        // You would need to adapt this part to fit your game's architecture.
        target.GetComponent<Human>().TakeDamage(20); // Example damage value

        Debug.Log("Troll gives damage to " + target.name);
    }

    private void Die()
    {
        //Debug.Log("Troll died.");

        // Here you can add what happens when the troll dies, like playing an animation.
        // Destroy(gameObject); // Uncomment to destroy the troll object upon death.
    }

    // Example of detecting collision with another object (e.g., player's weapon).
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has a tag "Weapon" to apply damage.
        if (collision.gameObject.tag == "Weapon")
        {
            TakeDamage(20); // Example damage value
        }
    }
}

