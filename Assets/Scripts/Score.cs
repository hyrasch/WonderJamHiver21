using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public List<string> FPlayerNames { get; } = new List<string>();
    public List<string> SPlayerNames { get; } = new List<string>();

    public List<int> FPlayerScores { get; } = new List<int>();
    public List<int> SPlayerScores { get; } = new List<int>();

    private void SaveScore()
    {
        SaveSystem.SaveScore(this);
    }

    private void LoadScore()
    {
        var data = SaveSystem.LoadScore();
        
        FPlayerNames.Clear();
        SPlayerNames.Clear();
        
        FPlayerScores.Clear();
        SPlayerScores.Clear();

        int i;
        for (i = 0; i < data.FPlayerNames.Length; i++)
        {
            FPlayerNames.Add(data.FPlayerNames[i]);
        }
        for (i = 0; i < data.FPlayerScores.Length; i++)
        {
            FPlayerScores.Add(data.FPlayerScores[i]);
        }
        
        for (i = 0; i < data.SPlayerNames.Length; i++)
        {
            SPlayerNames.Add(data.SPlayerNames[i]);
        }
        for (i = 0; i < data.SPlayerScores.Length; i++)
        {
            SPlayerScores.Add(data.SPlayerScores[i]);
        }
    }
}
