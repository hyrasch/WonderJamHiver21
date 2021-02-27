using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatBG : MonoBehaviour
{
    public float lenght;

    private float posBG1;
    private float posBG2;


    private void Start()
    {
        posBG1 = -3.19213f;
        posBG2 = 4.599999f;
    }

    private void Update()
    {
        if(transform.position.y > posBG1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    var gameObject = new GameObject();
                    var pos = new Vector3(-12.09f + (j * 2.58f), (posBG1 + 2*lenght) + (i * 2.5899999f ), 4);
                    gameObject.transform.position = pos;
                    var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                    var sprite = Resources.Load<Sprite>("stoneBG");
                    spriteRenderer.sprite = sprite;
                }
            }
            
            posBG1 += 2 * lenght;
        }

        if (transform.position.y > posBG2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    var gameObject = new GameObject();
                    var pos = new Vector3(-12.09f + (j * 2.58f), (posBG2 + 2*lenght) + (i * 2.5899999f ), 4);
                    gameObject.transform.position = pos;
                    var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                    var sprite = Resources.Load<Sprite>("stoneBG");
                    spriteRenderer.sprite = sprite;
                }
            }
            
            posBG2 += 2 * lenght;
        }
    }
}
