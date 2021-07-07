using UnityEngine;
using System.IO;

public class SaveScore
{
    public string jsonInfo;
    public void SaveScoreAmount(Score scoreToSave)
    {
        jsonInfo = JsonUtility.ToJson(scoreToSave);
        PlayerPrefs.SetString("highScoreToJson.json",jsonInfo);
    }
    public Score LoadScoresFromJson()
    {
        Score scoreCache = new Score { namePlayer = "Nan", score = 0 };
        jsonInfo = PlayerPrefs.GetString("highScoreToJson.json", jsonInfo);
        JsonUtility.FromJsonOverwrite(jsonInfo, scoreCache);

        return scoreCache;
    }
}