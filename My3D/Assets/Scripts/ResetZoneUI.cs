using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetZoneUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI totalScoreText;

    private PlayerScore playerScore;
    private int totalScore = 0;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerScore = player.GetComponent<PlayerScore>();

        UpdateText();
    }

    public void AddToTotal(int amount)
    {
        totalScore += amount;
        UpdateText();
    }

    private void UpdateText()
    {
        if (totalScoreText == null) return;
        totalScoreText.text = $"$ {totalScore}";
    }
} // ResetZoneUI
