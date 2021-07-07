﻿using UnityEngine;

public class UI_FinalSplash : MonoBehaviour
{
    [SerializeField] Animation myAnim;
    void Start()
    {
        if(GameManager.Get() != null)
        {
            if (!GameManager.Get().WasFinalSplashPlayed())
            {
                myAnim.Play();
                GameManager.Get().FinalSplash();
            }
            else
            {
                myAnim.Play();
            }
        }
    }
}