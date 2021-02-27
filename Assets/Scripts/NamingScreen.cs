using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NamingScreen : MonoBehaviour
{
    public Text firstLetter;
    public Text secondLetter;
    public Text thirdLetter;
    public Text titleText;
    int numberOfPlayerNamed=0;
    // Start is called before the first frame update
    public void setup()
    {
        gameObject.SetActive(true);
    }

    public void upButton(int index)
    {
        Text currentText = switchLetter(index);
        string current = currentText.text;
        char next =--current.ToCharArray()[0];
        if(next <65)
        {
            next = 'Z';
        }
        currentText.text = next.ToString();
    }

    public void downButton(int index)
    {
        Text currentText = switchLetter(index);
        string current = currentText.text;
        char next = ++current.ToCharArray()[0];
        if (next > 90)
        {
            next = 'A';
        }
        currentText.text = next.ToString();
    }


    public void acceptButton()
    {
        numberOfPlayerNamed++;
        //save name in gameManager in fonction of numberOfPlayerNamed
        if (numberOfPlayerNamed<2) //si les deux joueurs n'ont pas été nommé on recharge l'écran pour donner un second nom.
        {
            resetName();
            titleText.text = "Choose player 2 name";
            titleText.color = Color.blue;
            Debug.Log(numberOfPlayerNamed);
        } else
        {
            //Go to nextScreen
            Debug.Log("fin");
        }
    }

    private void resetName()
    {
        firstLetter.text = "A";
        secondLetter.text = "A";
        thirdLetter.text = "A";
    }

    private Text switchLetter(int index)
    {
        switch (index)
        {
            case 1: return firstLetter;
            case 2: return secondLetter; 
            case 3: return thirdLetter; 
            default: return firstLetter;  //juste pour éviter erreur variable non assignée
        }
    }
}
