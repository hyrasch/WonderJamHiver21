using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform _entryContainer;
    private Transform _entryTemplate;
    
    private readonly List<Transform> _highScoreTransformList = new List<Transform>();

    private Score _score;

    private void Awake()
    {
        _entryContainer = transform.Find("HighScoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighScoreEntryTemplate");
        
        _score = new Score();
        _score.LoadScore();

        foreach (var (item1, item2) in _score.Scores)
        {
            CreateHighScoreEntryTransform(item1, item2, _entryContainer, _highScoreTransformList);
        }
    }
    
    private void CreateHighScoreEntryTransform(string pName, int score,Transform container, ICollection<Transform> transformList)
    {
        const float templateHeight = 40f;
        var entryTransform = Instantiate(_entryTemplate, container);
        var entryRectTransform = entryTransform.GetComponent<RectTransform>();
        
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        var rank = transformList.Count + 1;
        string rankString;
        
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.transform.Find("posText").GetComponent<Text>().text = rankString;
        entryTransform.transform.Find("scoreText").GetComponent<Text>().text = score.ToString();
        entryTransform.transform.Find("nameText").GetComponent<Text>().text = pName;
        entryTransform.transform.Find("background").gameObject.SetActive(rank % 2 == 1);
        
        switch(rank)
        {
            default:
                entryTransform.transform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.transform.Find("trophy").gameObject.SetActive(true);
                entryTransform.transform.Find("trophy").GetComponent<Image>().color = Color.white;
                break;

            case 2:
                entryTransform.transform.Find("trophy").gameObject.SetActive(true);
                entryTransform.transform.Find("trophy").GetComponent<Image>().color = new Color(0.7607844f, 0.7607844f, 0.7607844f, 1f);
                break;

            case 3:
                entryTransform.transform.Find("trophy").gameObject.SetActive(true);
                entryTransform.transform.Find("trophy").GetComponent<Image>().color = new Color(1f, 0.516129f, 0, 1);
                break;
        }
        
        if (rank == 1)
        {
            var green = new Color(0, 0.8490566f, 0.04675445f, 1f);
            
            entryTransform.transform.Find("nameText").GetComponent<Text>().color = green;
            entryTransform.transform.Find("posText").GetComponent<Text>().color = green;
            entryTransform.transform.Find("scoreText").GetComponent<Text>().color = green;
        }
        
        transformList.Add(entryTransform.transform);
    }
}
