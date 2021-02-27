using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{

    TetrisBlockStaticValue.BlockEffect _AttachedEffect;
    SpriteRenderer _SpriteComponent;

    [SerializeField]
    TetrisBlockStaticValue _StaticAttribute;

    // Start is called before the first frame update
    void Start()
    {
        _SpriteComponent = GetComponent<SpriteRenderer>();
        _AttachedEffect = TetrisBlockStaticValue.BlockEffect.Neutral;
    }

    void RotateRight() {
        transform.Rotate(0,0,90);
    }

    void RotateLeft(){
        transform.Rotate(0, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {}


    TetrisBlockStaticValue.BlockEffect chooseRandomEffect() {
        List<int> population = new List<int>();
        for (int i = 0; i < _StaticAttribute._effectProbabilty.Count; i++) {
            for (int j = 0; j < 100 * _StaticAttribute._effectProbabilty[i]; j++) {
                population.Add(i);
            }
        }
        return (TetrisBlockStaticValue.BlockEffect)population[Random.Range(0, _StaticAttribute._effectProbabilty.Count)];
    }


    public void SpawnInGameWorld() {
        _AttachedEffect = chooseRandomEffect();
        _SpriteComponent.color = _StaticAttribute._effectColor[(int)_AttachedEffect];
    }
}
