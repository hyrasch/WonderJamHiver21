using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TetrisBlock : MonoBehaviour
{
    TetrisBlockStaticValue.BlockEffect _AttachedEffect;
    SpriteRenderer _SpriteComponent;

    [SerializeField] TetrisBlockStaticValue _StaticAttribute;
    
    public GameObject enemyPrefab;

    private void Start()
    {
        InitAttr();
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -100f);
    }

    private void FixedUpdate()
    {
    }

    private void InitAttr()
    {
        if (_SpriteComponent != null) return;

        _SpriteComponent = GetComponent<SpriteRenderer>();
        _AttachedEffect = TetrisBlockStaticValue.BlockEffect.Neutral;
    }

    public void RotateRight()
    {
        transform.Rotate(0, 0, 90);
    }

    public void RotateLeft()
    {
        transform.Rotate(0, 0, -90);
    }

    private TetrisBlockStaticValue.BlockEffect ChooseRandomEffect()
    {
        var population = new List<int>();
        for (var i = 0; i < _StaticAttribute._effectProbabilty.Count; i++)
        {
            for (var j = 0; j < 100 * _StaticAttribute._effectProbabilty[i]; j++)
            {
                population.Add(i);
            }
        }

        return (TetrisBlockStaticValue.BlockEffect) population[
            Random.Range(0, _StaticAttribute._effectProbabilty.Count)];
    }

    public void SpawnInGameWorld()
    {
        InitAttr();

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
                    Fire(collision);
                    break;
                case TetrisBlockStaticValue.BlockEffect.Ice:
                    Ice();
                    break;
                case TetrisBlockStaticValue.BlockEffect.Explosion:
                    StartCoroutine(WaitForBoom(2.0f));
                    break;
                case TetrisBlockStaticValue.BlockEffect.Enemy:
                    Enemy();
                    break;
                case TetrisBlockStaticValue.BlockEffect.Malus:
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
        
    }
}