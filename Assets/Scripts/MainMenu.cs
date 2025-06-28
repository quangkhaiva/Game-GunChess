using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); // Tải scene có chỉ số 1
    }

  /*  public void QuitGame()
    {
        Debug.Log("Player has quit the game");
        Application.Quit(); // Thoát ứng dụng
    }*/

    public void NextGame()
    {
        GameManager.Instance.NextLevel(); // Chuyển sang cấp độ tiếp theo
    }

    public void ResetGamee()
    {
        GameManager.Instance.ResetGame(); // Reset lại game
    }

    public void BackMenu()
    {
        GameManager.Instance.BackGame(); // Quay lại menu chính
    }
}
