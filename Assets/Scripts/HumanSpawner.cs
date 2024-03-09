using UnityEngine;
using System.Collections;

public class HumanSpawner : MonoBehaviour
{
    public GameObject humanPrefab; // Assign
    public float spawnInterval = 5f; // Time between each spawn
    private int humansToSpawn = 1; // humans to spawn in the next wave

    public void StartSpawning()
    {
        StartCoroutine(SpawnHumans());

    }

    IEnumerator SpawnHumans()
    {
        while(true) // Infinite loop to keep spawning
        {
            for (int i = 0; i < humansToSpawn; i++)
            {
                Instantiate(humanPrefab, transform.position, humanPrefab.transform.rotation);
            }

            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval
            humansToSpawn++; // Increment the number of humans to spawn next time
        }
    }
}
