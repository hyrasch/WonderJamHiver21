using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Transform selection;

    private Player _master;

    private void Awake() {
        _master = ReInput.players.GetPlayer("Master");
    }

    private void Start() {
        foreach (var ruleSet in _master.controllers.maps.mapEnabler.ruleSets)
            ruleSet.enabled = false;
        _master.controllers.maps.mapEnabler.ruleSets.Find(rs => rs.tag == "Menu").enabled = true;
        _master.controllers.maps.mapEnabler.Apply();
    }

    private void Update() {
        selection.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }

    public void NewGame() {
        SceneManager.LoadScene("Main");
    }

    public void Scores() {
        Debug.Log("Scores");
    }

    public void Quit() {
        Application.Quit();
    }
}