using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth baseHealth;
    public GameObject[] enemies;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemies.Length);

        Instantiate (enemies[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
