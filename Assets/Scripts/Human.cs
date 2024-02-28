using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    //public float speed = 5.0f;
    public float attackRange = 1.5f;
    public int attackDamage = 1;
    int health = 100;
    public GameObject weaponRest;
    public GameObject weaponCombat;
    //public GameObject CombatPos;

    private Transform trollTransform;
    private bool isPlayerInRange = false;

    Troll troll;

    public float minimumImpactVelocity = 3.0f; // Requires a moderate throw speed to cause damage
    public float damageMultiplier = 1.5f; // Makes the damage somewhat higher than the impact speed

    private Animator animator;

    void Start()
    {
        // player has a tag called "Player"
        troll = FindObjectOfType<Troll>();
        trollTransform = troll.transform;
        animator = GetComponent<Animator>();
        //ai navigation
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = trollTransform.position;

        weaponRest.SetActive(true);
        weaponCombat.SetActive(false);
        //StartCoroutine(EverySecond());
    }

    // IEnumerator EverySecond(){
    //     if (isPlayerInRange) AttackPlayer();
    //     else MoveTowardsPlayer();
    //     yield return new WaitForSeconds(0.5f);
    // }
    

   void FixedUpdate(){
        
        if (isPlayerInRange) AttackPlayer();
        else MoveTowardsPlayer();
        if (health == 0) Die();  
    }

    void MoveTowardsPlayer(){

        animator.SetBool("isRunning", true);
        // Check if the player is within the attack range
        if (Vector3.Distance(transform.position, trollTransform.position) < attackRange){
            isPlayerInRange = true;
            animator.SetBool("inRange", true);    
        } else{
            isPlayerInRange = false;
            animator.SetBool("inRange", false);
        }
    }

    void AttackPlayer(){
        weaponRest.SetActive(false);
        weaponCombat.SetActive(true);
        animator.SetBool("isAttacking", true);
        troll.TakeDamage(attackDamage);
        // attack
    }

    void OnCollisionEnter(Collision collision){
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
        animator.SetBool("isDead", true);
        // Destroy the enemy GameObject
        //Destroy(gameObject);
    }

    public void TakeDamage (int damage){
        if (health > 0) health = health - damage;
        else Die();
        //Debug.Log("Enemy health " + health);
    }
}
