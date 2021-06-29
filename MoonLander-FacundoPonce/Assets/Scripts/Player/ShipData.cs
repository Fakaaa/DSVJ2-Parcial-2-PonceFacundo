using UnityEngine;

public class ShipData : MonoBehaviour
{
    [SerializeField] public float altitude;
    [SerializeField] public float verticalVelocity;
    [SerializeField] public float horizontalVelocity;
    [SerializeField] public float gravityInfluence;
    [SerializeField] public float fuel;
    [SerializeField] public float propulsionPower;
    [SerializeField] public float rotationSpeed;

    [SerializeField] public bool landed;

    public delegate void PassShipDataToUI();
    public static PassShipDataToUI updateUIShip;

    private void Update()
    {
        updateUIShip?.Invoke();
    }
}
