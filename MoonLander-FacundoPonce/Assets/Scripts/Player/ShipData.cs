using System;
using UnityEngine;

[Serializable]
public class ShipData : MonoBehaviour
{
    [HideInInspector]public float altitude;
    [HideInInspector]public float verticalVelocity;
    [HideInInspector]public float horizontalVelocity;
    [HideInInspector]public float gravityInfluence;
    [HideInInspector]public float fuel;
    [HideInInspector]public float propulsionPower;
    [HideInInspector]public float rotationSpeed;

    [HideInInspector]public bool landed;

    public delegate void PassShipDataToUI();
    public static PassShipDataToUI updateUIShip;

    private void Update()
    {
        updateUIShip?.Invoke();
    }
}