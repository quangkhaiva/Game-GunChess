using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Menu UI hiển thị khi tạm dừng
    private bool isPaused = false; // Trạng thái tạm dừng

    private void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // Đảm bảo menu tạm dừng tắt khi bắt đầu
        }
    }

    private void Update()
    {
        // Kiểm tra khi nhấn phím Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); // Tiếp tục trò chơi
            }
            else
            {
                PauseGame(); // Tạm dừng trò chơi
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true); // Hiển thị menu tạm dừng
        }
        Time.timeScale = 0; // Dừng thời gian trong game
        isPaused = true; // Cập nhật trạng thái tạm dừng
        Debug.Log("Game Paused");
    }

    public void Resume()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // Tắt menu tạm dừng
        }
        Time.timeScale = 1; // Tiếp tục thời gian trong game
        isPaused = false; // Cập nhật trạng thái không tạm dừng
        Debug.Log("Game Resumed");
    }

    public void Home()
    {
        Time.timeScale = 1; // Đảm bảo thời gian trở lại bình thường
        SceneManager.LoadScene(0); // Quay về màn hình chính
        Debug.Log("Returned to Home");
    }

    public void Restart()
    {
        Time.timeScale = 1; // Đảm bảo thời gian trở lại bình thường
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại cảnh hiện tại
        Debug.Log("Game Restarted");
    }
}
