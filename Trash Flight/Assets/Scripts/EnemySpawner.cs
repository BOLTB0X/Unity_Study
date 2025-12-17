using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    private float[] arrPosX = new float[] { -2.2f, -1, 1f, 0f, 1.1f, 2.2f };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (float i in arrPosX)
        {
            int index = Random.Range(0, enemies.Length);
            SpawnEnemy(i, index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(float posX, int index)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        Instantiate(enemies[index], spawnPos, Quaternion.identity);
    }
}
