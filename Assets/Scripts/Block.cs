using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    private Rigidbody2D rb;
    private Vector2 screenBound;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        //a enlever quand y aura collision entre les blocks
        if(transform.position.y<-2*screenBound.y)
        {
            Destroy(this.gameObject);
        }
    }
}
