using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameIsEnded = false;
    public GameOverScreen gameOverScreen;
 

    public void endGame()
    {
        if (!gameIsEnded)
        {
            Debug.Log("Game Over");
            gameIsEnded = true;
            gameOverScreen.setup(0,0,"TEST1","TEST2");
            //restart();
        }
    }

    public void restart()
    {
        gameIsEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
