using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Text coinText; // Hiển thị số tiền
    public Button[] itemButtons; // Nút mua từng vật phẩm
    public int[] itemPrices; // Giá của từng vật phẩm
    public Text playerStats; // Hiển thị thông tin người chơi
    public Button nextButton; // Nút chuyển cảnh
    public Button rollButton; // Nút Roll
    public int rollCost = 5; // Giá Roll

    private int playerCoins = 100; // Số tiền ban đầu của người chơi
    private string[] randomItems = { "Sword", "Shield", "Potion", "Armor", "Ring" }; // Danh sách vật phẩm ngẫu nhiên

    private void Start()
    {
        UpdateUI();

        // Gán sự kiện cho nút Roll
        rollButton.onClick.AddListener(RollItem);
    }

    // Hàm cập nhật giao diện
    private void UpdateUI()
    {
        coinText.text = playerCoins + " G"; // Hiển thị tiền

        // Cập nhật trạng thái nút mua vật phẩm
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].interactable = playerCoins >= itemPrices[i];
        }

        // Cập nhật trạng thái nút Roll
        rollButton.interactable = playerCoins >= rollCost;
    }

    // Hàm để mua vật phẩm
    public void BuyItem(int itemIndex)
    {
        if (playerCoins >= itemPrices[itemIndex])
        {
            playerCoins -= itemPrices[itemIndex]; // Trừ tiền
            Debug.Log("Bought item " + itemIndex);
            // Cập nhật thông tin người chơi (tuỳ chỉnh theo game của bạn)
            playerStats.text = "Bought Item " + itemIndex;
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    // Hàm chuyển cảnh
    public void GoToNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }

    // Hàm Roll để random vật phẩm
    public void RollItem()
    {
        if (playerCoins >= rollCost)
        {
            playerCoins -= rollCost; // Trừ tiền Roll
            int randomIndex = Random.Range(0, randomItems.Length); // Chọn ngẫu nhiên vật phẩm
            string randomItem = randomItems[randomIndex]; // Vật phẩm được chọn
            Debug.Log("Rolled: " + randomItem);
            playerStats.text = "You got: " + randomItem; // Hiển thị vật phẩm nhận được
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough coins to roll!");
        }
    }
}
