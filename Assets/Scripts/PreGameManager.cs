using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class PreGameManager : MonoBehaviour
{
    public List<GameObject> joysticksMaster;
    public List<GameObject> joysticksRunner;
    public List<GameObject> keyboardsMaster;
    public List<GameObject> keyboardsRunner;

    private Player _master;
    private Player _runner;

    private bool masterHadJoystick;
    private bool masterJoystick;
    private bool runnerHadJoystick;
    private bool runnerJoystick;

    private void Awake() {
        _master = ReInput.players.GetPlayer("Master");
        _runner = ReInput.players.GetPlayer("Runner");
    }

    private void Update() {
        masterHadJoystick = masterJoystick;
        runnerHadJoystick = runnerJoystick;

        masterJoystick = _master.controllers.joystickCount > 0;
        runnerJoystick = _runner.controllers.joystickCount > 0;

        switch (masterHadJoystick) {
            case false when masterJoystick: {
                foreach (var img in joysticksMaster)
                    img.SetActive(true);
                foreach (var img in keyboardsMaster)
                    img.SetActive(false);
                break;
            }
            case true when !masterJoystick: {
                foreach (var img in joysticksMaster)
                    img.SetActive(false);
                foreach (var img in keyboardsMaster)
                    img.SetActive(true);
                break;
            }
        }

        switch (runnerHadJoystick) {
            case false when runnerJoystick: {
                foreach (var img in joysticksRunner)
                    img.SetActive(true);
                foreach (var img in keyboardsRunner)
                    img.SetActive(false);
                break;
            }
            case true when !runnerJoystick: {
                foreach (var img in joysticksRunner)
                    img.SetActive(false);
                foreach (var img in keyboardsRunner)
                    img.SetActive(true);
                break;
            }
        }
    }
}