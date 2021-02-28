using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PostGameUIManager : MonoBehaviour
{
    bool gameIsEnded = false;
    public GameOverScreen gameOverScreen;
    public Canvas gameUICanvas;

    public void endGame()
    {
        BlockDrop bd = FindObjectOfType<BlockDrop>();
        TimerAndScore ts = FindObjectOfType<TimerAndScore>();
        if (!gameIsEnded && !bd.turnP1)
        {
            Debug.Log("Game Over");
            gameIsEnded = true;

            gameUICanvas.gameObject.SetActive(false);
            gameOverScreen.setup(ts.scoreP2, ts.scoreP1);
        }
        else
        {
            bd.setTurnP2();
            ts.setTurn2();

            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
            foreach (GameObject block in blocks)
            {
                Destroy(block);
            }
        }
    }
}