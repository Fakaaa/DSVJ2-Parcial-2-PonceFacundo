using UnityEngine;
using TMPro;

public class GMReference : MonoBehaviour
{
    private GameManager referenceManager;
    public TextMeshProUGUI playerName;
    void Start()
    {
        referenceManager = GameManager.Get();
        if (referenceManager == null)
            referenceManager = FindObjectOfType<GameManager>();
    }   

    public void SeeHighScore()
    {
        referenceManager.CallUpdateHighScore();
    }
    public void ChangeLevel()
    {
        referenceManager?.ChangeLevel();
    }
    public void SetPlayerName()
    {
        referenceManager?.SetNamePlayer(playerName.text);
    }
    public void MainMenu()
    {
        referenceManager?.ResumeGame();
        referenceManager?.ResetScore();
        referenceManager?.ChangeSceneByName("MainMenu");
    }
    public void Credits()
    {
        referenceManager?.ChangeSceneByName("Credits");
    }

    public void QuitGame()
    {
        referenceManager?.QuitGame();
    }

    public void PauseGame()
    {
        referenceManager?.PauseGame();
    }
}