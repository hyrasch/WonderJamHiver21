using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PostGameUIManager : MonoBehaviour
{
    bool gameIsEnded = false;
    public GameOverScreen gameOverScreen;
    public Canvas CanvasUI;
    public Canvas gameUICanvas;

    public void endGame()
    {
        if (!gameIsEnded)
        {
            Debug.Log("Game Over");
            gameIsEnded = true;
            TimerAndScore ts = FindObjectOfType<TimerAndScore>();
            CanvasUI.gameObject.SetActive(false);
            gameUICanvas.gameObject.SetActive(false);
            gameOverScreen.setup(ts.scoreP1, ts.scoreP2);

        }
    }
}
