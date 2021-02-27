using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //sorting
        for(int i=0; i < highscores.highscoreEntryList.Count;i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {


                    HighScoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highScoreEntryTransformList = new List<Transform>();
        foreach(HighScoreEntry highScoreEntry in highscores.highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
        
    }
    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry,Transform container,List<Transform> transformList)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highScoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        switch(rank)
        {
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("trophy").gameObject.SetActive(true);
                entryTransform.Find("trophy").GetComponent<Image>().color = Color.white;
                break;

            case 2:
                entryTransform.Find("trophy").gameObject.SetActive(true);
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color(0.7607844f, 0.7607844f, 0.7607844f,1f);
                break;

            case 3:
                entryTransform.Find("trophy").gameObject.SetActive(true);
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color(1f, 0.516129f, 0, 1);
                break;
        }
        if ( rank ==1)
        {
            Color green = new Color(0, 0.8490566f, 0.04675445f, 1f);
            entryTransform.Find("nameText").GetComponent<Text>().color = green;
            entryTransform.Find("posText").GetComponent<Text>().color = green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = green;
        }
        transformList.Add(entryTransform);
    }

    private void addHighScoreEntry(int score,string name)
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry { name = name, score = score };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highScoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighScoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
