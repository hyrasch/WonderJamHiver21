using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverScreen : MonoBehaviour
{

    public Text Player1Score;
    public Text Player2Score;

    public Text Player1Name;
    public Text Player2Name;

    public void setup(int score1,int score2,string name1,string name2)
    {
        Player1Score.text = score1.ToString()+" PTS";
        Player2Score.text = score2.ToString() + " PTS";
        Player1Name.text = name1;
        Player2Name.text = name2;
        gameObject.SetActive(true);
    }

    public void restartButton()
    {
        this.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitButton()
    {
        //SceneManager.LoadScene("MainMenu");
    }
}
