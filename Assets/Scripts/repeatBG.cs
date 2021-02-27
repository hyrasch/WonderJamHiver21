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
        posBG1 = -3.82f;
        posBG2 = 3.960001f;
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
                    var pos = new Vector3(-7.49f + (j * 2.59f), (posBG1 + 2*lenght) + (i * 2.59f ), 4);
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
                    var pos = new Vector3(-7.49f + (j * 2.59f), (posBG2 + 2*lenght) + (i * 2.59f ), 4);
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
