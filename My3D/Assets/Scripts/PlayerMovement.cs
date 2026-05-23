using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothRotationTime = 0.1f; // 회전 부드러움 정도

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float rotationVelocity;
    private Transform cameraTransform;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    } // Awake

    private void Update()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, smoothRotationTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 finalMoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(finalMoveDir.normalized * moveSpeed * Time.deltaTime);
        }
    } // Update

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
    } // Move

} // PlayerMovement
