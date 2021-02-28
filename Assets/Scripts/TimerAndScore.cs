using System;
using UnityEngine;
using TMPro;

public class TimerAndScore : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timer;

    [SerializeField]
    TextMeshProUGUI scoreUI;

    public int scoreP1;
    public int scoreP2;

    private bool turnP1 = true;

    public Character2DController Runner;
    
    public float timeRemaining = 300;
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
        setScore(Runner.GetScore());
    }

    public void setScore(int value)
    {
        if (turnP1)
        {
            scoreP1 = Mathf.Max(scoreP1, value - 1);
        }
        else
        {
            scoreP2 = Mathf.Max(scoreP2, value - 1);
        }
        scoreUI.SetText(turnP1 ? scoreP1.ToString() : scoreP2.ToString());
    }

    public void addToScore(int value)
    {
        if (turnP1)
        {
            scoreP1 += value;
        }
        else
        {
            scoreP2 += value;
        }
        setScore(turnP1 ? scoreP1 : scoreP2);
    }

    void setTimer(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void setTurn2()
    {
        turnP1 = !turnP1;
    }

}
