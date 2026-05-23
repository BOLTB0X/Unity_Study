using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    [Header("Prefab Settings")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ResetZoneUI resetZoneUI;

    void Start()
    {
        Init();
    } // Start

    private void Init()
    {
    } // Init

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScore playerScore = other.GetComponent<PlayerScore>();

            if (playerScore != null)
            {
                int currentCount = playerScore.GetCurrentScore();
                Collider zoneCollider = GetComponent<Collider>();
                for (int i = 0; i < currentCount; i++)
                {
                    if (cubePrefab != null && spawnPoint != null)
                    {
                        float randomX = UnityEngine.Random.Range(zoneCollider.bounds.min.x, zoneCollider.bounds.max.x);
                        float randomZ = UnityEngine.Random.Range(zoneCollider.bounds.min.z, zoneCollider.bounds.max.z);

                        Vector3 spawnPosition = new Vector3(randomX, spawnPoint.position.y, randomZ);

                        // 프리팹 생성
                        GameObject newCube = Instantiate(cubePrefab, spawnPosition, spawnPoint.rotation);
                        // 슬라이드 작동
                        SlidingCube slidingCube = newCube.GetComponent<SlidingCube>();

                        if (slidingCube != null)
                        {
                            slidingCube.StartSlide((endPos) => {
                                SpawnFloatingText(endPos);
                            }
                            );
                        }
                    } // if
                } // for

                if (resetZoneUI != null)
                {
                    resetZoneUI.AddToTotal(currentCount);
                }

                playerScore.ResetScore();
            } // if
        } // if
    } // OnTriggerEnter

    private void SpawnFloatingText(Vector3 pos)
    {
        if (floatingTextPrefab == null)
        {
            //Debug.Log("floatingTextPrefab 널");
            return;
        }

        GameObject textObj = Instantiate(floatingTextPrefab, pos, Quaternion.identity);
        FloatingText floatingText = textObj.GetComponent<FloatingText>();
        if (floatingText != null)
        {
            //Debug.Log("SpawnFloatingText 실행");
            floatingText.Play(pos);
        }
    } // SpawnFloatingText

} // ResetZone
