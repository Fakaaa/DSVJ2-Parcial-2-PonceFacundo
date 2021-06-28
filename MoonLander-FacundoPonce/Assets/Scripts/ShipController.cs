using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] ParticleSystem propulsion;
    [SerializeField] Animator crashAnimator;
    public ShipData dataSpaceShip;
    private Rigidbody2D myBody;

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
        myBody.gravityScale = dataSpaceShip.gravityInfluence;
    }

    void Update()
    {
        RotateShip();
        ImpulseShip();

        Debug.Log(minDegreeToExplode);
        Debug.Log(Mathf.Abs(myBody.transform.rotation.z));

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
            Debug.Log(Mathf.Abs(myBody.transform.rotation.z) > minDegreeToExplode);

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
                GameManager.Get()?.IncreaseScore(250);
            }
        }
        else
        {
            switch (collision.gameObject.tag)   
            {
                case "X2":
                    GameManager.Get()?.IncreaseScore(250 * 2);
                    break;
                case "X4":
                    GameManager.Get()?.IncreaseScore(250 * 4);
                    break;
                case "X6":
                    GameManager.Get()?.IncreaseScore(250 * 6);
                    break;
                case "X8":
                    GameManager.Get()?.IncreaseScore(250 * 8);
                    break;
            }
        }
    }
}
