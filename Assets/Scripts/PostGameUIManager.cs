using Rewired;
using UnityEngine;

public class PostGameUIManager : MonoBehaviour
{
    public bool gameIsEnded;
    public GameOverScreen gameOverScreen;
    public Canvas gameUICanvas;

    private Player _master;
    private Player _runner;

    private void Awake() {
        _master = ReInput.players.GetPlayer("Master");
        _runner = ReInput.players.GetPlayer("Runner");
    }

    public void EndGame() {
        BlockDrop bd = FindObjectOfType<BlockDrop>();
        TimerAndScore ts = FindObjectOfType<TimerAndScore>();

        if (!gameIsEnded && !bd.turnP1) {
            gameIsEnded = true;

            _master.isPlaying = false;
            _runner.isPlaying = false;

            gameUICanvas.gameObject.SetActive(false);
            gameOverScreen.Setup(ts.scoreP2, ts.scoreP1);
        }
        else {
            bd.setTurnP2();
            ts.setTurn2();

            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
            foreach (GameObject block in blocks) {
                Destroy(block);
            }
        }
    }
}