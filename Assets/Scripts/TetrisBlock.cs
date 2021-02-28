using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TetrisBlock : MonoBehaviour
{
    [SerializeField] private TetrisBlockStaticValue staticAttribute;
    [SerializeField] private float wheelSpeed = .005f;
    [SerializeField] private int nbWheelRotation = 25;
    [SerializeField] private float wheelBorder = 192f;
    [SerializeField] private int wheelStep = 4;
    [SerializeField] private int wheelImgSize = 64;

    [SerializeField] GameObject _fireParticle;
    [SerializeField] GameObject _explosionParticle;
    [SerializeField] GameObject _malusParticle;
    [SerializeField] int _indexIceSprite;
    [SerializeField] GameObject _dustParticle;

    SpriteRenderer _renderer;
    Sprite _defaultSprite;
    GameObject _systemAttached;
    private TetrisBlockStaticValue.BlockEffect _attachedEffect;
    private SpriteRenderer _spriteComponent;
    private Image _wheel;
    private TextMeshProUGUI _textUnderWheel;
    public GameObject enemyPrefab;

    private readonly List<string> _textEffects = new List<string>
    {
        "Aucun effet...", "Il fait chaud !", "Sol glissant !", "Bim Bam Boum !", "Soldat obscure.", "Pas de chance..."
    };

    private bool isFireSoundPlaying;

    private void Start()
    {
        InitAttr();
        _renderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _renderer.sprite;
        isFireSoundPlaying = false;
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

    public void InitWheel(Image wheel, TextMeshProUGUI text)
    {
        if (_wheel != null) return;

        _wheel = wheel;
        _textUnderWheel = text;
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

                    wheelTransform.localPosition =
                        new Vector2(wheelPosition.x, wheelPosition.y - wheelBorder / wheelStep);
                    yield return new WaitForSeconds(wheelSpeed * i);
                }
            }

            UpdateParticles((TetrisBlockStaticValue.BlockEffect) (i % staticAttribute._effectColor.Count));
            _textUnderWheel.text = _textEffects[i % staticAttribute._effectColor.Count];
            _spriteComponent.color = staticAttribute._effectColor[i % staticAttribute._effectColor.Count];
            yield return new WaitForSeconds(wheelSpeed * i);
        }

        _attachedEffect = ChooseRandomEffect();
        UpdateParticles(_attachedEffect);
        _textUnderWheel.text = _textEffects[(int) _attachedEffect];
        _spriteComponent.color = staticAttribute._effectColor[(int) _attachedEffect];

        GameObject scoreGo = GameObject.Find("/gameUICanvas/timerEtScore");
        if (scoreGo != null)
        {
            switch (_attachedEffect)
            {
                case TetrisBlockStaticValue.BlockEffect.Neutral:
                    if (scoreGo.GetComponent<TimerAndScore>().turnP1)
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP1++;
                    }
                    else
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP2++;
                    }
                    break;
                case TetrisBlockStaticValue.BlockEffect.Enemy:
                    if (scoreGo.GetComponent<TimerAndScore>().turnP1)
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP1+=10;
                    }
                    else
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP2+=10;
                    }
                    break;
                case TetrisBlockStaticValue.BlockEffect.Explosion:
                    if (scoreGo.GetComponent<TimerAndScore>().turnP1)
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP1+=10;
                    }
                    else
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP2+=10;
                    }
                    break;
                case TetrisBlockStaticValue.BlockEffect.Fire:
                    if (scoreGo.GetComponent<TimerAndScore>().turnP1)
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP1+=5;
                    }
                    else
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP2+=5;
                    }
                    break;
                case TetrisBlockStaticValue.BlockEffect.Ice:
                    if (scoreGo.GetComponent<TimerAndScore>().turnP1)
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP1+=6;
                    }
                    else
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP2+=6;
                    }
                    break;
                case TetrisBlockStaticValue.BlockEffect.Malus:
                    if (scoreGo.GetComponent<TimerAndScore>().turnP1)
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP1+=8;
                    }
                    else
                    {
                        scoreGo.GetComponent<TimerAndScore>().scoreP2+=8;
                    }
                    break;
            }
        }
        //player.AddToScore(tmpScore);

        if (_wheel == null) yield break;
        {
            var wheelTransform = _wheel.transform;
            var wheelPosition = wheelTransform.localPosition;

            wheelTransform.localPosition =
                new Vector2(wheelPosition.x, wheelBorder * -1 + (int) _attachedEffect * wheelImgSize);
        }
    }

    private void UpdateParticles(TetrisBlockStaticValue.BlockEffect blockEffect)
    {
        DisableParticles();
        // TODO : Add all effects
        // TODO : Active explosion when grounded ?
        switch (blockEffect)
        {
            case TetrisBlockStaticValue.BlockEffect.Neutral:

                break;
            case TetrisBlockStaticValue.BlockEffect.Fire:
                _fireParticle.SetActive(true);
                break;
            case TetrisBlockStaticValue.BlockEffect.Ice:
                _renderer.sprite = staticAttribute._iceSprites[_indexIceSprite]; 
                break;
            case TetrisBlockStaticValue.BlockEffect.Explosion:
                _systemAttached = _explosionParticle;
                break;
            case TetrisBlockStaticValue.BlockEffect.Enemy:

                break;
            case TetrisBlockStaticValue.BlockEffect.Malus:
                _systemAttached = _malusParticle;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(blockEffect), blockEffect, null);
        }
    }

    private void DisableParticles()
    {
        _renderer.sprite = _defaultSprite;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            switch (_attachedEffect)
            {
                case TetrisBlockStaticValue.BlockEffect.Fire:
                    Debug.Log("ça brule");
                    Fire(collider);
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && gameObject.layer != 8)
        {
            gameObject.layer = 8;
           /* GameObject dust = Instantiate(_dustParticle.gameObject,transform);
            dust.GetComponent<ParticleSystem>().Play();
            StartCoroutine(DestroyParticle(dust, 1.5f));*/
        }

        if (gameObject.layer != 8) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (_attachedEffect)
            {
                case TetrisBlockStaticValue.BlockEffect.Neutral:

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
            }
        }

        if (gameObject.layer == 8)
        {
            switch (_attachedEffect)
            {
                case TetrisBlockStaticValue.BlockEffect.Explosion:
                    Debug.Log("Tic Tac");
                    StartCoroutine(WaitForBoom(2.0f));
                    break;
                case TetrisBlockStaticValue.BlockEffect.Enemy:
                    Debug.Log("Be careful");
                    Enemy();
                    break;
            }
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

        GameObject gameObjectParticle = Instantiate(_systemAttached,_systemAttached.transform.position,_systemAttached.transform.rotation, null);
        ParticleSystem particles = gameObjectParticle.GetComponent<ParticleSystem>();
        gameObjectParticle.SetActive(true);
        ParticleSystem.ShapeModule shape = particles.shape;
        shape.scale *= 2;
        particles.Play();
        StartCoroutine(DestroyParticle(gameObjectParticle,1.5f));

        foreach (var hit in colliders)
        {
            var rb = hit.GetComponent<Rigidbody2D>();
            if (rb == null) continue;

            if (rb.CompareTag("Block"))
            {
                Destroy(rb.gameObject);
            }

            if (rb.CompareTag("Player"))
            {
                rb.gameObject.GetComponent<Character2DController>().diminishHealth(.5f);
            }
        }

        FindObjectOfType<AudioManager>().Play("Explosion");

        Destroy(gameObject);
    }

    IEnumerator DestroyParticle(GameObject particle, float time)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);
        Destroy(particle);
    }

    private void Fire(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Character2DController>().diminishHealth(0.005f);
        }

        if (!isFireSoundPlaying)
        {
            isFireSoundPlaying = true;
            FindObjectOfType<AudioManager>().Play("Fire");

            StartCoroutine(WaitForFireSoundEnd());
        }
    }

    private IEnumerator WaitForFireSoundEnd()
    {
        yield return new WaitForSeconds(0.340f);
        isFireSoundPlaying = false;
    }

    private void Ice()
    {
        //gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 0;
    }

    private void Enemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y, 0),
            Quaternion.identity);
        GameObject kingGo = GameObject.Find("/King");
        if (kingGo != null)
            newEnemy.gameObject.GetComponent<Ennemy>().playerTransform = kingGo.transform;
        Destroy((gameObject));

        FindObjectOfType<AudioManager>().Play("Enemy");
    }

    private void Malus()
    {
        Debug.Log("Malus");
        ParticleSystem particles = _systemAttached.GetComponent<ParticleSystem>();
        _systemAttached.SetActive(true);
        particles.Play();

        GameObject timerGo = GameObject.Find("/gameUICanvas/timerEtScore");
        if (timerGo != null)
            timerGo.GetComponent<TimerAndScore>().timeRemaining -= 10;

        FindObjectOfType<AudioManager>().Play("Malus");
    }
}