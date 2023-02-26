using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharControl : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    [Header("Ground Check")]
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    const float checkRadiusGround = 0.15f;

    [Header("Wall Movement")]
    private bool isOnWall;
    private bool isWallSliding;
    [SerializeField] float wallSlideSpeed = 0f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask whatIsWall;
    const float checkRadiusWall = 0.28f;

    [Header("Jumping")]
    private Rigidbody2D rigid;
    [SerializeField] private float jumpHeight = 6f;

    [Header("Movement")]
    [SerializeField] private float speed = 6f;
    private Vector2 refVelocity = Vector2.zero;
    [SerializeField] private float moveSmoothing = 0.05f;

    [Header("Take Damage")]
    [SerializeField] private float recoil = 30f;

    [Header("Knockback")]
    [SerializeField] private float knockback = 5f;
    [SerializeField] public float knockbackLength = 0.5f;
    [SerializeField] public float knockbackCount = 0f;
    public bool knockFromRight;

    [Header("Particles")]
    public GameObject blood;

    //flip sprite
    private bool faceLeft = false;
    private SpriteRenderer sprite;
    private UserControl player;
    private AudioSource audioHit;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = this.GetComponent<UserControl>();
        audioHit = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGround();
        CheckWall();
        WallSliding();
    }

    public void Move(float move, bool jump)
    {
        Jumping(jump);
        Movement(move);
        CheckOrientation(move);
    }

    void Jumping(bool jump)
    {
        if (jump && isGrounded)
        {
            rigid.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
        }
        else if ((isWallSliding || isOnWall) && jump)
        {
            rigid.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    void Movement(float move)
    {
         if (knockbackCount <= 0)
         {
             Vector2 targetVelocity = new Vector2(move * speed, rigid.velocity.y);
             rigid.velocity = Vector2.SmoothDamp(rigid.velocity, targetVelocity,
                 ref refVelocity, moveSmoothing);
         }
         else if (knockFromRight)
         {
             rigid.velocity = new Vector2(-knockback, knockback);
         }
         else if (!knockFromRight)
         {
             rigid.velocity = new Vector2(knockback, knockback);
         }
         knockbackCount -= Time.deltaTime;
    }

    void WallSliding()
    {
        if (isOnWall == true && isGrounded == false && rigid.velocity.y < 0)
            isWallSliding = true;
        else isWallSliding = false;

        if(isWallSliding)
        {
            player.wall = true;
            rigid.velocity = new Vector2(rigid.velocity.x, wallSlideSpeed);
        }
        else
        {
            player.wall = false;
        }
    }

    void CheckGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, checkRadiusGround, whatIsGround);
        isGrounded = (collider != null) ? true : false;
    }

    void CheckWall()
    {
        Collider2D collider = Physics2D.OverlapCircle(wallCheck.position, checkRadiusWall, whatIsWall);
        isOnWall = (collider != null) ? true : false;
    }

    void CheckOrientation(float move)
    {
        if ((move > 0 && faceLeft) || (move < 0 && !faceLeft) && !isWallSliding)
        {
            faceLeft = !faceLeft;
            sprite.flipX = faceLeft;
        }
    }

    public void TakeDamage()
    {
        player.damage = true;

        audioHit.Play();
        blood.transform.position = player.transform.position;
        blood.GetComponent<ParticleSystem>().Play();

        manager.LoseLife();
    }
}
