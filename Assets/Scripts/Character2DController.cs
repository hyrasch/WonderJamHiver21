﻿using Rewired;
using UnityEngine;
using UnityEngine.Events;

public class Character2DController : MonoBehaviour
{
    public float health = 10f;
    public float speed;            // Character's movement speed
    public float jumpForce;        // Character's jump force
    public Transform groundCheck;  // Transform of the ground checking object
    public Transform ceilCheck;    // Transform of the ground checking object
    public float checkRadius;      // Radius of the ground checking object
    public float ceilRadius;       // Radius of the ground checking object
    public LayerMask whatIsGround; // Ground layer
    public LayerMask whatIsBlock;  // Ground layer
    public int extraJumpsValue;    // Number of extra jumps
    public Vector3 respawnPoint;
    public UnityEvent onLandEvent;
    public UnityEvent onJumpEvent;

    private Rigidbody2D _rb;  // Character's rigidbody2D
    private bool _isGrounded; // Status of the character's position relative to the ground
    private int _extraJumps;  // Number of extra jumps
    private Player _runner;   // Runner inputs

    private float _move;
    private bool _jump;

    private int score;
    private int scoreP1;
    private int scoreP2;

    private bool _fallOnPlayer;

    private void Awake() {
        // Getting components
        _rb = GetComponent<Rigidbody2D>();
        _runner = ReInput.players.GetPlayer("Runner");
    }

    private void Start() {
        if (onLandEvent == null)
            onLandEvent = new UnityEvent();

        if (onJumpEvent == null)
            onJumpEvent = new UnityEvent();

        _extraJumps = extraJumpsValue;

        _runner.controllers.maps.mapEnabler.ruleSets.Find(rs => rs.tag == "Runner").enabled = true;
        _runner.controllers.maps.mapEnabler.Apply();

        score = 0;
        respawnPoint = transform.position;
    }

    private void FixedUpdate() {
        // Cache grounded status
        var wasGrounded = _isGrounded;
        _isGrounded = false;

        // Updating character's status compared to his collision to ground
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        _fallOnPlayer = Physics2D.OverlapCircle(ceilCheck.position, ceilRadius, whatIsBlock);

        if (_fallOnPlayer) {
            DiminishHealth(2f);
        }

        switch (wasGrounded) {
            case false when _isGrounded: // If the character is landing
                onLandEvent.Invoke();
                break;
        }

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
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
                _extraJumps--;
                // Invoking jump event
                onJumpEvent.Invoke();
                break;
            case true when _extraJumps == 0 && _isGrounded: // If jumping when grounded
                // Impulse up
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
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


    public void DiminishHealth(float damage) {
        health -= damage;
        if (health > 0.01) return;

        respawn();
        FindObjectOfType<PostGameUIManager>().EndGame();
    }

    public void respawn() {
        this.gameObject.transform.position = respawnPoint;
        this.health = 1.0f;
        this.score = 0;
    }

    public int GetScore() {
        return score + 4 + (int) Mathf.Round(gameObject.transform.localPosition.y);
    }

    public void AddToScore(int value) {
        score += value;
    }
}