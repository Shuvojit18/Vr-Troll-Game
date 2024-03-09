
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        // Check if the collider belongs to the player's hand controller
        if (other.CompareTag("PlayerHand")) {
            Application.Quit();
        }
    }
   
}
