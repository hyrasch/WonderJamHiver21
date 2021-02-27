using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks;
    [SerializeField] private Transform parent;
    [SerializeField] private float timeBetweenDrop = 2f;

    private GameObject _block;
    private bool _canSelect = true;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GetNextBlock();
        
        if (_block == null) return;
        
        MoveBlock();
        DropBlock();
    }
    
    private void GetNextBlock()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !_canSelect  || _block != null) return;
        
        _block = Instantiate(RandomBlock(), parent);
        _canSelect = false;
    }

    private void MoveBlock()
    {
        var blockTransform = _block.transform;
        if (Input.GetKey(KeyCode.A))
        {
            blockTransform.position += new Vector3(-0.01f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            blockTransform.position += new Vector3(0.01f, 0f, 0f);
        }
    }

    private void DropBlock()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) return;
        
        _block.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
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
