using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameIsEnded = false;
    public void endGame()
    {
        if (!gameIsEnded)
        {
            Debug.Log("Game Over");
            gameIsEnded = true;
            restart();
        }
    }

    public void restart()
    {
        gameIsEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
