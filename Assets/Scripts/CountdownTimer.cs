using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // ?? qu?n lý chuy?n scene
using TMPro; // N?u dùng TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; // Th?i gian ??m ng??c (60 giây)
    public TMP_Text timerText; // Text ?? hi?n th? th?i gian (ho?c dùng Text n?u không dùng TMP)

    private float currentTime;

    private void Start()
    {
        // Gán giá tr? th?i gian b?t ??u
        currentTime = countdownTime;
    }

    private void Update()
    {
        // Gi?m th?i gian m?i frame
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            // ??m b?o th?i gian không b? âm
            if (currentTime < 0)
                currentTime = 0;

            // C?p nh?t UI hi?n th? th?i gian
            UpdateTimerText();
        }
        else
        {
            // Chuy?n sang scene Shop
            GameManager.Instance.GoToShop();
        }
    }

    private void UpdateTimerText()
    {
        // ??nh d?ng th?i gian theo phút:giây
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{seconds:00}";
    }
}
