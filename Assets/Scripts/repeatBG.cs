using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatBG : MonoBehaviour
{
    public float lenght;
    public float columnlenght;

    private float posBG1;
    private float posBG2;

    private float columnPosBG1;
    private float columnPosBG2;


    private void Start()
    {
        posBG1 = -23.780001f;
        posBG2 = -16f;

        columnPosBG1 = 5.29f;
        columnPosBG2 = 11.179f;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var gameObject = new GameObject();
                var pos = new Vector3(-10f + (j * lenght / 3), (posBG1 + 2 * lenght) + (i * lenght / 3), 4);
                gameObject.transform.position = pos;
                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                var sprite = Resources.Load<Sprite>("brickBG");
                spriteRenderer.sprite = sprite;
            }
        }

        posBG1 += 2 * lenght;


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var gameObject = new GameObject();
                var pos = new Vector3(-10f + (j * lenght / 3), (posBG2 + 2 * lenght) + (i * lenght / 3), 4);
                gameObject.transform.position = pos;
                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                var sprite = Resources.Load<Sprite>("brickBG");
                spriteRenderer.sprite = sprite;
            }
        }

        posBG2 += 2 * lenght;

        for (int i = 0; i < 3; i++)
        {
            var gameObject = new GameObject();
            
            var pos = new Vector3(-3.374f, (columnPosBG1 + 2 * columnlenght) + (i * columnlenght / 3), 3);
            gameObject.transform.position = pos;
            gameObject.transform.localScale = 
                new Vector2(gameObject.transform.localScale.x * 0.82f, gameObject.transform.localScale.y * 0.82f);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("column2");
            spriteRenderer.sprite = sprite;
            
            BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.91f, 2.4f);
        }

        for (int i = 0; i < 3; i++)
        {
            var gameObject = new GameObject();
            
            var pos = new Vector3(3.374f, (columnPosBG1 + 2 * columnlenght) + (i * columnlenght / 3), 3);
            gameObject.transform.position = pos;
            gameObject.transform.localScale = 
                new Vector2(gameObject.transform.localScale.x * -0.82f, gameObject.transform.localScale.y * 0.82f);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("column2");
            spriteRenderer.sprite = sprite;
            
            BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.91f, 2.4f);
        }

        columnPosBG1 += 2 * columnlenght;
        
        for (int i = 0; i < 3; i++)
        {
            var gameObject = new GameObject();
            
            var pos = new Vector3(-3.374f, (columnPosBG2 + 2 * columnlenght) + (i * columnlenght / 3), 3);
            gameObject.transform.position = pos;
            gameObject.transform.localScale = 
                new Vector2(gameObject.transform.localScale.x * 0.82f, gameObject.transform.localScale.y * 0.82f);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("column2");
            spriteRenderer.sprite = sprite;
            
            BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.91f, 2.4f);
        }

        for (int i = 0; i < 3; i++)
        {
            var gameObject = new GameObject();
            
            var pos = new Vector3(3.374f, (columnPosBG2 + 2 * columnlenght) + (i * columnlenght / 3), 3);
            gameObject.transform.position = pos;
            gameObject.transform.localScale = 
                new Vector2(gameObject.transform.localScale.x * -0.82f, gameObject.transform.localScale.y * 0.82f);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("column2");
            spriteRenderer.sprite = sprite;
            
            BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.91f, 2.4f);
        }

        columnPosBG2 += 2 * columnlenght;
    }

    private void Update()
    {
        if (transform.position.y > posBG1)
        {
            BG1();
        }

        if (transform.position.y > posBG2)
        {
            BG2();
        }

        if (transform.position.y > columnPosBG1)
        {
            Column1();
        }
        
        if (transform.position.y > columnPosBG2)
        {
            Column2();
        }
    }

    private void BG1()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var gameObject = new GameObject();
                var pos = new Vector3(-10f + (j * 2.59f), (posBG1 + 2 * lenght) + (i * 2.59f), 4);
                gameObject.transform.position = pos;
                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                var sprite = Resources.Load<Sprite>("brickBG");
                spriteRenderer.sprite = sprite;
            }
        }

        posBG1 += 2 * lenght;
    }
    
    private void BG2()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var gameObject = new GameObject();
                var pos = new Vector3(-10f + (j * 2.59f), (posBG2 + 2 * lenght) + (i * 2.59f), 4);
                gameObject.transform.position = pos;
                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                var sprite = Resources.Load<Sprite>("brickBG");
                spriteRenderer.sprite = sprite;
            }
        }

        posBG2 += 2 * lenght;
    }

    private void Column1()
    {
        for (int i = 0; i < 3; i++)
        {
            var gameObject = new GameObject();
            
            var pos = new Vector3(-3.374f, (columnPosBG1 + 2 * columnlenght) + (i * columnlenght / 3), 3);
            gameObject.transform.position = pos;
            gameObject.transform.localScale = 
                new Vector2(gameObject.transform.localScale.x * 0.82f, gameObject.transform.localScale.y * 0.82f);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("column2");
            spriteRenderer.sprite = sprite;
            
            BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.91f, 2.4f);
        }

        for (int i = 0; i < 3; i++)
        {
            var gameObject = new GameObject();
            
            var pos = new Vector3(3.374f, (columnPosBG1 + 2 * columnlenght) + (i * columnlenght / 3), 3);
            gameObject.transform.position = pos;
            gameObject.transform.localScale = 
                new Vector2(gameObject.transform.localScale.x * -0.82f, gameObject.transform.localScale.y * 0.82f);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("column2");
            spriteRenderer.sprite = sprite;
            
            BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.91f, 2.4f);
        }

        columnPosBG1 += 2 * columnlenght;
        
        
    }

    private void Column2()
    {
        for (int i = 0; i < 3; i++)
        {
            var gameObject = new GameObject();
            
            var pos = new Vector3(-3.374f, (columnPosBG2 + 2 * columnlenght) + (i * columnlenght / 3), 3);
            gameObject.transform.position = pos;
            gameObject.transform.localScale = 
                new Vector2(gameObject.transform.localScale.x * 0.82f, gameObject.transform.localScale.y * 0.82f);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("column2");
            spriteRenderer.sprite = sprite;
            
            BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.91f, 2.4f);
        }

        for (int i = 0; i < 3; i++)
        {
            var gameObject = new GameObject();
            
            var pos = new Vector3(3.374f, (columnPosBG2 + 2 * columnlenght) + (i * columnlenght / 3), 3);
            gameObject.transform.position = pos;
            gameObject.transform.localScale = 
                new Vector2(gameObject.transform.localScale.x * -0.82f, gameObject.transform.localScale.y * 0.82f);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var sprite = Resources.Load<Sprite>("column2");
            spriteRenderer.sprite = sprite;
            
            BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.91f, 2.4f);
        }

        columnPosBG2 += 2 * columnlenght;
    }
}