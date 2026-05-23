using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // 쫓아갈 대상 (Player 캡슐)

    [Header("Movement Settings")]
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -8f);

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Init();
    } // Start

    private void Init()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraFollow 스크립트에 Target이 할당 X.");
        }
    } // Init

    void LateUpdate()
    {
        if (target == null) return;

        // 카메라가 가야 할 목표 위치(플레이어 위치 + 오프셋) 계산
        Vector3 desiredPosition = target.position + offset;

        // 현재 위치에서 목표 위치까지 부드럽게 이동 (SmoothDamp)
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            1f / smoothSpeed
        );

        transform.LookAt(target);
    } // LateUpdate

} // CameraFollow
