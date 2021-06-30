using UnityEngine;
using TMPro;

public class UI_Highscore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        if(GameManager.Get() != null)
        {
            GameManager.Get().updateHSUI += UpdateDataHighScore;
        }
    }
    private void OnDestroy()
    {
        if (GameManager.Get() != null)
        {
            GameManager.Get().updateHSUI -= UpdateDataHighScore;
        }
    }
    void UpdateDataHighScore(ref Score scorePlayer)
    {
        text.text = scorePlayer.namePlayer + ":" + scorePlayer.score;
    }
}