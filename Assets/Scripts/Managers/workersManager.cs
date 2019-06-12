using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class workersManager : MonoBehaviour
{
    public PlayerManager thePlayer;

    // HAAL HIER EEN SCRIPTABLE OBJECT OP OM DE SCIENTIST ETC OP TE HALEN. MISSCHIEN OOK HANDIG OM JE GAME OP TE SLAAN
    [Header("Scientists")]
    public GameObject ScientistUI;
    public Slider ScientistCounter;
    public TextMeshProUGUI ScientistText;

    [Header("Mechanics")]
    public GameObject MechanicUI;
    public Slider MechanicCounter;
    public TextMeshProUGUI MechanicText;

    [Header("Lauch")]
    public GameObject LaunchUI;
    // Start is called before the first frame update
    void Start()
    {
        ScientistCounter.maxValue = (thePlayer.defaultIncome / PlayerManager.scientistPrice);
        MechanicCounter.maxValue = (thePlayer.defaultIncome / PlayerManager.engineerPrice);
        ScientistCounter.value = thePlayer.scientistCount;
        MechanicCounter.value = thePlayer.engineerCount;


        ScientistUI.SetActive(false);
        MechanicUI.SetActive(false);
    }

    void Update()
    {
        thePlayer.scientistCount = Mathf.RoundToInt(ScientistCounter.value);
        thePlayer.engineerCount = Mathf.RoundToInt(MechanicCounter.value);
        ScientistText.text = thePlayer.scientistCount.ToString();
        MechanicText.text = thePlayer.engineerCount.ToString();
        // Debug.Log(thePlayer.income - ((thePlayer.scientistCount / PlayerManager.scientistPrice) + (thePlayer.mechanicCount / PlayerManager.mechanicPrice)));
        ScientistCounter.maxValue = (thePlayer.defaultIncome / PlayerManager.scientistPrice);
        MechanicCounter.maxValue = (thePlayer.defaultIncome / PlayerManager.engineerPrice);

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "MechanicsBuilding")
                {
                    if(!MechanicUI.activeSelf)
                        MechanicUI.SetActive(true);
                    else
                        MechanicUI.SetActive(false);
                    ScientistUI.SetActive(false);
                    LaunchUI.SetActive(false);
                }
                else if (hit.transform.name == "ScientistBuilding")
                {
                    if (!ScientistUI.activeSelf)
                        ScientistUI.SetActive(true);
                    else
                        ScientistUI.SetActive(false);
                    MechanicUI.SetActive(false);
                    LaunchUI.SetActive(false);
                }
                else if (hit.transform.name == "LaunchPad")
                {
                    if (!LaunchUI.activeSelf)
                        LaunchUI.SetActive(true);
                    else
                        LaunchUI.SetActive(false);
                    MechanicUI.SetActive(false);
                    ScientistUI.SetActive(false);
                }
            }
            else
            {
                ScientistUI.SetActive(false);
                MechanicUI.SetActive(false);
            }
        }
    }
}
