using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance; // Đối tượng Singleton

    public bool isPaused = false; // Biến kiểm tra trạng thái tạm dừng
    private GameObject pauseUI; // Không sử dụng nút UI, chỉ bật/tắt bằng phím

    private void Start()
    {
        pauseUI = GameObject.Find("PauseMenu"); // Tìm kiếm PauseMenu trong cảnh
        if (pauseUI != null)
        {
            pauseUI.SetActive(false); // Tắt menu tạm dừng khi bắt đầu
        }
    }

    private void Update()
    {
        // Kiểm tra khi nhấn phím Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause(); // Chuyển đổi trạng thái tạm dừng
        }
    }

    public void SwitchPause()
    {
        if (pauseUI == null) return; // Kiểm tra nếu UI chưa được gán

        isPaused = !isPaused; // Đổi trạng thái tạm dừng
        pauseUI.SetActive(isPaused); // Hiển thị hoặc ẩn giao diện tạm dừng
        Time.timeScale = isPaused ? 0 : 1; // Ngừng hoặc tiếp tục thời gian
    }

    public void Restart()
    {
        ResetTimeScale(); // Đảm bảo thời gian không bị tạm dừng
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Tải lại cảnh hiện tại
    }

    public void Quit()
    {
        ResetTimeScale(); // Đảm bảo thời gian không bị tạm dừng
        SceneManager.LoadScene("Menu"); // Quay về menu chính
    }

    private void Awake()
    {
        // Thiết lập Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ đối tượng này không bị hủy khi tải cảnh mới
        }
        else
        {
            Destroy(gameObject); // Hủy đối tượng nếu đã tồn tại
        }
    }

    public void NextLevel()
    {
        ResetTimeScale(); // Đảm bảo thời gian không bị tạm dừng
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); // Tải cảnh tiếp theo
    }

    public void LoadScene(string sceneName)
    {
        ResetTimeScale(); // Đảm bảo thời gian không bị tạm dừng
        SceneManager.LoadSceneAsync(sceneName); // Tải cảnh theo tên
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1; // Khôi phục tốc độ thời gian về bình thường
    }
}
