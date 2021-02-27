using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    [SerializeField] private TetrisBlockStaticValue staticAttribute;
    [SerializeField] private float wheelSpeed = .005f;
    [SerializeField] private int nbWheelRotation = 25;
    [SerializeField] private float wheelBorder = 192f;
    [SerializeField] private int wheelStep = 4;
    [SerializeField] private int wheelImgSize = 64;

    private TetrisBlockStaticValue.BlockEffect _attachedEffect;
    private SpriteRenderer _spriteComponent;
    private SpriteRenderer _wheel;

    // Start is called before the first frame update
    private void Start()
    {
        InitAttr();
    }

    private void InitAttr()
    {
        if (_spriteComponent != null) return;
        
        _spriteComponent = GetComponent<SpriteRenderer>();
        _attachedEffect = TetrisBlockStaticValue.BlockEffect.Neutral;
    }

    public void RotateRight() {
        transform.Rotate(0,0,90);
    }

    public void RotateLeft() {
        transform.Rotate(0, 0, -90);
    }

    private TetrisBlockStaticValue.BlockEffect ChooseRandomEffect() {
        var population = new List<int>();
        for (var i = 0; i < staticAttribute._effectProbabilty.Count; i++) {
            for (var j = 0; j < 100 * staticAttribute._effectProbabilty[i]; j++) {
                population.Add(i);
            }
        }
        return (TetrisBlockStaticValue.BlockEffect) population[Random.Range(0, staticAttribute._effectProbabilty.Count)];
    }

    public void SpawnInGameWorld() {
        InitAttr();
        
        StartCoroutine(ColorRandomizer());
    }

    public void InitWheel(SpriteRenderer wheel)
    {
        if (_wheel != null) return;
        
        _wheel = wheel;
    }

    private IEnumerator ColorRandomizer()
    {
        for (var i = 0; i < nbWheelRotation; i++)
        {
            if (_wheel != null)
            {
                for (var j = 0; j < wheelStep; j++)
                {
                    var wheelTransform = _wheel.transform;
                    var wheelPosition = wheelTransform.localPosition;
                
                    if (wheelPosition.y <= -wheelBorder)
                    {
                        wheelTransform.localPosition = new Vector2(wheelPosition.x, wheelBorder);
                        wheelPosition = wheelTransform.localPosition;
                    }
                    wheelTransform.localPosition = new Vector2(wheelPosition.x, wheelPosition.y - wheelBorder / wheelStep);
                    yield return new WaitForSeconds(wheelSpeed * i);
                }
            }
            _spriteComponent.color = staticAttribute._effectColor[i % staticAttribute._effectColor.Count];
            yield return new WaitForSeconds(wheelSpeed * i);
        }
        
        _attachedEffect = ChooseRandomEffect();
        _spriteComponent.color = staticAttribute._effectColor[(int) _attachedEffect];

        if (_wheel == null) yield break;
        {
            var wheelTransform = _wheel.transform;
            var wheelPosition = wheelTransform.localPosition;
            
            wheelTransform.localPosition = new Vector2(wheelPosition.x, 192 * -1 - (int) _attachedEffect * wheelImgSize);
        }
    }
}
