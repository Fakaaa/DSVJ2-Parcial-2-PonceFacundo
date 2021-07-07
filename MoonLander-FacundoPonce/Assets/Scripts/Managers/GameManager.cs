using MonoBehaviourSingletonScript;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [Header("IN GAME MAGNAMENT")]
    [SerializeField] public bool playerAlive;
    [SerializeField] public int playerActualScore;
    [SerializeField] public string playerName;
    [SerializeField] public Score playerHighScore;
    [SerializeField] public int pointsPerLand;
    [SerializeField] public bool gamePaused;

    [Header("LEVEL MAGNAMENT")]
    [SerializeField] public int actualLevel;
    [SerializeField] private TextMeshProUGUI levelUI;
    [SerializeField] public MapRandomizer randomizer;
    [SerializeField] private CanvasGroup blendPerLevel;

    [Header("SCENE MAGNAMENT")]
    [SerializeField] public SceneLoader sceneLoader;
    private bool finalSplashPlayed;
    private float amountBlend = 0.5f;

    private SaveScore scoreSaver;
    public delegate void PauseGameEvent();
    public PauseGameEvent OnGamePaused;
    public delegate void UpdateHighScoreUI(ref Score playerHS);
    public UpdateHighScoreUI updateHSUI;

    //UNITY FUNCIONTS
    //========================================
    private void Start()
    {
        ShipController.playerScoreIncrease += PlayerScores;
        LoadAndSaveScores();   
        finalSplashPlayed = false;
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
    private void OnDisable()
    {
        ShipController.playerScoreIncrease -= PlayerScores;
    }
    //========================================
    //SCORE MAGNAMENT
    //========================================
    public void SetNamePlayer(string name)
    {
        playerName = name;
    }
    public void LoadAndSaveScores()
    {
        scoreSaver = new SaveScore();
        playerHighScore = scoreSaver.LoadScoresFromJson();

        if(playerHighScore != null)
        {
            SetHighScore(playerHighScore.namePlayer, playerHighScore.score);
        }
        else
        {
            playerHighScore = new Score();
            playerHighScore.score = 0;
            playerHighScore.namePlayer = "Nan";
            scoreSaver.SaveScoreAmount(playerHighScore);
            SetHighScore(playerHighScore.namePlayer, playerHighScore.score);
        }
        CallUpdateHighScore();
    }
    public void CallUpdateHighScore()
    {
        updateHSUI?.Invoke(ref playerHighScore);
    }
    public void ResetScore()
    {
        playerActualScore = 0;
    }
    public int GetPointsPerLand()
    {
        return pointsPerLand;
    }
    public void IncreaseScore(int scoreAmount)
    {
        playerActualScore += scoreAmount;
        if (playerActualScore > playerHighScore.score)
        {
            SetHighScore(playerName, playerActualScore);
            scoreSaver.SaveScoreAmount(playerHighScore);
            CallUpdateHighScore();
        }
    }
    public void PlayerScores(ref Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:
                IncreaseScore(GetPointsPerLand() * 2);
                break;
            case 9:
                IncreaseScore(GetPointsPerLand() * 4);
                break;
            case 10:
                IncreaseScore(GetPointsPerLand() * 6);
                break;
            case 11:
                IncreaseScore(GetPointsPerLand() * 8);
                break;
            default:
                IncreaseScore(GetPointsPerLand());
                break;
        }
    }
    public int GetScorePlayer()
    {
        return playerActualScore;
    }
    public void SetHighScore( string namePlayer, int score)
    {
        playerHighScore.namePlayer = namePlayer;
        playerHighScore.score = score;
    }
    public Score GetHighScore()
    {
        return playerHighScore;
    }
    //========================================
    //SCENE MAGNAMENT
    //========================================
    public bool WasFinalSplashPlayed()
    {
        return finalSplashPlayed;
    }
    public void FinalSplash()
    {
        finalSplashPlayed = true;
    }
    IEnumerator ChangeScene(string nameScene)
    {
        if (nameScene == "MainMenu" || nameScene == "Credits")
            levelUI.gameObject.SetActive(false);

        blendPerLevel.gameObject.SetActive(true);
        while (blendPerLevel.alpha < 1)
        {
            blendPerLevel.alpha += Mathf.Clamp(amountBlend, 0, 1) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        sceneLoader.LoadScene(nameScene);

        yield return new WaitForSeconds(0.5f);
        if(nameScene != "MainMenu" && nameScene != "Credits")
        {
            if(levelUI.gameObject.activeSelf == false)
                levelUI.gameObject.SetActive(true);
            randomizer.ChoosRandomLevel();
        }

        while (blendPerLevel.alpha > 0)
        {
            blendPerLevel.alpha -= Mathf.Clamp(amountBlend, 0, 1) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        blendPerLevel.gameObject.SetActive(false);
    }
    public void ChangeSceneByName(string scene)
    {
        if (scene == "MainMenu")
            actualLevel = 0;
        StartCoroutine(ChangeScene(scene));
    }
    //========================================
    //GAME MAGNAMENT
    //========================================
    public void ChangeLevel()
    {
        actualLevel++;
        levelUI.text = "LEVEL:" + actualLevel.ToString();
        StartCoroutine(ChangeScene("Game"));
    }
    public void ResumeGame()
    {
        gamePaused = false;
    }
    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        gamePaused = !gamePaused;
    }
    public void QuitGame()
    {
        sceneLoader.QuitGame();
    }
    //========================================
}