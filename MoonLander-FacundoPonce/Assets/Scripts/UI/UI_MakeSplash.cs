using UnityEngine;

public class UI_MakeSplash : MonoBehaviour
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