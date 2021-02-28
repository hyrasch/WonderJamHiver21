using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreGameManager : MonoBehaviour
{
    public List<GameObject> joysticksMaster;
    public List<GameObject> joysticksRunner;
    public List<GameObject> keyboardsMaster;
    public List<GameObject> keyboardsRunner;

    private Player _master;
    private Player _runner;

    private bool _masterHadJoystick;
    private bool _masterJoystick;
    private bool _runnerHadJoystick;
    private bool _runnerJoystick;

    private void Awake() {
        _master = ReInput.players.GetPlayer("Master");
        _runner = ReInput.players.GetPlayer("Runner");
    }

    private void Start() {
        _master.controllers.maps.mapEnabler.ruleSets.Find(rs => rs.tag == "Master").enabled = true;
        _runner.controllers.maps.mapEnabler.ruleSets.Find(rs => rs.tag == "Runner").enabled = true;
        _master.controllers.maps.mapEnabler.Apply();
        _runner.controllers.maps.mapEnabler.Apply();
    }

    private void Update() {
        if (_master.GetButtonLongPressDown("Select")) {
            SceneManager.LoadScene("Main");
        }

        _masterHadJoystick = _masterJoystick;
        _runnerHadJoystick = _runnerJoystick;

        _masterJoystick = _master.controllers.joystickCount > 0;
        _runnerJoystick = _runner.controllers.joystickCount > 0;

        switch (_masterHadJoystick) {
            case false when _masterJoystick: {
                foreach (var img in joysticksMaster)
                    img.SetActive(true);
                foreach (var img in keyboardsMaster)
                    img.SetActive(false);
                break;
            }
            case true when !_masterJoystick: {
                foreach (var img in joysticksMaster)
                    img.SetActive(false);
                foreach (var img in keyboardsMaster)
                    img.SetActive(true);
                break;
            }
        }

        switch (_runnerHadJoystick) {
            case false when _runnerJoystick: {
                foreach (var img in joysticksRunner)
                    img.SetActive(true);
                foreach (var img in keyboardsRunner)
                    img.SetActive(false);
                break;
            }
            case true when !_runnerJoystick: {
                foreach (var img in joysticksRunner)
                    img.SetActive(false);
                foreach (var img in keyboardsRunner)
                    img.SetActive(true);
                break;
            }
        }
    }
}