﻿using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] ParticleSystem propulsion;
    [SerializeField] Animator crashAnimator;
    public ShipData dataSpaceShip;
    private Rigidbody2D myBody;

    private bool landed;
    public delegate void ShipLanded(ref bool landed);
    public static ShipLanded shipLanded;

    private Ray rayToGround;
    private float maxHeight;
    private float minDegreeToExplode;
    private float maxYvelToCrash;

    void Start()
    {
        minDegreeToExplode = 0.35f;
        maxYvelToCrash = -20f;
        maxHeight = 400;
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.isKinematic = false;
        landed = false;
        myBody.gravityScale = dataSpaceShip.gravityInfluence;
    }

    void Update()
    {
        if(!landed)
        {
            RotateShip();
            ImpulseShip();
        }

        CheckIfIsNearGround();
    }

    void ImpulseShip()
    {
        if (Input.GetKey(KeyCode.Space) && dataSpaceShip.fuel > 0)
        {
            myBody.AddForce(transform.up * dataSpaceShip.propulsionPower * Time.deltaTime, ForceMode2D.Force);
            dataSpaceShip.fuel--;
            if (!propulsion.isPlaying)
                propulsion.Play(true);
        }
        else
        {
           propulsion.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    void RotateShip()
    {
        if (Input.GetKey(KeyCode.A))
            myBody.transform.Rotate(transform.forward * Time.deltaTime * dataSpaceShip.rotationSpeed);
        if (Input.GetKey(KeyCode.D))
            myBody.transform.Rotate(-transform.forward * Time.deltaTime * dataSpaceShip.rotationSpeed);
    }

    void CheckIfIsNearGround()
    {
        rayToGround = new Ray(myBody.transform.position, Vector3.down * maxHeight);
        Debug.DrawRay(rayToGround.origin, rayToGround.direction * maxHeight);

        RaycastHit2D hitInfo = Physics2D.Raycast(rayToGround.origin, rayToGround.direction, maxHeight);

        dataSpaceShip.altitude = Vector2.Distance(myBody.transform.position, hitInfo.point);
        dataSpaceShip.verticalVelocity = myBody.velocity.y;
        dataSpaceShip.horizontalVelocity = myBody.velocity.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if(Mathf.Abs(myBody.transform.rotation.z) > minDegreeToExplode || dataSpaceShip.verticalVelocity < maxYvelToCrash)
            {
                myBody.isKinematic = true;
                crashAnimator.SetBool("Crash", true);
                propulsion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                Destroy(gameObject, 0.5f);
                Debug.Log("Bad Landing");
            }
            else
            {
                switch (collision.gameObject.layer)
                {
                    case 8:
                        GameManager.Get()?.IncreaseScore(GameManager.Get().GetPointsPerLand() * 2);
                        break;
                    case 9:
                        GameManager.Get()?.IncreaseScore(GameManager.Get().GetPointsPerLand() * 4);
                        break;
                    case 10:
                        GameManager.Get()?.IncreaseScore(GameManager.Get().GetPointsPerLand() * 6);
                        break;
                    case 11:
                        GameManager.Get()?.IncreaseScore(GameManager.Get().GetPointsPerLand() * 8);
                        break;
                    default:
                        GameManager.Get()?.IncreaseScore(GameManager.Get().GetPointsPerLand());
                        break;
                }
                landed = true;
                shipLanded?.Invoke(ref landed);
            }
        }
    }
}
