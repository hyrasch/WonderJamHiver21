using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarreDeVie : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Runner;
    private GameObject GoBarre { get; set; }
    private float vie;
    public float vieValue 
    { 
        get { return vie; }
        set {
            if (value > 1f) value = 1f;
            else if (value < 0f) value = 0f;
            vie = value;
            GoBarre.GetComponent<Image>().fillAmount = vie;
        }
    }

    void Start()
    {
        GoBarre = gameObject.transform.GetChild(2).gameObject;
        vieValue = Runner.gameObject.GetComponent<Character2DController>().health;
        vie = vieValue;

    }

    private void Update()
    {
        vieValue = Runner.gameObject.GetComponent<Character2DController>().health;
        vie = vieValue;
    }
}
