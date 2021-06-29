using System.Collections.Generic;
using UnityEngine;

public class MapRandomizer : MonoBehaviour
{
    [SerializeField] public List<GameObject> levels;
    private int randomOfLevels;
    private int lastRandValue;

    public void ChoosRandomLevel()
    {
        randomOfLevels = Random.Range(0, levels.Count);
        if(randomOfLevels == lastRandValue)
        {
            randomOfLevels++;
            if (randomOfLevels >= levels.Count)
                randomOfLevels = 0;
        }
        lastRandValue = randomOfLevels;

        GameObject go = Instantiate(levels[randomOfLevels], transform.position, Quaternion.identity);
        go.transform.position = new Vector3(0, 0, 300);
    }
}