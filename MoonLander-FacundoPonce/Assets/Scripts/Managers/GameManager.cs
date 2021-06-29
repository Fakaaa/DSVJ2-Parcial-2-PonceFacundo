using MonoBehaviourSingletonScript;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [Header("IN GAME MAGNAMENT")]
    [SerializeField] public bool playerAlive;
    [SerializeField] public int playerScore;
    [SerializeField] public List<Score> highScores;
    [SerializeField] public int pointsPerLand;
    [SerializeField] public bool gamePaused;

    [Header("LEVEL MAGNAMENT")]
    [SerializeField] public int actualLevel;
    [SerializeField] private TextMeshProUGUI levelUI;
    [SerializeField] public MapRandomizer randomizer;
    [SerializeField] private CanvasGroup blendPerLevel;

    [Header("SCENE MAGNAMENT")]
    [SerializeField] public SceneLoader sceneLoader;
    private float amountBlend = 0.5f;

    public delegate void PauseGameEvent();
    public PauseGameEvent isGamePaused;

    private void Start()
    {
        blendPerLevel.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();

        if (gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void ResumeGame()
    {
        gamePaused = false;
    }
    public void PauseGame()
    {
        isGamePaused?.Invoke();
        gamePaused = !gamePaused;
    }
    public void QuitGame()
    {
        sceneLoader.QuitGame();
    }
    IEnumerator ChangeScene(string nameScene)
    {
        blendPerLevel.gameObject.SetActive(true);
        while (blendPerLevel.alpha < 1)
        {
            blendPerLevel.alpha += Mathf.Clamp(amountBlend, 0, 1) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(sceneLoader.AsynchronousLoad(nameScene));

        yield return new WaitForSeconds(1);
        if(nameScene != "MainMenu")
            randomizer.ChoosRandomLevel();

        while (blendPerLevel.alpha > 0)
        {
            blendPerLevel.alpha -= Mathf.Clamp(amountBlend, 0, 1) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        blendPerLevel.gameObject.SetActive(false);
    }
    public int GetPointsPerLand()
    {
        return pointsPerLand;
    }
    public void ChangeSceneByName(string scene)
    {
        if (scene == "MainMenu")
            actualLevel = 0;
        StartCoroutine(ChangeScene(scene));
    }
    public void ChangeLevel()
    {
        actualLevel++;
        levelUI.text = "LEVEL:" + actualLevel.ToString();
        StartCoroutine(ChangeScene("Game"));
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
