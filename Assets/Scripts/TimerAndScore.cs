using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerAndScore : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timer;

    [SerializeField]
    TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void setScore(int value)
    {
        score.SetText(value.ToString());
    }

    void setTimer(string value)
    {
        timer.SetText(value);
    }

}
