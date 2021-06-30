using UnityEngine;

public class LimitLevels : MonoBehaviour
{
    private ShipController player;

    private void Start()
    {
        player = FindObjectOfType<ShipController>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (player != null)
                player.DestroyShip();
        }
    }
}
