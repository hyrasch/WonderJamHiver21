using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatBG : MonoBehaviour
{
    public float lenght;

    public Transform playerTransform;

    private void Update()
    {
        if(playerTransform.position.y - gameObject.transform.position.y > lenght)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y + lenght * 2);
            transform.position = pos;
        }
    }
}
