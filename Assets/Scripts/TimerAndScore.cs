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

    public GameObject Runner;
    
    private float timeRemaining = 300;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                setTimer(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        
        setScore(Runner.gameObject.GetComponent<Character2DController>().score);
    }

    void setScore(int value)
    {
        score.SetText(value.ToString());
    }

    void setTimer(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
