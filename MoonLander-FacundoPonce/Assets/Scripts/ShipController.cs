using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    [SerializeField] ParticleSystem propulsion;
    [SerializeField] Animator crashAnimator;
    public ShipData dataSpaceShip;
    public float minDegreeToExplode;
    private Rigidbody2D myBody;

    private Ray rayToGround;
    private float maxHeight;
    private float maxYvelToCrash;

    void Start()
    {
        maxYvelToCrash = -15f;
        maxHeight = 400;
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.gravityScale = dataSpaceShip.gravityInfluence;
    }

    void Update()
    {
        RotateShip();
        ImpulseShip();

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
            transform.Rotate(transform.forward * Time.deltaTime * dataSpaceShip.rotationSpeed);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(-transform.forward * Time.deltaTime * dataSpaceShip.rotationSpeed);
    }

    void CheckIfIsNearGround()
    {
        rayToGround = new Ray(transform.position, Vector3.down * maxHeight);
        Debug.DrawRay(rayToGround.origin, rayToGround.direction * maxHeight);

        RaycastHit2D hitInfo = Physics2D.Raycast(rayToGround.origin, rayToGround.direction, maxHeight);

        dataSpaceShip.altitude = Vector2.Distance(transform.position, hitInfo.point);
        dataSpaceShip.verticalVelocity = myBody.velocity.y;
        dataSpaceShip.horizontalVelocity = myBody.velocity.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if(transform.eulerAngles.z > minDegreeToExplode || transform.eulerAngles.z < -minDegreeToExplode)
            {
                if (dataSpaceShip.verticalVelocity > maxYvelToCrash)
                {
                    crashAnimator.SetBool("Crash", true);
                    StartCoroutine(PlayerDies());
                }
            }

            Debug.Log("Good Landing");
        }
    }

    IEnumerator PlayerDies()
    {
        yield return new WaitForSeconds(1);
        crashAnimator.SetBool("Crash", true);
        Destroy(gameObject);
    }
}
