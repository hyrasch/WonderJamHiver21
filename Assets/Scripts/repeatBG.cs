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
        posBG1 = -1.34f;
        posBG2 = 11.18f;
    }

    private void Update()
    {
        if(transform.position.y > posBG1)
        {
            var gameObject = new GameObject();
            var pos = new Vector3(-4.64f, posBG1 + 2*lenght, 4);
            gameObject.transform.localScale = new Vector3(1.575756f, 1.575756f, 1.575756f);
            gameObject.transform.position = pos;
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("castleBG");
            spriteRenderer.sprite = sprite;
            posBG1 += 2 * lenght;
        }

        if (transform.position.y > posBG2)
        {
            var gameObject = new GameObject();
            var pos = new Vector3(-4.64f, posBG2 + 2*lenght, 4);
            gameObject.transform.localScale = new Vector3(1.575756f, 1.575756f, 1.575756f);
            gameObject.transform.position = pos;
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("castleBG");
            spriteRenderer.sprite = sprite;
            posBG2 +=  2 *lenght;
        }
    }
}
