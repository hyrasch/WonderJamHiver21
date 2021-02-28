using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public GameObject enemyPrefab;
    public Character2DController player;

    private void Start()
    {
        InitAttr();
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -100f);
    }

    private void InitAttr()
    {
        if (_spriteComponent != null) return;
        
        _spriteComponent = GetComponent<SpriteRenderer>();
        _attachedEffect = TetrisBlockStaticValue.BlockEffect.Neutral;
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
        var rand = Random.value;
        var currentProb = 0f;
        for (var i = 0; i < staticAttribute._effectProbabilty.Count; i++)
        {
            currentProb += staticAttribute._effectProbabilty[i];
            if (rand <= currentProb) 
            {
                return (TetrisBlockStaticValue.BlockEffect) i;
            }
        }
        return (TetrisBlockStaticValue.BlockEffect) (staticAttribute._effectProbabilty.Count - 1);
    }

    public void SpawnInGameWorld()
    {
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

        int tmpScore = 0;
        switch (_attachedEffect)
        {
            case TetrisBlockStaticValue.BlockEffect.Neutral:
                tmpScore++;
                break;
            case TetrisBlockStaticValue.BlockEffect.Enemy:
                tmpScore+=10;
                break;
            case TetrisBlockStaticValue.BlockEffect.Explosion:
                tmpScore+=10;
                break;
            case TetrisBlockStaticValue.BlockEffect.Fire:
                tmpScore+=5;
                break;
            case TetrisBlockStaticValue.BlockEffect.Ice:
                tmpScore+=6;
                break;
            case TetrisBlockStaticValue.BlockEffect.Malus:
                tmpScore+=8;
                break;
        }
        //player.AddToScore(tmpScore);

        if (_wheel == null) yield break;
        {
            var wheelTransform = _wheel.transform;
            var wheelPosition = wheelTransform.localPosition;

            wheelTransform.localPosition = new Vector2(wheelPosition.x, wheelBorder * -1 + (int) _attachedEffect * wheelImgSize);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && gameObject.layer != 8)
        {
            gameObject.layer = 8;
        }

        if (gameObject.layer != 8) return;
        
        switch (_attachedEffect)
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
                    Debug.Log("Bad luck!");
                    Malus();
                    break;
            

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
        var colliders = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach (var hit in colliders)
        {
            var rb = hit.GetComponent<Rigidbody2D>();
            if (rb == null) continue;
            
            if (rb.CompareTag("Block"))
            {
                Destroy(rb.gameObject);
            }
        }
        
        Destroy(gameObject);
    }

    private static void Fire(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        GameObject kingGo = GameObject.Find("/King");
        if (kingGo != null)
            newEnemy.gameObject.GetComponent<Ennemy>().playerTransform = kingGo.transform;
        Destroy((gameObject));
    }

    private void Malus()
    {
        
    }
}