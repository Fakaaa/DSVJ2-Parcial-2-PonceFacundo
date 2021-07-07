using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] ParticleSystem propulsion;
    [SerializeField] Animator crashAnimator;
    public ShipData dataSpaceShip;
    private Rigidbody2D myBody;

    public delegate void ShipLanded(ref bool landed);
    public static ShipLanded shipLanded;
    public delegate void PlayerGainPoints(ref Collision2D collision);
    public static PlayerGainPoints playerScoreIncrease;

    private Ray rayToGround;
    private float maxHeight;
    private float minDegreeToExplode;
    private float maxYvelToCrash;

    void Start()
    {
        LimitLevels.playerOutOfLimits += DestroyShip;

        minDegreeToExplode = 0.35f;
        maxYvelToCrash = -20f;
        maxHeight = 600;
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.isKinematic = false;
        myBody.transform.rotation = new Quaternion(0,0,-1.0f,1);
        dataSpaceShip.landed = false;
        myBody.gravityScale = dataSpaceShip.gravityInfluence;
    }

    void FixedUpdate()
    {
        if(!dataSpaceShip.landed)
        {
            RotateShip();
            ImpulseShip();
        }

        CheckIfIsNearGround();
    }

    private void OnDisable()
    {
        LimitLevels.playerOutOfLimits -= DestroyShip;
    }

    void ImpulseShip()
    {
        if (Input.GetKey(KeyCode.Space) && dataSpaceShip.fuel > 0)
        {
            myBody.AddForce(transform.up * dataSpaceShip.propulsionPower, ForceMode2D.Force);
            dataSpaceShip.fuel--;
            if (!propulsion.isPlaying) propulsion.Play(true);
        }
        else
           propulsion.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    void RotateShip()
    {
        if (Input.GetKey(KeyCode.A))
            myBody.transform.Rotate(transform.forward * dataSpaceShip.rotationSpeed);
        if (Input.GetKey(KeyCode.D))
            myBody.transform.Rotate(-transform.forward * dataSpaceShip.rotationSpeed);
    }

    void CheckIfIsNearGround()
    {
        rayToGround = new Ray(myBody.transform.position, Vector3.down * maxHeight);

        RaycastHit2D hitInfo = Physics2D.Raycast(rayToGround.origin, rayToGround.direction, maxHeight);

        UpdateShipInfo(ref hitInfo);
    }

    public void UpdateShipInfo(ref RaycastHit2D hitInfoAltitude)
    {
        dataSpaceShip.altitude = Vector2.Distance(myBody.transform.position, hitInfoAltitude.point);
        dataSpaceShip.verticalVelocity = myBody.velocity.y;
        dataSpaceShip.horizontalVelocity = myBody.velocity.x;
    }

    public void DestroyShip()
    {
        myBody.isKinematic = true;
        crashAnimator.SetBool("Crash", true);
        propulsion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        dataSpaceShip.landed = false;
        shipLanded?.Invoke(ref dataSpaceShip.landed);
        Destroy(gameObject, 0.5f);
    }

    public void ShipLand(ref Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (Mathf.Abs(myBody.transform.rotation.z) > minDegreeToExplode || dataSpaceShip.verticalVelocity < maxYvelToCrash)
            {
                DestroyShip();
            }
            else
            {
                playerScoreIncrease?.Invoke(ref collision);
                dataSpaceShip.landed = true;
                shipLanded?.Invoke(ref dataSpaceShip.landed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         ShipLand(ref collision);
    }
}