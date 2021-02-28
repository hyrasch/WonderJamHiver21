using System.Collections;
using UnityEngine;
using static Utils;

public class CharacterSprite : MonoBehaviour
{
    private Rigidbody2D _rb;                // Character rigidbody
    private BoxCollider2D _boxCollider;
    private Animator _animator;             // Character animator
    private SpriteRenderer _spriteRenderer; // Character sprite

    private bool _isFacingRight = true; // Facing status of the sprite

    private void Awake() {
        _rb = GetComponentInParent<Rigidbody2D>();
        _boxCollider = GetComponentInParent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        CheckFlip();

        // Character's running animation based on his speed
        _animator.SetFloat(Speed, Mathf.Abs(_rb.velocity.x));
        // Character's falling animation based on his fall speed
        _animator.SetFloat(FallSpeed, _rb.velocity.y);
    }

    /**
     * Flips the character's sprite based on his current speed
     */
    private void CheckFlip() {
        switch (_isFacingRight) {
            case false when _rb.velocity.x > 0: // If looking left and going right
            case true when _rb.velocity.x < 0:                  // If looking right and going left
                _spriteRenderer.flipX = !_spriteRenderer.flipX; // Invert flipping
                _isFacingRight = !_isFacingRight;
                // Invert facing status
                break;
        }
    }

    /**
     * Stops the current jumping / falling animation
     */
    public void StopJumpAnim() {
        _animator.ResetTrigger(Jump);
        _animator.SetTrigger(Grounded);
    }

    /**
     * Starts the jumping animation
     */
    public void StartJumpAnim() {
        _animator.ResetTrigger(Grounded);
        _animator.SetTrigger(Jump);
    }
}