using UnityEngine;

public class GMReference : MonoBehaviour
{
    private GameManager referenceManager;
    void Start()
    {
        referenceManager = GameManager.Get();
        if (referenceManager == null)
            referenceManager = FindObjectOfType<GameManager>();
    }   
    public void ChangeLevel()
    {
        referenceManager?.ChangeLevel();
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