﻿using Rewired;
using UnityEngine;
using UnityEngine.Events;

public class Character2DController : MonoBehaviour
{
    public float speed;            // Character's movement speed
    public float jumpForce;        // Character's jump force
    public Transform groundCheck;  // Transform of the ground checking object
    public float checkRadius;      // Radius of the ground checking object
    public LayerMask whatIsGround; // Ground layer
    public int extraJumpsValue;    // Number of extra jumps

    public UnityEvent onLandEvent;
    public UnityEvent onJumpEvent;

    private Rigidbody2D _rb;  // Character's rigidbody2D
    private bool _isGrounded; // Status of the character's position relative to the ground
    private int _extraJumps;  // Number of extra jumps
    private Player _runner;   // Runner inputs

    private float _move;
    private bool _jump;

    private void Awake() {
        // Getting components
        _rb = GetComponent<Rigidbody2D>();
        _runner = ReInput.players.GetPlayer(1);
    }

    private void Start() {
        if (onLandEvent == null)
            onLandEvent = new UnityEvent();

        if (onJumpEvent == null)
            onJumpEvent = new UnityEvent();

        _extraJumps = extraJumpsValue;

        // Enabling gameplay mode
        _runner.controllers.maps.mapEnabler.ruleSets.Find(rs => rs.tag == "Gameplay").enabled = true;
    }

    private void FixedUpdate() {
        // Cache grounded status
        var wasGrounded = _isGrounded;
        _isGrounded = false;

        // Updating character's status compared to his collision to ground
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        // If the character is landing
        if (!wasGrounded && _isGrounded)
            onLandEvent.Invoke();

        // Updating character's velocity
        _rb.velocity = new Vector2(_move * speed, _rb.velocity.y);
    }

    private void Update() {
        GetInputs();

        // If the character is on the ground
        if (_isGrounded) {
            // Extra jumps reset
            _extraJumps = extraJumpsValue;
        }

        switch (_jump) {
            case true when _extraJumps > 0: // If Jumping with extra jump
                // Impulse up
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _extraJumps--;
                // Invoking jump event
                onJumpEvent.Invoke();
                break;
            case true when _extraJumps == 0 && _isGrounded: // If jumping when grounded
                // Impulse up
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                // Invoking jump event
                onJumpEvent.Invoke();
                break;
        }
    }

    /**
     * Gets all the player inputs
     */
    private void GetInputs() {
        _move = _runner.GetAxis("Move Horizontal");
        _jump = _runner.GetButtonDown("Jump");
    }
}