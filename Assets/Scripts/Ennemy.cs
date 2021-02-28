using System.Collections;
using UnityEngine;
using static Utils;

public class Ennemy : MonoBehaviour
{
    public float damage;

    public Transform playerTransform;
    public float speed;
    public float jumpForce;
    public float jumpRadius;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float lastPos;
    private float currentPos;

    public Animator animator;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        lastPos = rb.position.x;
        StartCoroutine(WaitDespawnEnemy(5f));
    }

    private void FixedUpdate() {
        currentPos = rb.position.x;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        var playerPositionwithoutY =
            playerTransform.position; //ça faisait un peu bugger l'ennemi si on utilisait directement la position du joueur
        playerPositionwithoutY.y = rb.position.y;

        transform.position = Vector2.MoveTowards(rb.position, playerPositionwithoutY, speed * Time.deltaTime);
        var movementX = currentPos - lastPos;

        switch (facingRight) {
            case true when movementX < 0:
            case false when movementX > 0:
                Flip();
                break;
        }

        animator.SetFloat(Speed, Mathf.Abs(movementX));

        if (Mathf.Abs(lastPos - currentPos) < jumpRadius && isGrounded == true) {
            rb.velocity = Vector2.up * jumpForce;
        }

        switch (isGrounded) {
            case false when rb.velocity.y > 0:
                animator.SetBool(IsJumping, true);
                animator.SetBool(IsFalling, false);
                break;
            case false when rb.velocity.y < 0:
                animator.SetBool(IsJumping, false);
                animator.SetBool(IsFalling, true);
                break;
            default:
                animator.SetBool(IsJumping, false);
                animator.SetBool(IsFalling, false);
                break;
        }

        lastPos = currentPos;
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Character2DController>().DiminishHealth(damage);
        }
    }

    private IEnumerator WaitDespawnEnemy(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}