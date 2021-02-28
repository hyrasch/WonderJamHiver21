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
            gameOverScreen.setup(0, 1);
            CanvasUI.gameObject.SetActive(false);
            gameUICanvas.gameObject.SetActive(false);
        }
    }
}
