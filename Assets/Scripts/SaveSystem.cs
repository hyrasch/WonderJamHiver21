using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveScore(Score score)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "scores.bin";
        var stream = new FileStream(path, FileMode.Create);
        var data = new ScoreData(score);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ScoreData LoadScore()
    {
        var path = Application.persistentDataPath + "scores.bin";

        if (!File.Exists(path)) return null;
        
        var formatter = new BinaryFormatter();
        var stream = new FileStream(path, FileMode.Open);
        var data = formatter.Deserialize(stream) as ScoreData;
        
        stream.Close();
        return data;
    }
}
