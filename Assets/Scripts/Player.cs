using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    public int lives = 3;

    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isInmune = false;

    public float speed = 5f;
    public float jumpForce = 3f;
    public float movHor;

    public float inmuneTimeCont = 0f;
    public float inmuneTime = 0.5f;

    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movHor = Input.GetAxisRaw("Horizontal");
        isMoving = movHor != 0;
        if (isMoving)
        {
            flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
        }

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsGrounded", isGrounded);

    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(movHor * speed, rigidBody.velocity.y);

    }

    public void jump()
    {

        rigidBody.velocity = Vector2.up * jumpForce;
    }
    public void flip()
    {
        switch (movHor)
        {
            case 1:
                player.transform.localScale = new Vector2(1, player.transform.localScale.y);
                break;
            case -1:
                player.transform.localScale = new Vector2(-1, player.transform.localScale.y);
                break;
            default:
                break;
        }
    }

    void OnDestroy()
    {
        player = null;
    }

}
