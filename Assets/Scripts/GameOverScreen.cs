using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text player1Score;
    public Text player2Score;
    public Text winnerName;

    public NamingScreen namingScreen;

    private Player _system;

    private int _scorePlayer1;
    private int _scorePlayer2;

    private void Awake() {
        _system = ReInput.players.GetPlayer("System");
    }

    private void Update() {
        if (!_system.GetAnyButtonDown()) return;

        gameObject.SetActive(false);
        namingScreen.Setup(_scorePlayer1, _scorePlayer2);
    }

    public void Setup(int score1, int score2) {
        _scorePlayer1 = score1;
        _scorePlayer2 = score2;
        player1Score.text = score1 + " PTS";
        player2Score.text = score2 + " PTS";
        winnerName.text = (score1 > score2 ? "PLAYER 1" : "PLAYER 2") + " A GAGNE !";
        gameObject.SetActive(true);
    }
}