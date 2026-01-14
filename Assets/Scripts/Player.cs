using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 2f;
    public Vector3 moveInput;
    private Rigidbody2D rb;
    public SpriteRenderer characterSR;
    public Animator animator;
    private StaticBase sb;

    public float dashBoost = 1f;
    private float dashTime;
    public float DashTime = 3f;
    bool dashOnce = false;
    // gioi han ban co
    public Vector2 minBoundary = new Vector2(-8.875f, -8.875f);
    public Vector2 maxBoundary = new Vector2(8.875f, 8.875f);
    public Vector2 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sb = GameManager.Instance.SB;
        speed = sb.PlayerMoveSpeed;
        dashBoost = sb.DashSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += speed * Time.deltaTime * moveInput;

        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            animator.SetBool("dash", true);
            speed += dashBoost;
            dashTime = DashTime;
            dashOnce = true;
        }
        if (dashTime <= 0 && dashOnce == true)
        {
            animator.SetBool("dash", false);
            speed -= dashBoost;
            dashOnce = false;

        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        if (moveInput.x != 0)
        {
            if (moveInput.x < 0)
                characterSR.transform.localScale = new Vector3(-1, 1, 0);
            else
                characterSR.transform.localScale = new Vector3(1, 1, 0);
        }

    }
    void FixedUpdate()
    {
        Vector3 newPosition = transform.position + new Vector3(movement.x, movement.y, 0) * sb.PlayerMoveSpeed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minBoundary.x, maxBoundary.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBoundary.y, maxBoundary.y);

        //cap nhan vi tri
        transform.position = newPosition;
    }

}
