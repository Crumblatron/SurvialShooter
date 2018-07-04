using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    int increaseSpawnRate = 20;

    void Start() {
        // Invoke repeating is probably more expensive then putting the logic into the Update function, but for now this is fine
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        InvokeRepeating("IncreaseSpawnRate", increaseSpawnRate, increaseSpawnRate);
    }

    void Spawn() {
        if(playerHealth.currentHealth <= 0f) {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    void IncreaseSpawnRate() {

        //Todo: May want to do a logarithmic curve here
        var spawnIncrease = 0.5f;

        CancelInvoke("Spawn");

        // Limit so that spwnrate never goes below 0.5 seconds
        if((spawnTime - spawnIncrease) < 0.5) {
            spawnTime = 0.5f;
        } else {
            spawnTime -= spawnIncrease;
        }
        //Todo: Probably want to adjust for the last spawntime here instead of setting 0
        InvokeRepeating("Spawn", 0, spawnTime);
    }
}
