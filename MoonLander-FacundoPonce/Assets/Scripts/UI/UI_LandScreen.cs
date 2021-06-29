using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UI_LandScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI landedCondition;
    [SerializeField] GameObject panelLand;

    [SerializeField] Button nextLevel;
    [SerializeField] Button nextLevelDisable;

    void Start()
    {
        panelLand.gameObject.SetActive(false);
        ShipController.shipLanded += UpdateLandScree;
    }
    private void OnDisable()
    {
        ShipController.shipLanded -= UpdateLandScree;
    }
    public void DeactiveLandScreen()
    {
        panelLand.gameObject.SetActive(false);
    }
    void UpdateLandScree(ref bool landed)
    {
        panelLand.gameObject.SetActive(true);

        if (landed)
        {
            landedCondition.text = "You land!";
            nextLevel.gameObject.SetActive(true);
            nextLevelDisable.gameObject.SetActive(false);
        }
        else
        {
            landedCondition.text = "You crash!";
            nextLevel.gameObject.SetActive(false);
            nextLevelDisable.gameObject.SetActive(true);
        }
    }
}
