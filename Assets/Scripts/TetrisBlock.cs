using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{

    TetrisBlockStaticValue.BlockEffect _AttachedEffect;
    SpriteRenderer _SpriteComponent;

    [SerializeField]
    TetrisBlockStaticValue _StaticAttribute;

    // Start is called before the first frame update
    private void Start()
    {
        InitAttr();
    }

    private void InitAttr()
    {
        if (_SpriteComponent != null) return;
        
        _SpriteComponent = GetComponent<SpriteRenderer>();
        _AttachedEffect = TetrisBlockStaticValue.BlockEffect.Neutral;
    }

    public void RotateRight() {
        transform.Rotate(0,0,90);
    }

    public void RotateLeft() {
        transform.Rotate(0, 0, -90);
    }

    private TetrisBlockStaticValue.BlockEffect ChooseRandomEffect() {
        var population = new List<int>();
        for (var i = 0; i < _StaticAttribute._effectProbabilty.Count; i++) {
            for (var j = 0; j < 100 * _StaticAttribute._effectProbabilty[i]; j++) {
                population.Add(i);
            }
        }
        return (TetrisBlockStaticValue.BlockEffect) population[Random.Range(0, _StaticAttribute._effectProbabilty.Count)];
    }

    public void SpawnInGameWorld() {
        InitAttr();
        
        _AttachedEffect = ChooseRandomEffect();
        _SpriteComponent.color = _StaticAttribute._effectColor[(int)_AttachedEffect];
    }
}
