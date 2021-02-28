using UnityEngine;
using UnityEngine.EventSystems;

public class PauseSelection : MonoBehaviour
{
    public Transform selection;

    private void Update() {
        selection.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}