using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour
{
    public PlayerManager P_Stats;

    public Button CargoUpgradeBt;
    public TextMeshProUGUI CargoLevel;
    public TextMeshProUGUI CargoPrice;

    public Button HullUpgradeBt;
    public TextMeshProUGUI HullLevelText;
    public TextMeshProUGUI HullPrice;

    public Button EngineUpgradeBt;
    public TextMeshProUGUI EngineLevel;
    public TextMeshProUGUI EnginePrice;


    int defaultCargoPrice = 15000;
    int defaultHullPrice = 55000;
    int defaultEnginePrice = 35000;

    public int CarryWeight = 30;
    public int HullLevel = 1;
    public int RocketLevel = 1;

    private void OnEnable()
    {

        CargoLevel.text = "Cargo Research Capacity " + CarryWeight;
        CargoPrice.text = "Upgrade Price " + (defaultCargoPrice * (CarryWeight / 15)).ToString();

        HullLevelText.text = "Hull Research Level " + HullLevel;
        HullPrice.text = "Upgrade Price " + (defaultHullPrice * RocketLevel).ToString();

        EngineLevel.text = "Engine Research Level " + RocketLevel;
        EnginePrice.text = "Upgrade Price " + (defaultEnginePrice * HullLevel).ToString();
    }
    // RESEARCH IS NU NOG INSTANT, FIX DIT
    // fix de research over scene + check of je genoeg geld hebt voor research.
    private void Update()
    {
        if (P_Stats.balance > defaultCargoPrice * (P_Stats.R_Stats.CarryWeight / 15))
            CargoUpgradeBt.interactable = true;
        else
            CargoUpgradeBt.interactable = false;

        if (P_Stats.balance > defaultHullPrice * HullLevel)
            HullUpgradeBt.interactable = true;
        else
            HullUpgradeBt.interactable = false;


        if (P_Stats.balance > defaultEnginePrice * RocketLevel)
            EngineUpgradeBt.interactable = true;
        else
            EngineUpgradeBt.interactable = false;
    }

    public void UpgradeCargo(Button button)
    {
        
        button.interactable = false;
        P_Stats.balance -= defaultCargoPrice * (CarryWeight / 15);
        CarryWeight += 15;
        StartCoroutine(EnableButton(button, (100 * (CarryWeight / 15)) / P_Stats.scientistCount));

        CargoLevel.text = "Cargo Research Capacity " + CarryWeight;
        CargoPrice.text = "Upgrade Price " + (defaultCargoPrice * (CarryWeight / 15)).ToString();
    }

    public void UpgradeHull(Button button)
    {
        P_Stats.balance -= defaultHullPrice * HullLevel;
        HullLevel += 1;
        button.interactable = false;
        StartCoroutine(EnableButton(button, (100 * HullLevel) / P_Stats.scientistCount));

        HullLevelText.text = "Hull Research Level " + HullLevel;
        HullPrice.text = "Upgrade Price " + (defaultHullPrice * RocketLevel).ToString();
    }

    public void UpgradeEngine(Button button)
    {
        P_Stats.balance -= defaultEnginePrice * RocketLevel;
        RocketLevel += 1;
        button.interactable = false;
        StartCoroutine(EnableButton(button, (100 * RocketLevel) / P_Stats.scientistCount));

        EngineLevel.text = "Engine Research Level " + RocketLevel;
        EnginePrice.text = "Upgrade Price " + (defaultEnginePrice * HullLevel).ToString();
    }

    private IEnumerator EnableButton(Button button, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        button.interactable = true;
    }
}
