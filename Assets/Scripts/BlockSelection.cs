using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks;
    [SerializeField] private Transform parent;
    [SerializeField] private float timeBetweenDrop = 2f;
    [SerializeField] private float fallSpeed;

    private GameObject _block;
    private TetrisBlock _tetrisBlock;
    private bool _canSelect = true;
    
    private void Update()
    {
        GetNextBlock();
        
        if (_block == null) return;
        
        MoveBlock();
        RotateBlock();
        DropBlock();
    }
    
    private void GetNextBlock()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !_canSelect  || _block != null) return;
        
        _block = Instantiate(RandomBlock(), parent);
        _tetrisBlock = _block.GetComponent<TetrisBlock>();
        _tetrisBlock.SpawnInGameWorld();
        _canSelect = false;
    }

    private void MoveBlock()
    {
        var blockTransform = _block.transform;
        if (Input.GetKey(KeyCode.A))
        {
            blockTransform.position += new Vector3(-5f, 0f, 0f) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            blockTransform.position += new Vector3(5f, 0f, 0f) * Time.deltaTime;
        }
    }

    private void RotateBlock()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _tetrisBlock.RotateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _tetrisBlock.RotateRight();
        }
    }

    private void DropBlock()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) return;

        var rigidBody = _block.GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.None;
        rigidBody.gravityScale = fallSpeed;
        _block = null;

        StartCoroutine(UpdateDropTimer());
    }

    private GameObject RandomBlock()
    {
        var index = Random.Range(0, blocks.Count);
        return blocks[index];
    }

    private IEnumerator UpdateDropTimer()
    {
        yield return new WaitForSeconds(timeBetweenDrop);

        _canSelect = true;
    }
}
