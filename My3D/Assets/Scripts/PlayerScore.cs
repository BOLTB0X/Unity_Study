using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Color Settings")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color maxColor = Color.red;

    [Header("UI Offset")]
    public Vector3 scoreOffset = new Vector3(0, 2.5f, 0);

    private int currentScore = 0;
    private const int maxScore = 10;
    private bool isMaxReached = false;

    private Transform cameraTransform;

    void Start()
    {
        Init();
    } // Start

    private void Init()
    {
        cameraTransform = Camera.main.transform;

        if (scoreText != null)
        {
            scoreText.color = normalColor;
        }

        UpdateScoreUI();
    } // Init

    void LateUpdate()
    {
        if (scoreText != null && cameraTransform != null)
        {
            scoreText.transform.position = transform.position + scoreOffset;
            scoreText.transform.rotation = Quaternion.LookRotation(scoreText.transform.position - cameraTransform.position);
        }
    } // LateUpdate

    public void AddScore(int amount)
    {
        if (isMaxReached) return;

        currentScore += amount;

        if (currentScore >= maxScore)
        {
            currentScore = maxScore;
            isMaxReached = true;

            if (scoreText != null)
            {
                scoreText.text = "MAX";
                scoreText.color = maxColor;
            }
        }
        else
        {
            UpdateScoreUI();
        } // if - else
    } // AddScore

    private void UpdateScoreUI()
    {
        if (scoreText != null && !isMaxReached)
        {
            scoreText.text = $"{currentScore}";
        }
    } // UpdateScoreUI

    public void ResetScore()
    {
        currentScore = 0;
        isMaxReached = false;

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
            scoreText.color = normalColor;
        }

        UpdateScoreUI();
        Debug.Log("점수 초기화");
    } // ResetScore

    public int GetCurrentScore()
    {
        return currentScore;
    } // GetCurrentScore

} // PlayerScore
