using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlocSelection : MonoBehaviour
{

    [SerializeField]
    private GameObject[] prochainsBlocs;    //List contenant les 4 images utilisées pour prévisualiser les blocs qui arriveront
    private int indexFirst; //Variable servant à déterminer l'index de l'image se situant le plus en haut

    [SerializeField]
    private GameObject keyBlock;
    [SerializeField]
    private GameObject lBlock;
    [SerializeField]
    private GameObject longBlock;
    [SerializeField]
    private GameObject squareBlock;
    [SerializeField]
    private GameObject stairBlock;
    [SerializeField]
    private GameObject stairBlockF;

    // Start is called before the first frame update
    void Start()
    {
        indexFirst = 0;
        prochainsBlocs[GetLastIndex()].GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("f"))  //TODO: à enlever, c'est là pour les tests
        {
            addBloc(UnityEngine.Random.Range(0,6), UnityEngine.Random.Range(0, 5));
        }
    }

    public void addBloc(int type, int effect)
    {
        Sprite spriteOfBloc = null;
        Color colorOfBloc = new Color();

        switch (type)
        {
            /*case 0:
                spriteOfBloc = keyBlock;
                break;
            case 1:
                spriteOfBloc = lBlock;
                break;
            case 2:
                spriteOfBloc = longBlock;
                break;
            case 3:
                spriteOfBloc = squareBlock;
                break;
            case 4:
                spriteOfBloc = stairBlock;
                break;
            case 5:
                spriteOfBloc = stairBlockF;
                break;
            default:
                throw new Exception("Erreur: type de bloc invalide");
        }*/
        switch (effect)
        {
            case 0:
                colorOfBloc = new Color(1f, 0.5f, 0.1f);  //Vaguement orange
                break;
            case 1:
                colorOfBloc = Color.red;
                break;
            case 2:
                colorOfBloc = Color.blue;
                break;
            case 3:
                colorOfBloc = Color.green;
                break;
            case 4:
                colorOfBloc = Color.yellow;
                break;
            default:
                throw new Exception("Erreur: effet de bloc invalide");
        }

        prochainsBlocs[GetLastIndex()].GetComponent<Image>().sprite = spriteOfBloc;
        prochainsBlocs[GetLastIndex()].GetComponent<Image>().color = colorOfBloc;
        StartCoroutine(MoveEveryBloc());
    }

    void IncrementIndexFirst()
    {
        if (indexFirst == 3)
            indexFirst = 0;
        else indexFirst++;
    }

    int GetLastIndex()
    {
        if (indexFirst == 0)
            return 3;
        else return indexFirst - 1;
    }

    IEnumerator MoveEveryBloc()
    {
        var totalTime=0f;
        while(totalTime<1f)
        {
            var dt = Time.deltaTime;
            totalTime += dt;
            var colorFirst = prochainsBlocs[indexFirst].GetComponent<Image>().color;
            var colorLast = prochainsBlocs[GetLastIndex()].GetComponent<Image>().color;

            colorFirst.a = 1f-totalTime;
            colorLast.a = totalTime;

            // le premier bloc disparait
            prochainsBlocs[indexFirst].GetComponent<Image>().color = colorFirst;

            // le dernier bloc apparait
            prochainsBlocs[GetLastIndex()].GetComponent<Image>().color = colorLast;
            
            //On déplace tout les blocs
            foreach(var bloc in prochainsBlocs)
            {
                bloc.GetComponent<RectTransform>().anchoredPosition = new Vector2(bloc.GetComponent<RectTransform>().anchoredPosition.x, bloc.GetComponent<RectTransform>().anchoredPosition.y+dt*150); ;
            }
            yield return null;
        }
        var colorFirstEnd = prochainsBlocs[indexFirst].GetComponent<Image>().color;
        var colorLastEnd = prochainsBlocs[GetLastIndex()].GetComponent<Image>().color;

        colorFirstEnd.a = 0;
        colorLastEnd.a = 1f;

        // le premier bloc disparait
        prochainsBlocs[indexFirst].GetComponent<Image>().color = colorFirstEnd;

        // le dernier bloc apparait
        prochainsBlocs[GetLastIndex()].GetComponent<Image>().color = colorLastEnd;


        foreach (var bloc in prochainsBlocs)
        {
            Debug.Log(bloc.GetComponent<RectTransform>().anchoredPosition.y %25);
            bloc.GetComponent<RectTransform>().anchoredPosition = new Vector2(bloc.GetComponent<RectTransform>().anchoredPosition.x, roundToNearest25(bloc.GetComponent<RectTransform>().anchoredPosition.y));
        }

        var tmpPos = prochainsBlocs[indexFirst].GetComponent<RectTransform>().anchoredPosition;
        prochainsBlocs[indexFirst].GetComponent<RectTransform>().anchoredPosition = new Vector2(tmpPos.x, tmpPos.y - 600);
        IncrementIndexFirst();
    }

    float roundToNearest25(float value)
    {
        return (float)(Math.Round(value / 25f)) * 25f;
    }

}
