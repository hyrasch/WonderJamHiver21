using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public int damage;

    public Transform playerTransform;
    public float speed;
    public float jumpForce;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float lastPos;
    private float currentPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPos = rb.position.x;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        transform.position = Vector2.MoveTowards(rb.position, playerTransform.position, speed * Time.deltaTime);
        
        switch (facingRight)
        {
            case false when rb.velocity.x > 0:
            case true when rb.velocity.x < 0:
                Flip();
                break;
        }

        currentPos = rb.position.x;

        if (Mathf.Abs(lastPos - currentPos) < 0.01f && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        lastPos = currentPos;
    }
    private void Update()
    {
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Character2DController>().health -= damage;
        }
    }

}
