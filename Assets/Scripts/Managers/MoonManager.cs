using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoonManager : MonoBehaviour
{
    public GameObject MainHub;
    public GameObject Theatre;
    public GameObject Lab;
    public GameObject Storage;
    public GameObject LaunchPad;

    public TextMeshProUGUI ColonistsText;
    public TextMeshProUGUI SuppliesText;
    public TextMeshProUGUI HappynessText;

    float timestamp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(!GameManager.MainHubBuilt)
            MainHub.SetActive(false);
        if (!GameManager.TheatreBuilt)
            Theatre.SetActive(false);
        if (!GameManager.LabBuilt)
            Lab.SetActive(false);
        if (!GameManager.StorageBuilt)
            Storage.SetActive(false);
        if (!GameManager.LaunchPadBuilt)
            LaunchPad.SetActive(false);
        timestamp = Time.time + 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.MainHubBuilt && !MainHub.activeSelf)
            MainHub.SetActive(true);
        if (GameManager.TheatreBuilt && !Theatre.activeSelf)
            Theatre.SetActive(true);
        if (GameManager.LabBuilt && !Lab.activeSelf)
            Lab.SetActive(true);
        if (GameManager.StorageBuilt && !Storage.activeSelf)
            Storage.SetActive(true);
        if (GameManager.LaunchPadBuilt && !LaunchPad.activeSelf)
            LaunchPad.SetActive(true);

        ColonistsText.text = "Colonists: " + GameManager.Colonists.ToString();
        SuppliesText.text = "Supplies: " + GameManager.supplies.ToString();
        HappynessText.text = "Happyness: " + GameManager.happyness.ToString();
    }
}
