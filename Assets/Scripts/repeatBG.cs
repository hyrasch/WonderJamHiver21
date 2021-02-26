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
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + lenght * 2, transform.position.z);
            transform.position = pos;
        }
    }
}
