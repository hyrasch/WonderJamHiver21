using System;
using System.Collections.Generic;
using System.Linq;
using Save;

[Serializable]
public class Score
{
    public List<Tuple<string, int>> Scores { get; private set; }

    public Score()
    {
        Scores = new List<Tuple<string, int>>();
    }
    
    public Score(List<Tuple<string, int>> scores)
    {
        Scores = scores;
    }

    public void SaveScore()
    {
        SaveSystem.SaveScore(this);
    }

    public void LoadScore()
    {
        Scores = SaveSystem.LoadScore().Scores;
    }

    private void SortScore()
    {
        Scores.Sort((x, y) => y.Item2.CompareTo(x.Item2));
    }

    private void CleanScores()
    {
        Scores = Scores.Take(10).ToList();
    }

    public void AddScore(string playerName, int playerScore)
    {
        Scores.Add(Tuple.Create(playerName, playerScore));

        if (Scores.Count < 10) return;
        
        SortScore();
        CleanScores();
    }
}
