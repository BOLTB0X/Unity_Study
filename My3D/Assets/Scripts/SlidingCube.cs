using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingCube : MonoBehaviour
{
    [Header("Slide Settings")]
    [SerializeField] private Vector3 slideDirection = Vector3.down;
    [SerializeField] private float slideDistance = 2f;
    [SerializeField] private float slideDuration = 1f;

    private MeshRenderer cubeMeshRenderer;
    private bool isSliding;

    private Action<Vector3> onSlideComplete;

    void Start()
    {
        Init();
    } // Start

    private void Init()
    {
        cubeMeshRenderer = GetComponent<MeshRenderer>();
        isSliding = false;
    } // Init

    public void StartSlide(Action<Vector3> onComplete = null)
    {
        onSlideComplete = onComplete;
        if (!isSliding)
            StartCoroutine(SlideRoutine());
    }


    public void StartSlide()
    {
        if (!isSliding)
        {
            StartCoroutine(SlideRoutine());
        }
    } // StartSlide

    private IEnumerator SlideRoutine()
    {
        isSliding = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + (slideDirection.normalized * slideDistance);
        float elapsedTime = 0f;

        // slideDuration 동안 매 프레임 조금씩 이동
        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / slideDuration);
            yield return null;
        }

        transform.position = targetPosition;

        if (cubeMeshRenderer != null)
        {
            cubeMeshRenderer.enabled = false;
        }

        onSlideComplete?.Invoke(targetPosition);

        Destroy(gameObject);

    } // SlideRoutine

} // SlidingCube
