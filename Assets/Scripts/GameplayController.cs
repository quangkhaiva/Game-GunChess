using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;


public class GameplayController : MonoBehaviour
{
    private int level; // Màn ch?i hi?n t?i
    public TextMeshProUGUI levelText;
    private StaticBase SB;
    private Shop Sh;

    private void Start()
    {
        // L?y màn ch?i hi?n t?i t? GameManager
        SB = GameManager.Instance.SB;
        Sh = GameManager.Instance.Sh;
        level = GameManager.Instance.currentLevel;
        UpdateLevel();
        // G?i hàm ?? kh?i t?o màn ch?i d?a trên s? level
        InitializeLevel(level);
    }
    public void UpdateLevel()
    {
        levelText.text = "Màn " + level.ToString();
    }

    private void InitializeLevel(int level)
    {
        // Th?c hi?n kh?i t?o màn ch?i d?a trên level
        // Ví d?:
        Debug.Log("Initializing Level: " + level);

        // Tùy thu?c vào trò ch?i c?a b?n, thêm logic ?? t?o quái, v?t ph?m, hay thi?t l?p khác theo level.
    }
}
