using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    [SerializeField]
    Vector3 _InitialPosition;
    [SerializeField]
    Vector3 _InitialRotation;

    Sprite _SpriteComponent;



    // Start is called before the first frame update
    void Start()
    {
        _SpriteComponent = GetComponent<Sprite>();
        
    }

    public void RotateRight() {
        transform.Rotate(0,0,90);
    }

    public void RotateLeft() {
        transform.Rotate(0, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {}

    public void SpawnInGameWorld(Vector3 Position, Vector3 Rotation, Vector3 Scale) {
        _InitialPosition = Position;
        _InitialRotation = Rotation;

        transform.position = Position;
        transform.rotation = Quaternion.Euler(Rotation);
        transform.localScale = Scale;
    }
}
