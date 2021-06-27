using UnityEngine;
using MonoBehaviourSingletonScript;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] public bool playerAlive;
    [SerializeField] public float playerScore;
}
