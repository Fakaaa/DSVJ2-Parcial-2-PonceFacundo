using UnityEngine;

public class ShipData : MonoBehaviour
{
    public float altitude;
    public float verticalVelocity;
    public float horizontalVelocity;
    public float gravityInfluence;
    public float fuel;
    public float propulsionPower;
    public float rotationSpeed;

    public bool landed;

    public delegate void PassShipDataToUI();
    public static PassShipDataToUI updateUIShip;

    private void Update()
    {
        updateUIShip?.Invoke();
    }
}