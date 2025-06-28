using UnityEngine;
using TMPro;
using System;
public class Health : MonoBehaviour
{
    public bool bishop = false;
    public bool pawn = false;
    public bool queen = false;
    public bool rook = false;
    public bool knight = false;
    public bool Player = false;

    public int maxHealth;
    public int armor;
    [HideInInspector] public int currentHealth;

    public HealthBar healthBar;

    private float safeTime;
    public float safeTimeDuration = 0f;
    public bool isDead = false;

    public bool camShake = false;

    public GameObject popUpDamagePrefab;
    public TMP_Text popUpText;
    public Transform popupPosition; // Thêm bi?n này ?? xác ??nh v? trí popup

    private StaticBase sb;
    private Shop shop;
    private int coin;

    private void Start()
    {
        sb = GameManager.Instance.SB;
        shop = GameManager.Instance.Sh;
        if (Player)
        {
            maxHealth = sb.PlayerHP;
            armor = sb.PlayerArmor;
        }
        if (queen)
        {
            maxHealth = sb.QueenHP;
            armor = sb.QueenArmor;
            coin = sb.QueenCoin;
        }
        if (rook)
        {
            maxHealth = sb.RookHP;
            armor = sb.RookArmor;
            safeTimeDuration = sb.RockSafeTimeDuration;
            coin = sb.RookCoin;
        }
        if (knight)
        {
            maxHealth = sb.KnightHP;
            armor = sb.KnightArmor;
            coin = sb.KnightCoin;
        }
        if (bishop)
        {
            maxHealth = sb.BishopHP;
            armor = sb.BishopArmor;
            coin = sb.BishopCoin;
        }
        if (pawn)
        {
            maxHealth = sb.PawnHP;
            armor = sb.PawnArmor;
            coin = sb.PawnCoin;
        }

        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDam(int damage)
    {
        int dam = damage - armor;
        if (safeTime <= 0)
        {
            currentHealth -= dam;
            // ?i?u ch?nh v? trí popup d?a trên popupPosition
            Vector3 popupSpawnPosition = popupPosition != null ? popupPosition.position : transform.position;
            GameObject popupInstance = Instantiate(popUpDamagePrefab, popupSpawnPosition, Quaternion.identity);

            // C?p nh?t text cho popup
            TMP_Text damageText = popupInstance.GetComponentInChildren<TMP_Text>();
            if (damageText != null)
                damageText.text = dam.ToString();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (this.gameObject.tag == "Enemy")
                {
                    shop.AddMoney(coin);
                    Destroy(this.gameObject, 0f);
                }
                isDead = true;
            }

            // If player then update health bar
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }

    private void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
        if (isDead)
        {
            GameManager.Instance.GameOver();
        }
    }
}
