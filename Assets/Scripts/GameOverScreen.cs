using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverScreen : MonoBehaviour
{

    public Text Player1Score;
    public Text Player2Score;

    public Text WinnerName;

    public NamingScreen namingScreen;
    int scorePlayer1;
    int scorePlayer2;
    public void setup(int score1,int score2)
    {
        scorePlayer1 = score1;
        scorePlayer2 = score2;
        Player1Score.text = score1.ToString()+" PTS";
        Player2Score.text = score2.ToString() + " PTS";
        WinnerName.text = (score1 > score2 ? "PLAYER 1" : "PLAYER2")+ " WINS !";
        gameObject.SetActive(true);
    }

    public void continueButton()
    {
        //this.gameObject.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Passer aux écrans de noms
        gameObject.SetActive(false);
        namingScreen.setup(scorePlayer1, scorePlayer2);
    }

}
