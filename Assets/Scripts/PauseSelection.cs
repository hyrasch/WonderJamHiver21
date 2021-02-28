using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseSelection : MonoBehaviour
{
    public Transform selection;

    private void Start() {
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    private void Update() {
        selection.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}