using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject scoreboardUI;
    public GameObject backButton;
    public Transform selection;

    private Player _system;

    private void Awake() {
        _system = ReInput.players.GetPlayer("System");
    }

    private void Start() {
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    private void Update() {
        selection.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }

    public void NewGame() {
        SceneManager.LoadScene("Main");
    }

    public void Scores() {
        mainMenuUI.SetActive(false);
        selection.gameObject.SetActive(false);
        scoreboardUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void Quit() {
        Application.Quit();
    }

    public void Back() {
        scoreboardUI.SetActive(false);
        mainMenuUI.SetActive(true);
        selection.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }
}