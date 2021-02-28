using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NamingScreen : MonoBehaviour
{
    public Text firstLetter;
    public Text secondLetter;
    public Text thirdLetter;
    public Text titleText;
    private int scorePlayer1;
    private int scorePlayer2;
    int numberOfPlayerNamed=0;
    Score score;

    public HighScoreTable highScoreTable;
    // Start is called before the first frame update
    public void setup(int score1,int score2)
    {
        scorePlayer1 = score1;
        scorePlayer2 = score2;
        gameObject.SetActive(true);
        score = new Score();
        score.LoadScore();
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
        score.AddScore(string.Concat(firstLetter.text, secondLetter.text, thirdLetter.text), numberOfPlayerNamed == 1 ? scorePlayer1  : scorePlayer2);
        score.SaveScore();
        //save name in gameManager in fonction of numberOfPlayerNamed
        if (numberOfPlayerNamed<2) //si les deux joueurs n'ont pas été nommé on recharge l'écran pour donner un second nom.
        {

            resetName();
            titleText.text = "Choose player 2 name";
            titleText.color = Color.blue;
            transform.Find("GoToHighScore").gameObject.SetActive(true);
            transform.Find("Retry").gameObject.SetActive(true);
            transform.Find("Accept").gameObject.SetActive(false);

        }
    }

    public void goToScoreBoard()
    {
        gameObject.SetActive(false);
        highScoreTable.display();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
