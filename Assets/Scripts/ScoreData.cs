[System.Serializable]
public class ScoreData
{
    public string[] FPlayerNames { get; }
    public string[] SPlayerNames { get; }
    public int[] FPlayerScores { get; }
    public int[] SPlayerScores { get; }

    public ScoreData(Score score)
    {
        FPlayerNames = new string[score.FPlayerNames.Count];
        SPlayerNames = new string[score.SPlayerNames.Count];
        
        FPlayerScores = new int[score.FPlayerScores.Count];
        SPlayerScores = new int[score.SPlayerScores.Count];

        int i;
        for (i = 0; i < FPlayerNames.Length; i++)
        {
            FPlayerNames[i] = score.FPlayerNames[i];
        }
        for (i = 0; i < SPlayerNames.Length; i++)
        {
            SPlayerNames[i] = score.SPlayerNames[i];
        }
        
        for (i = 0; i < FPlayerScores.Length; i++)
        {
            FPlayerScores[i] = score.FPlayerScores[i];
        }
        for (i = 0; i < SPlayerScores.Length; i++)
        {
            SPlayerScores[i] = score.SPlayerScores[i];
        }
    }
}
