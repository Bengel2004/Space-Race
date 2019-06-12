using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoonManager : MonoBehaviour
{
    public int Colonists;
    public int supplies;
    public int happyness;
    public MoonStats theMoonStats;
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
    private void Awake()
    {
        supplies = theMoonStats.Supplies;
        Colonists = theMoonStats.Colonists;
        happyness = theMoonStats.Happyness;
    }

    void Start()
    {
        if(!theMoonStats.MainHubBuilt)
            MainHub.SetActive(false);
        if (!theMoonStats.TheatreBuilt)
            Theatre.SetActive(false);
        if (!theMoonStats.LabBuilt)
            Lab.SetActive(false);
        if (!theMoonStats.StorageBuilt)
            Storage.SetActive(false);
        if (!theMoonStats.LaunchPadBuilt)
            LaunchPad.SetActive(false);
        timestamp = Time.time + 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (theMoonStats.MainHubBuilt && !MainHub.activeSelf)
            MainHub.SetActive(true);
        if (theMoonStats.TheatreBuilt && !Theatre.activeSelf)
            Theatre.SetActive(true);
        if (theMoonStats.LabBuilt && !Lab.activeSelf)
            Lab.SetActive(true);
        if (theMoonStats.StorageBuilt && !Storage.activeSelf)
            Storage.SetActive(true);
        if (theMoonStats.LaunchPadBuilt && !LaunchPad.activeSelf)
            LaunchPad.SetActive(true);

        happyness = Mathf.Clamp(happyness, 0, 100);
        supplies = Mathf.Clamp(supplies, 0, 3000);

        theMoonStats.Supplies = supplies;
        theMoonStats.Colonists = Colonists;
        theMoonStats.Happyness = happyness;

        ColonistsText.text = "Colonists: " + Colonists;
        SuppliesText.text = "Supplies: " + supplies;
        HappynessText.text = "Happyness: " + happyness;



    }
}
