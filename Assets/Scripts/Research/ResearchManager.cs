using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour
{
    public PlayerManager P_Stats;
    public ResearchProgress R_Stats;

    public TextMeshProUGUI CargoLevel;
    public TextMeshProUGUI CargoPrice;

    public TextMeshProUGUI HullLevel;
    public TextMeshProUGUI HullPrice;

    public TextMeshProUGUI EngineLevel;
    public TextMeshProUGUI EnginePrice;

    private void OnEnable()
    {
        CargoLevel.text = "Cargo Research Capacity " + P_Stats.P_Stats.rocketCarryWeight;
        CargoPrice.text = "Upgrade Price " + (15000 * (P_Stats.P_Stats.rocketCarryWeight / 15)).ToString();

        HullLevel.text = "Hull Research Level " + R_Stats.hullLevel;
        HullPrice.text = "Upgrade Price " + (55000 * R_Stats.hullLevel).ToString(); ;

        EngineLevel.text = "Engine Research Level " + R_Stats.rocketLevel;
        EnginePrice.text = "Upgrade Price " + (35000 * R_Stats.hullLevel).ToString();
    }
    // RESEARCH IS NU NOG INSTANT, FIX DIT
    public void UpgradeCargo(Button button)
    {
        button.interactable = false;
        P_Stats.P_Stats.rocketCarryWeight += 15;
        P_Stats.P_Stats.Balance -= 15000 * (P_Stats.P_Stats.rocketCarryWeight / 15);
        StartCoroutine(EnableButton(button, (100 * (P_Stats.P_Stats.rocketCarryWeight / 15)) / P_Stats.scientistCount));
    }

    public void UpgradeHull(Button button)
    {
        R_Stats.hullLevel += 1;
        P_Stats.P_Stats.Balance -= 55000 * R_Stats.hullLevel;
        button.interactable = false;
        StartCoroutine(EnableButton(button, (100 * R_Stats.hullLevel) / P_Stats.scientistCount));
    }

    public void UpgradeEngine(Button button)
    {
        R_Stats.rocketLevel += 1;
        P_Stats.P_Stats.Balance -= 35000 * R_Stats.hullLevel;
        button.interactable = false;
        StartCoroutine(EnableButton(button, (100 * R_Stats.rocketLevel) / P_Stats.scientistCount));
        Debug.Log("TRUE");
    }

    private IEnumerator EnableButton(Button button, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        button.interactable = true;
    }
}
