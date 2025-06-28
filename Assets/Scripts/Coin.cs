using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int currentPlayerMoney;
    private Shop shop;

    private void Start()
    {
        shop = GameManager.Instance.Sh;
    }
    private void Update()
    {
        currentPlayerMoney = shop.playerMoney;
        UpdateCoin(currentPlayerMoney);
    }

    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
