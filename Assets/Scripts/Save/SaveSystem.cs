using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Save
{
    public static class SaveSystem
    {
        public static void SaveScore(Score score)
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "scores.bin";
            var stream = new FileStream(path, FileMode.Create);
            var data = new Score(score.Scores);
        
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static Score LoadScore()
        {
            var path = Application.persistentDataPath + "scores.bin";

            if (!File.Exists(path)) return new Score();
        
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            var data = formatter.Deserialize(stream) as Score;
        
            stream.Close();
            return data;
        }
    }
}
