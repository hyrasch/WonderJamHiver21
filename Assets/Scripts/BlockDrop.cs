using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Random = UnityEngine.Random;

public class BlockDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks;
    [SerializeField] private Transform parent;
    [SerializeField] private float timeBetweenDrop = 2f;
    [SerializeField] private float fallSpeed;
    [SerializeField] private Image wheel;
    [SerializeField] private Character2DController player;
    [SerializeField] private BlocSelection blockSelection;
    [SerializeField] private TextMeshProUGUI textEffect;

    private Player _master;
    private GameObject _block;
    private TetrisBlock _tetrisBlock;
    private bool _canSelect = true;
    public bool turnP1;

    bool _statusDrop;

    void Awake() {
        _master = ReInput.players.GetPlayer("Master"); 
    }

    private void Start()
    {
        _master.controllers.maps.mapEnabler.ruleSets.Find(rs => rs.tag == "Master").enabled = true;
        _master.controllers.maps.mapEnabler.Apply();
    }

    public void setTurnP2()
    {
        turnP1 = false;
    }
    private void Update()
    {
        if (!_statusDrop)
        {
            GetNextBlock();
        }
        else {
            DropBlock();
        }
        
        if (_block == null) return;

        MoveBlock();
        RotateBlock();
    }

    private void GetNextBlock()
    {
        if (!_master.GetButtonDown("Select") || !_canSelect  || _block != null) return;

        _block = blockSelection.SelectBlock();
        _block.SetActive(true);
        
        _tetrisBlock = _block.GetComponent<TetrisBlock>();
        //_tetrisBlock.player = player;
        _tetrisBlock.InitWheel(wheel, textEffect);
        _tetrisBlock.SpawnInGameWorld();
        _canSelect = false;
        _statusDrop = true;
    }

    private void MoveBlock()
    {
        var blockTransform = _block.transform;
        blockTransform.position += new Vector3(_master.GetAxis("Move Horizontal")*5f, 0f, 0f) * Time.deltaTime;
        blockTransform.position = new Vector2(blockTransform.position.x, parent.position.y);
    }

    private void RotateBlock()
    {
        if (_master.GetButtonDown("Rotate Left"))
        {
            _tetrisBlock.RotateLeft();
        }
        else if (_master.GetButtonDown("Rotate Right"))
        {
            _tetrisBlock.RotateRight();
        }
    }

    private void DropBlock()
    {
        if (!_master.GetButtonDown("Select")) return;

        var rigidBody = _block.GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.None;
        rigidBody.gravityScale = fallSpeed;
        _block = null;

        _statusDrop = false;
        StartCoroutine(UpdateDropTimer());
    }

    private IEnumerator UpdateDropTimer()
    {
        yield return new WaitForSeconds(timeBetweenDrop);

        _canSelect = true;
    }
}
