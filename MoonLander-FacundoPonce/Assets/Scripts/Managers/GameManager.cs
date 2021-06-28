﻿using MonoBehaviourSingletonScript;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] public bool playerAlive;
    [SerializeField] public int playerScore;
    [SerializeField] public List<Score> highScores;
    [SerializeField] public int actualLevel;

    [SerializeField] private CanvasGroup blendPerLevel;
    private float amountBlend = 0.5f;

    IEnumerator WaitForBlend()
    {
        while (blendPerLevel.alpha < 1)
        {
            blendPerLevel.alpha += Mathf.Clamp(amountBlend, 0, 1) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        while (blendPerLevel.alpha > 0)
        {
            blendPerLevel.alpha -= Mathf.Clamp(amountBlend, 0, 1) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    public void ChangeLevel()
    {
        actualLevel++;
        StartCoroutine(WaitForBlend());
    }
    public void IncreaseScore(int scoreAmount)
    {
        playerScore += scoreAmount;
    }
    public int GetScorePlayer()
    {
        return playerScore;
    }
    public void SetHighScore( string namePlayer, int score)
    {
        Score newHighScore = new Score();
        newHighScore.namePlayer = namePlayer;
        newHighScore.score = score;
        highScores.Add(newHighScore);
    }
}
