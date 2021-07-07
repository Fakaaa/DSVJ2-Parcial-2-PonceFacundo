using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    void Start()
    {
        if(GameManager.Get() != null)
            GameManager.Get().OnGamePaused += ActivatePause;
    }
    private void OnDisable()
    {
        if (GameManager.Get() != null) 
            GameManager.Get().OnGamePaused -= ActivatePause;
    }
    void ActivatePause()
    {
        panelPause.gameObject.SetActive(!panelPause.gameObject.activeSelf);
    }
}