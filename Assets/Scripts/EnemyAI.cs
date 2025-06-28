using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool bishop = false;
    public bool pawn = false;
    public bool queen = false;
    public bool rook = false;
    public bool knight = false;

    bool reachDestination = false;
    public float moveSpeed;
    public float nextWPDistance = 0.3f;
    public bool updateContinuesPath;
    public Seeker seeker;
    Path path;
    Rigidbody2D rb;
    public SpriteRenderer characterSR;
    public Animator animator;

    Coroutine moveCoroutine;
    private GameObject player;
    public StaticBase sb;


    Health PlayerHealth;
    

    // shoot
    public bool isShootable;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    private float fireCooldown;
    public int damage;

    // sound
    public AudioClip collisionSound; // File âm thanh va chạm
    public AudioSource audioSource;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sb = GameManager.Instance.SB;

        if (pawn)
        {
            moveSpeed = sb.PawnMoveSpeed;
            damage = sb.PawnDam;
        }
        if (queen)
        {
            moveSpeed = sb.QueenMoveSpeed;
            damage = sb.QueenDam;
        }
        if (rook)
        {
            moveSpeed = sb.RookMoveSpeed;
            damage = sb.RookDam;
        }
        if (knight)
        {
            moveSpeed = sb.KnightMoveSpeed;
            damage = sb.KnightDam;
        }
        if (bishop)
        {
            moveSpeed = sb.BishopMoveSpeed;
            damage = sb.BishopDam;
        }


        player = Object.FindFirstObjectByType<Player>().gameObject;

        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
    }
    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown < 0 && isShootable)
        {
            fireCooldown = timeBtwFire;
            EnemyFireBullet();
        }
    }

    void EnemyFireBullet()
    {
        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

        Vector3 playerPos = player.transform.position;
        Vector3 direction = playerPos - (Vector3)rb.position;
        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();
        if (seeker.IsDone() && (reachDestination || updateContinuesPath))
            seeker.StartPath(rb.position, target, OnPathCompleted);
    }

    void OnPathCompleted(Path p)
    {
        if (p.error) return;

        path = p;
        // move
        MoveToTarget();

    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }
    
    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)rb.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
                currentWP++;

            if (force.x != 0)
                if (force.x < 0)
                    characterSR.transform.localScale = new Vector3(-1, 1, 0);
                else
                    characterSR.transform.localScale = new Vector3(1, 1, 0);

            yield return null;
        }
        reachDestination = true;
    }

    Vector2 FindTarget()
    {
        Vector3 playerPos = player.transform.position;
        if (bishop)
            return (Vector2)playerPos + (Random.Range(3f, 3f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
        if (pawn)
            return playerPos;
        return playerPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth = collision.GetComponent<Health>();
            InvokeRepeating("DamagePlayer", 0, 1f);
            // Phát âm thanh va chạm
            if (collisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CancelInvoke("DamagePlayer");
            PlayerHealth = null;
        }
        
    }

    void DamagePlayer()
    {
        
        PlayerHealth.TakeDam(damage);
    }
}
