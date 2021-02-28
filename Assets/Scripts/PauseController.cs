using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseUI;
    public PostGameUIManager postGameUIManager;
    public RestartController restartController;

    private Player _master;
    private Player _runner;

    private void Awake() {
        _master = ReInput.players.GetPlayer("Master");
        _runner = ReInput.players.GetPlayer("Runner");
    }

    private void Update() {
        if (postGameUIManager.gameIsEnded || restartController.restart) return;

        if (!_master.GetButtonDown("Pause") && !_runner.GetButtonDown("Pause")) return;

        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        _runner.isPlaying = _master.isPlaying = false;
    }

    // public void UpdateControllerMaps(string masterMapName, string runnerMapName) {
    //     var masterMap = _master.controllers.maps.mapEnabler;
    //     var runnerMap = _runner.controllers.maps.mapEnabler;
    //
    //     foreach (var ruleSet in masterMap.ruleSets)
    //         ruleSet.enabled = false;
    //
    //     foreach (var ruleSet in runnerMap.ruleSets)
    //         ruleSet.enabled = false;
    //
    //     masterMap.ruleSets.Find(rs => rs.tag == masterMapName).enabled = true;
    //     runnerMap.ruleSets.Find(rs => rs.tag == runnerMapName).enabled = true;
    //
    //     masterMap.Apply();
    //     runnerMap.Apply();
    // }

    public void Continue() {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        _master.isPlaying = _runner.isPlaying = true;
    }

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }
}