using MonoBehaviourSingletonScript;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] public bool playerAlive;
    [SerializeField] public int playerScore;
    [SerializeField] public List<Score> highScores;

    public void IncreaseScore(int scoreAmount)
    {
        playerScore += scoreAmount;
    }

    public void SetHighScore( string namePlayer, int score)
    {
        Score newHighScore = new Score();
        newHighScore.namePlayer = namePlayer;
        newHighScore.score = score;
        highScores.Add(newHighScore);
    }
}
