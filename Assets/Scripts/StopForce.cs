using UnityEngine;

public class StopForce : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector3 _position, _velocity;
    private float _rotation, _angularVelocity;
    private bool _isColliding;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isColliding) return;
        
        _position = _rigidbody.position;
        _rotation = _rigidbody.rotation;
        _velocity = _rigidbody.velocity;
        _angularVelocity = _rigidbody.angularVelocity;
    }

    private void LateUpdate()
    {
        if (!_isColliding) return;
        
        _rigidbody.position = _position;
        _rigidbody.rotation = _rotation;
        _rigidbody.velocity = _velocity;
        _rigidbody.angularVelocity = _angularVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(tag))
        {
            _isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.CompareTag(tag))
        {
            _isColliding = false;
        }
    }
}
