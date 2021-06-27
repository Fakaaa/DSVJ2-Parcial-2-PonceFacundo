using UnityEngine;

public class ShipController : MonoBehaviour
{
    public ShipData dataSpaceShip;
    private Rigidbody2D myBody;

    private Ray rayToGround;
    private float maxHeight;

    void Start()
    {
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
            myBody.AddForce(transform.up * dataSpaceShip.propulsionPower, ForceMode2D.Force);
            dataSpaceShip.fuel--;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Toco el piso xd");
        }
    }
}
