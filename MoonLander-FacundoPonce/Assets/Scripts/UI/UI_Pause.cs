using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    void Start()
    {
        if(GameManager.Get() != null)
            GameManager.Get().isGamePaused += ActivatePause;
    }
    private void OnDisable()
    {
        if (GameManager.Get() != null) 
            GameManager.Get().isGamePaused -= ActivatePause;
    }
    void ActivatePause()
    {
        panelPause.gameObject.SetActive(!panelPause.gameObject.activeSelf);
    }
}