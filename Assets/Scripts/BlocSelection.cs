using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class BlocSelection : MonoBehaviour
{
    enum SelectorDirection
    {
        Up,
        Down
    }

    [SerializeField] int _visibleBlocks;
    [SerializeField] Vector2 _distanceBetweenBlocks;
    [SerializeField] float _animationTime;

    [SerializeField] GameObject _spriteRendererTemplate;
    [SerializeField] List<GameObject> _blockTemplate = new List<GameObject>(); // Blocks template to instantiate
    List<KeyValuePair<GameObject, GameObject>> _spawnedBlocks;                 //Spawned block

    private Player _master;
    private bool _selectBlock;

    private void Awake() {
        _master = ReInput.players.GetPlayer("Master");
    }


    // Start is called before the first frame update
    void Start() {
        _spawnedBlocks = new List<KeyValuePair<GameObject, GameObject>>();
        SpawnInitialBlocks();
    }

    void SpawnInitialBlocks() {
        for (int i = 0; i < _visibleBlocks; i++) {
            Vector3 eulerRandomRotation = UnityEngine.Random.Range(0, 3) * new Vector3(0, 0, 90);
            Vector2 position = -_distanceBetweenBlocks * i;
            _spawnedBlocks.Add(AddBlock(UnityEngine.Random.Range(0, _blockTemplate.Count), position,
                                        Quaternion.Euler(eulerRandomRotation)));

            UpdateSpawnedBlocksAlpha();
        }
    }

    // Update is called once per frame
    void Update() {
        _selectBlock = _master.GetButtonDown("Select");
        if (!_selectBlock) return;

        GameObject newBlock = SelectBlock();
        Destroy(newBlock);
        //AddBlock(UnityEngine.Random.Range(0,6));
    }

    private void UpdateSpawnedBlocksAlpha() {
        for (var i = 0; i < _spawnedBlocks.Count; i++) {
            var tempColor = _spawnedBlocks[i].Key.GetComponent<Image>().color;
            tempColor.a = 1f - i * .25f;
            _spawnedBlocks[i].Key.GetComponent<Image>().color = tempColor;
        }
    }

    KeyValuePair<GameObject, GameObject> AddBlock(int BlockType, Vector2 Position, Quaternion Rotation) {
        GameObject block = Instantiate(_blockTemplate[BlockType]);
        SpriteRenderer blockRenderer = block.GetComponent<SpriteRenderer>();
        GameObject blockUIRenderer = Instantiate(_spriteRendererTemplate, Vector3.zero, Quaternion.identity, transform);

        blockUIRenderer.GetComponent<RectTransform>().anchoredPosition = Position;
        Image blockUIImage = blockUIRenderer.GetComponent<Image>();

        blockUIImage.sprite = blockRenderer.sprite;
        blockUIImage.preserveAspect = true;

        KeyValuePair<GameObject, GameObject> res = new KeyValuePair<GameObject, GameObject>(blockUIRenderer, block);

        foreach (Transform child in block.transform) {
            child.gameObject.SetActive(false);
        }

        block.SetActive(false);
        return res;
    }

    public GameObject SelectBlock() {
        GameObject selected = _spawnedBlocks[0].Value;
        Destroy(_spawnedBlocks[0].Key);
        _spawnedBlocks.RemoveAt(0);

        if (selected != null) {
            Vector3 eulerRandomRotation = UnityEngine.Random.Range(0, 3) * new Vector3(0, 0, 90);
            Vector3 spawnPos = _spawnedBlocks[_spawnedBlocks.Count - 1].Key.GetComponent<RectTransform>().anchoredPosition -
                               _distanceBetweenBlocks;
            _spawnedBlocks.Add(AddBlock(UnityEngine.Random.Range(0, _blockTemplate.Count), spawnPos,
                                        Quaternion.Euler(eulerRandomRotation)));
            MoveUp();
        }

        UpdateSpawnedBlocksAlpha();
        return selected;
    }

    public void MoveUp() {
        StartCoroutine(MoveEveryBloc(SelectorDirection.Up));
    }

    IEnumerator MoveEveryBloc(SelectorDirection Direction) {
        Vector2 travelDirection = _distanceBetweenBlocks.normalized;
        float distanceToTravel = _distanceBetweenBlocks.magnitude;
        float speed = distanceToTravel / _animationTime;
        float saveDistanceTraveled = 0;


        List<RectTransform> _blockPosition = new List<RectTransform>();
        foreach (KeyValuePair<GameObject, GameObject> Block in _spawnedBlocks) {
            _blockPosition.Add(Block.Key.GetComponent<RectTransform>());
        }

        while (true) {
            float step = distanceToTravel * Time.deltaTime;
            foreach (RectTransform rect in _blockPosition) {
                rect.anchoredPosition += step * travelDirection;
            }

            saveDistanceTraveled += step;
            float remainingDistance = Mathf.Max(0, distanceToTravel - saveDistanceTraveled);
            if (remainingDistance <= 0) {
                break;
            }

            yield return null;
        }

        yield return null;
    }

    float roundToNearest25(float value) {
        return (float) Math.Round(value / 25f) * 25f;
    }
}