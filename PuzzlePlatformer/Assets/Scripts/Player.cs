using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform GroundedChecker;
    [SerializeField] private float checkGroundRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float rememberGroundedFor;
    [SerializeField] private int defaultAdditionalJumps = 1;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float startDashTime;

    private bool isGrounded = false;
    private float lastTimeGrounded;
    private int additionalJumps;
    private bool facingRight;
    private float dashTime;
    private int direction;

    [Header("Realm Switch")]
    [SerializeField] private GameObject normalRealm;
    [SerializeField] private GameObject shadowRealm;
    [SerializeField] private Camera MainCamera;
    private bool realmSwitch;


    private Rigidbody2D rb;
    private Animator PlayerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        dashTime = startDashTime;

        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerAnimator.SetFloat("DashTime", startDashTime);
        PlayerAnimator.SetFloat("DashSpeed", dashSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        Move(horizontal);
        Jump();
        BetterJump();
        Dash(horizontal);
        Flip(horizontal);
        CheckIfGrounded();
        RealmSwitch();
    }

    void Move(float horizontal)
    {
        float moveBy = horizontal * MovementSpeed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        PlayerAnimator.SetFloat("speed", horizontal);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            PlayerAnimator.SetTrigger("Jump");
            additionalJumps--;
        }
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(GroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
            PlayerAnimator.SetBool("Grounded", true);
            additionalJumps = defaultAdditionalJumps;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
            PlayerAnimator.SetBool("Grounded", false);
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 thescale = transform.localScale;
            thescale.x *= -1;
            transform.localScale = thescale;
        }
    }

    private void RealmSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            realmSwitch = !realmSwitch;
        }
        if (realmSwitch)
        {
            normalRealm.SetActive(false);
            shadowRealm.SetActive(true);
            PlayerAnimator.SetBool("ShadowRealm", true);
            MainCamera.GetComponent<ToggleAdjustment>().SetAdjustments(true);
        }
        else
        {
            shadowRealm.SetActive(false);
            normalRealm.SetActive(true);
            PlayerAnimator.SetBool("ShadowRealm", false);
            MainCamera.GetComponent<ToggleAdjustment>().SetAdjustments(false);
        }
    }

    private void Dash(float horizontal)
    {

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            if (horizontal < 0)
            {
                direction = 1;
            }
            else if (horizontal > 0)
            {
                direction = 2;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                    PlayerAnimator.SetTrigger("Dash");
                }
                else if (direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                    PlayerAnimator.SetTrigger("Dash");
                }
            }
        }

    }
}
