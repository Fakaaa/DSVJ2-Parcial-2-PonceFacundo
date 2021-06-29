using UnityEngine;
using System.Collections.Generic;

public class BackgroundRand : MonoBehaviour
{
    [SerializeField] public SpriteRenderer myBg;
    [SerializeField] public List<Sprite> backgrounds;

    private int choosedBg;
    private int lastBgChoosed;
    void Start()
    {
        choosedBg = Random.Range(1, backgrounds.Count);
        if(choosedBg == lastBgChoosed)
        {
            choosedBg++;
            if (choosedBg >= backgrounds.Count)
                choosedBg = 0;
        }
        lastBgChoosed = choosedBg;

        myBg.sprite = backgrounds[choosedBg];
    }
}