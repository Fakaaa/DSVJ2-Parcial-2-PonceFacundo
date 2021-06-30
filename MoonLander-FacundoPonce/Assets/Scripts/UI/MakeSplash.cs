using UnityEngine;

public class MakeSplash : MonoBehaviour
{
    [SerializeField] Animation myAnim;
    void Start()
    {
        if(GameManager.Get() != null)
        {
            if(!GameManager.Get().WasFinalSplashPlayed())
            {
                myAnim.Play();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}