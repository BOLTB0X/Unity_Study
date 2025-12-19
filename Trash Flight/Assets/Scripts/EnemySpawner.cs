using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;
    private float[] arrPosX = new float[] { -4.2f, -2.2f, 0f, 2.2f, 4.2f };
    [SerializeField]
    private float spawnInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEnemyRoutine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(2f);

        int spawnCount = 0;
        int enumyIndex = 0;
        float moveSpeed = 5f;

        while (true)
        {
            foreach (float i in arrPosX)
            {
                SpawnEnemy(i, enumyIndex, moveSpeed);
            }

            spawnCount++;

            if (spawnCount % 3 == 0)
            {
                enumyIndex++;
                moveSpeed += 2f;
            }

            if (enumyIndex >= enemies.Length)
            {
                SpawnBoss();
                enumyIndex = 0;
                moveSpeed = 5f;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        index = (index + Random.Range(0, 5)) % enemies.Length;
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
