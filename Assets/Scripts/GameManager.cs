using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton
    public StaticBase SB;
    public Shop Sh;

    public int currentLevel = 1; // Màn chơi hiện tại

    private void Awake()
    {
        // Đảm bảo chỉ có một GameManager tồn tại
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Không phá hủy khi chuyển scene
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Hủy nếu đã có GameManager khác
        }
    }

    private void Start()
    {
        // Khởi tạo các chỉ số cơ bản và tài nguyên
        SB = new StaticBase();
        Sh = new Shop();
    }

    // Chuyển sang màn chơi tiếp theo
    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadSceneAsync(1);
    }

    public void GameOver()
    {
        SceneManager.LoadSceneAsync(3);
    }

    // Chuyển đến scene Shop
    public void GoToShop()
    {
        SceneManager.LoadSceneAsync(2);
    }

    // Reset về màn đầu tiên (nếu cần)
    public void ResetGame()
    {
        currentLevel = 1;
        SB = new StaticBase();
        Sh = new Shop();
        SceneManager.LoadSceneAsync(1);
    }

    // Quay lại menu chính
    public void BackGame()
    {
        currentLevel = 1;
        SB = new StaticBase();
        Sh = new Shop();
        SceneManager.LoadSceneAsync(0);
    }

    // Thoát game
    public void QuitGame()
    {
        Debug.Log("Player has quit the game");
        // Thoát trò chơi khi đã Build
        Application.Quit();
    }
}