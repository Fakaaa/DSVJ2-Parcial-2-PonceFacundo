using UnityEngine;
using TMPro;

public class TimerGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    private float seconds =0;
    private int minutes =0;

    void Update()
    {
        if (seconds < 60)
            seconds += Time.deltaTime;
        else
        {
            minutes++;
            seconds = 0;
        }

        timeText.text = "Time: " + minutes.ToString() + ":" + seconds.ToString("F0");
    }
}
