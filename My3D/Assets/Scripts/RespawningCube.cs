using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningCube : MonoBehaviour
{
    [Header("Cube Settings")]
    [SerializeField] private float respawnTime = 1f;

    private MeshRenderer meshRenderer;
    private Collider cubeCollider;

    void Start()
    {
        Init();
    } // Start

    private void Init()
    {
        // 컴포넌트 참조 가져오기
        meshRenderer = GetComponent<MeshRenderer>();
        cubeCollider = GetComponent<Collider>();

        // 플레이어가 벽에 부딪히지 않고 통과할 수 있도록 트리거 모드 활성화
        if (cubeCollider != null)
        {
            cubeCollider.isTrigger = true;
        }
    } // Init

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"무언가 큐브에 부딪힘: {other.name} (태그: {other.tag})");

        // 부딪힌 오브젝트의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            PlayerScore playerScore = other.GetComponent<PlayerScore>();
            if (playerScore != null)
            {
                playerScore.AddScore(1);
            }

            // 숨기기 및 재생성 루틴 시작
            StartCoroutine(HideAndRespawnRoutine());
        }
    } // OnTriggerEnter

    private IEnumerator HideAndRespawnRoutine()
    {
        // 큐브를 비활성화 (숨기기 및 충돌체 끄기)
        SetCubeState(false);

        // 설정한 시간만큼 대기
        yield return new WaitForSeconds(respawnTime);

        // 큐브를 다시 활성화
        SetCubeState(true);
    } // HideAndRespawnRoutine

    private void SetCubeState(bool isVisible)
    {
        if (meshRenderer != null) meshRenderer.enabled = isVisible;
        if (cubeCollider != null) cubeCollider.enabled = isVisible;
    } // SetCubeState
} // RespawningCube
