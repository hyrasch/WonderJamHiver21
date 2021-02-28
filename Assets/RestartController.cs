using System;
using Rewired;
using UnityEngine;

public class RestartController : MonoBehaviour
{
    public bool restart;

    private Player _master;
    private Player _runner;
    private Player _system;

    private void Awake() {
        _master = ReInput.players.GetPlayer("Master");
        _runner = ReInput.players.GetPlayer("Runner");
        _system = ReInput.players.GetPlayer("System");
    }

    private void Update() {
        if (!_system.GetAnyButtonDown()) return;

        Time.timeScale = 1f;
        _runner.isPlaying = _master.isPlaying = true;
        restart = false;
        gameObject.SetActive(false);
    }
}