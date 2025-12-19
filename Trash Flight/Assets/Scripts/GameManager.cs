using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI coinText;
    private int coin = 0;

    [SerializeField]
    private GameObject gameOverPanel;

    [HideInInspector]
    public bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void IncreaseCoin()
    {
        coin++;
        coinText.SetText(coin.ToString());

        if (coin % 5 == 0)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;

        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.StopEnemyRoutine();
        }
        
        Invoke("ShowGameOverPanel", 0.6f);
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
