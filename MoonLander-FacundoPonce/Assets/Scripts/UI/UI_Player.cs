using UnityEngine;
using TMPro;

public class UI_Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI altitude;
    [SerializeField] TextMeshProUGUI velocityX;
    [SerializeField] TextMeshProUGUI velocityY;
    [SerializeField] TextMeshProUGUI amountFuel;

    private ShipData myShipRef;

    void Start()
    {
        myShipRef = FindObjectOfType<ShipData>();
        ShipData.updateUIShip += UpdateMyData;
    }
    private void OnDisable()
    {
        ShipData.updateUIShip -= UpdateMyData;
    }

    void UpdateMyData()
    {
        if(myShipRef != null)
        {
            score.text = "Score:"+ GameManager.Get()?.GetScorePlayer().ToString();
            altitude.text = (Mathf.Abs(Mathf.Round(myShipRef.altitude))).ToString() + "mts";
            velocityX.text = "X:" + (Mathf.Round(myShipRef.horizontalVelocity)).ToString();
            velocityY.text = "Y:" + (Mathf.Round(myShipRef.verticalVelocity)).ToString();
            amountFuel.text = myShipRef.fuel.ToString();
        }
    }
}
