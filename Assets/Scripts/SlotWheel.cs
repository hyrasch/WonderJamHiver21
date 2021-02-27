using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotWheel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            if (transform.position.y <= 410)
            {
                transform.position = new Vector2(transform.position.x, 405);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
