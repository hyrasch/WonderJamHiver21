using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NamingScreen : MonoBehaviour
{
    public Text firstLetter;
    public Text secondLetter;
    public Text thirdLetter;
    public Text titleText;
    public GameObject firstSelect;

    public HighScoreTable highScoreTable;

    private Player _system;

    private int _scorePlayer1;
    private int _scorePlayer2;
    private int _numberOfPlayerNamed;
    private Score _score;

    private void Awake() {
        _system = ReInput.players.GetPlayer("System");
    }

    // Start is called before the first frame update
    public void Setup(int score1, int score2) {
        _scorePlayer1 = score1;
        _scorePlayer2 = score2;
        gameObject.SetActive(true);
        _score = new Score();
        _score.LoadScore();
        EventSystem.current.SetSelectedGameObject(firstSelect);
    }

    public void UpButton(int index) {
        var currentText = SwitchLetter(index);
        var current = currentText.text;
        var next = --current.ToCharArray()[0];

        if (next < 65)
            next = 'Z';

        currentText.text = next.ToString();
    }

    public void DownButton(int index) {
        var currentText = SwitchLetter(index);
        var current = currentText.text;
        var next = ++current.ToCharArray()[0];

        if (next > 90)
            next = 'A';

        currentText.text = next.ToString();
    }


    public void AcceptButton() {
        _numberOfPlayerNamed++;
        _score.AddScore(string.Concat(firstLetter.text, secondLetter.text, thirdLetter.text),
                        _numberOfPlayerNamed == 1 ? _scorePlayer1 : _scorePlayer2);
        _score.SaveScore();

        //save name in gameManager in fonction of numberOfPlayerNamed
        //si les deux joueurs n'ont pas été nommé on recharge l'écran pour donner un second nom.
        if (_numberOfPlayerNamed >= 2) return;

        ResetName();
        titleText.text = "Nom du joueur 2";
        titleText.color = Color.blue;
        transform.Find("GoToHighScore").gameObject.SetActive(true);
        transform.Find("Retry").gameObject.SetActive(true);
        transform.Find("Accept").gameObject.SetActive(false);
    }

    public void GoToScoreBoard() {
        gameObject.SetActive(false);
        highScoreTable.Display();
    }

    public void Quit() {
        SceneManager.LoadScene("Menu");
    }

    private void ResetName() {
        firstLetter.text = "A";
        secondLetter.text = "A";
        thirdLetter.text = "A";
    }

    private Text SwitchLetter(int index) {
        switch (index) {
            case 1: return firstLetter;
            case 2: return secondLetter;
            case 3: return thirdLetter;
            default: return firstLetter; //juste pour éviter erreur variable non assignée
        }
    }
}