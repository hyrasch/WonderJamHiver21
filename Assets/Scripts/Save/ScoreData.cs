using System;
using System.Collections.Generic;

namespace Save
{
    [Serializable]
    public class ScoreData
    {
        public List<Tuple<string, int>> Scores { get; }

        public ScoreData(Score score)
        {
            Scores = score.Scores;
        }
    }
}
