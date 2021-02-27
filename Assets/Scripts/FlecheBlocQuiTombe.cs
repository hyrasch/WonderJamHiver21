using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlecheBlocQuiTombe : MonoBehaviour
{
    [SerializeField]
    float tailleDesBordures;

    [SerializeField]
    int largeurAreneEnBloc;

    [SerializeField]
    float tailleArene;

    float pasDeDeplacement;

    int indexLane;

    // Start is called before the first frame update
    void Start()
    {
        pasDeDeplacement = (tailleArene) / (float)largeurAreneEnBloc;
        indexLane = 0;
        var tmpPos = GetComponent<RectTransform>().anchoredPosition;
        tmpPos.x = pasDeDeplacement * indexLane + tailleDesBordures + pasDeDeplacement / 2f;
        GetComponent<RectTransform>().anchoredPosition = tmpPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))  //TODO: à enlever, ici pour des tests
        {
            moveLeft();
        }
        if (Input.GetKeyDown("d"))  //TODO: à enlever, ici pour des tests
        {
            moveRight();
        }
    }

    void moveLeft()
    {
        if (indexLane == 0) return;
        indexLane--;
        var tmpPos = GetComponent<RectTransform>().anchoredPosition;
        tmpPos.x -= pasDeDeplacement;
        GetComponent<RectTransform>().anchoredPosition = tmpPos;
    }

    void moveRight()
    {
        if (indexLane == largeurAreneEnBloc - 1) return;
        indexLane++;
        var tmpPos = GetComponent<RectTransform>().anchoredPosition;
        tmpPos.x += pasDeDeplacement;
        GetComponent<RectTransform>().anchoredPosition = tmpPos;
    }
}
