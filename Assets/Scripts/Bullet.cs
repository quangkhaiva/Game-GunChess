using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public bool PlayerBullet;

    private StaticBase sb;
    void Start()
    {
        sb = GameManager.Instance.SB;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerBullet)
        {
            int damage = sb.BishopDam;
            collision.GetComponent<Health>().TakeDam(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy") && PlayerBullet)
        {
            int damage = sb.DamBullet;
            collision.GetComponent<Health>().TakeDam(damage);
            Destroy(gameObject);
        }
    }
}
