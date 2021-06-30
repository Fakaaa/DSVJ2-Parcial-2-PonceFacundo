using UnityEngine;

public class LimitLevels : MonoBehaviour
{
    public delegate void KillPlayerOffLimits();
    public static KillPlayerOffLimits playerOutOfLimits;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerOutOfLimits?.Invoke();
        }
    }
}