using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StartGame : MonoBehaviour{
    
    public HumanSpawner humanSpawnerScript;

    private void OnTriggerEnter(Collider other)
    {// Check if the collider belongs to the player's hand controller
        if (other.CompareTag("PlayerHand")) {
            humanSpawnerScript.StartSpawning();
            Destroy(gameObject);
        }
    }
}
