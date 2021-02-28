<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
=======
﻿using System.Collections.Generic;
using System.Collections;
>>>>>>> c6e772cdb721fb33bb9c9ac96630d5b7d43fdf15
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TetrisBlock : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private TetrisBlockStaticValue staticAttribute;
    [SerializeField] private float wheelSpeed = .005f;
    [SerializeField] private int nbWheelRotation = 25;
    [SerializeField] private float wheelBorder = 192f;
    [SerializeField] private int wheelStep = 4;
    [SerializeField] private int wheelImgSize = 64;

    private TetrisBlockStaticValue.BlockEffect _attachedEffect;
    private SpriteRenderer _spriteComponent;
    private SpriteRenderer _wheel;
=======
    TetrisBlockStaticValue.BlockEffect _AttachedEffect;
    SpriteRenderer _SpriteComponent;

    [SerializeField] TetrisBlockStaticValue _StaticAttribute;
    
    public GameObject enemyPrefab;
>>>>>>> c6e772cdb721fb33bb9c9ac96630d5b7d43fdf15

    private void Start()
    {
        InitAttr();
    }

    private void FixedUpdate()
    {
    }

    private void InitAttr()
    {
<<<<<<< HEAD
        if (_spriteComponent != null) return;
        
        _spriteComponent = GetComponent<SpriteRenderer>();
        _attachedEffect = TetrisBlockStaticValue.BlockEffect.Neutral;
=======
        if (_SpriteComponent != null) return;

        _SpriteComponent = GetComponent<SpriteRenderer>();
        _AttachedEffect = TetrisBlockStaticValue.BlockEffect.Neutral;
>>>>>>> c6e772cdb721fb33bb9c9ac96630d5b7d43fdf15
    }

    public void RotateRight()
    {
        transform.Rotate(0, 0, 90);
    }

    public void RotateLeft()
    {
        transform.Rotate(0, 0, -90);
    }

<<<<<<< HEAD
    private TetrisBlockStaticValue.BlockEffect ChooseRandomEffect() {
        var population = new List<int>();
        for (var i = 0; i < staticAttribute._effectProbabilty.Count; i++) {
            for (var j = 0; j < 100 * staticAttribute._effectProbabilty[i]; j++) {
                population.Add(i);
            }
        }
        return (TetrisBlockStaticValue.BlockEffect) population[Random.Range(0, staticAttribute._effectProbabilty.Count)];
=======
    private TetrisBlockStaticValue.BlockEffect ChooseRandomEffect()
    {
        float rand = Random.value;
        float currentProba = 0;
        for (var i = 0; i < _StaticAttribute._effectProbabilty.Count; i++){
            currentProba += _StaticAttribute._effectProbabilty[i];
            if (rand <= currentProba) {
                return (TetrisBlockStaticValue.BlockEffect)i;
            }
        }
        return (TetrisBlockStaticValue.BlockEffect)(_StaticAttribute._effectProbabilty.Count - 1);
>>>>>>> c6e772cdb721fb33bb9c9ac96630d5b7d43fdf15
    }

    public void SpawnInGameWorld()
    {
        InitAttr();
<<<<<<< HEAD
        
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
=======

        _AttachedEffect = ChooseRandomEffect();
        _SpriteComponent.color = _StaticAttribute._effectColor[(int) _AttachedEffect];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && gameObject.layer != 8)
        {
            gameObject.layer = 8;
        }

        if (gameObject.layer == 8)
        {
            switch (_AttachedEffect)
            {
                case TetrisBlockStaticValue.BlockEffect.Neutral:

                    break;
                case TetrisBlockStaticValue.BlockEffect.Fire:
                    Debug.Log("ça brule");
                    Fire(collision);
                    break;
                case TetrisBlockStaticValue.BlockEffect.Ice:
                    Debug.Log("Glagla");
                    Ice();
                    break;
                case TetrisBlockStaticValue.BlockEffect.Explosion:
                    Debug.Log("Tic Tac");
                    StartCoroutine(WaitForBoom(2.0f));
                    break;
                case TetrisBlockStaticValue.BlockEffect.Enemy:
                    Debug.Log("Be careful");
                    Enemy();
                    break;
                case TetrisBlockStaticValue.BlockEffect.Malus:
                    Debug.Log("Lucky you!");
                    Malus();
                    break;
            }

            //gameObject.GetComponent<Rigidbody2D>().isKinematic = true; A débattre
        }
    }

    private IEnumerator WaitForBoom(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Boom();
    }

    private void Boom()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (rb.tag == "Block")
                {
                    Destroy(rb.gameObject);
                }
            }
        }
    }

    private void Fire(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Character2DController>().health -= 3;
        }
    }

    private void Ice()
    {
        gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 0;
    }

    private void Enemy()
    {
        Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy((gameObject));
    }

    private void Malus()
    {
        
>>>>>>> c6e772cdb721fb33bb9c9ac96630d5b7d43fdf15
    }
}