using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // 1-2 day
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private Vector2 velocity;
    private float InputAxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    // 3 day
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(InputAxis) > 0.25f;
    public bool sliding => (InputAxis > 0f && velocity.x < 0f) || (InputAxis < 0f && velocity.x > 0f);
    
    // Start is called before the first frame update

    // private void Start // no one OnDisable and OnEnable 
    private void Awake() // all script running 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        camera = Camera.main;

    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;

    }

    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    // Update is called once per frame
    private void Update()
    {
        HorizontalMovement();
        
        grounded = rigidbody.Raycast(Vector2.down);

        if(grounded)
        {
            GroundedMovemnet();
        }

        ApplyGravity();
    }
    
    //2 day
    private void GroundedMovemnet() // velocity.y > 0f on the ground
    {
        
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
        else
        {
            jumping = false;
        }

    }
    // 2 day
    private void ApplyGravity() //
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f); // tốc độ rơi (-1.3, -20)
    }
    private void HorizontalMovement()
    {
        InputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, InputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        // 3 day

        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }
        
        // player 0-180 độ and velocity.y --> 0-180 độ
        if(velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
            
        }
        else if(velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);// (0,0)
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f); // giới hạn phạm vi X

        rigidbody.MovePosition(position);
    }

    // 3 DAY

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // player juming nhảy lên kẻ địch trúng 1 lần nữa  phần NameToLayer("Enemy")
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                jumping = true;

            }
        }
        else if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            //velocity.y = 0f;
            if(transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;    
            }

        }
    }
}
