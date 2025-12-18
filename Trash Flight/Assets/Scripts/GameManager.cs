using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI coinText;
    private int coin = 0;

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
}
