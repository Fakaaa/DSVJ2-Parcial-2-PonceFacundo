﻿using UnityEngine;

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
        referenceManager?.ChangeSceneByName("MainMenu");
    }
    public void QuitGame()
    {
        referenceManager?.QuitGame();
    }
}
