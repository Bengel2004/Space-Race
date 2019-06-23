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
    public GameObject MarsLaunchUI;

    [Header("Cursors")]
    public Texture2D mouseCursor;
    public Texture2D pointCursur;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
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


        if (EventSystem.current.IsPointerOverGameObject()) {
            Cursor.SetCursor(mouseCursor, hotSpot, cursorMode);
            return;
    }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "MechanicsBuilding")
            {
                Cursor.SetCursor(pointCursur, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    if (!MechanicUI.activeSelf)
                        MechanicUI.SetActive(true);
                    else
                        MechanicUI.SetActive(false);
                    ScientistUI.SetActive(false);
                    LaunchUI.SetActive(false);
                }
            }
            else if (hit.transform.name == "ScientistBuilding")
            {
                Cursor.SetCursor(pointCursur, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    if (!ScientistUI.activeSelf)
                    ScientistUI.SetActive(true);
                else
                    ScientistUI.SetActive(false);
                    MechanicUI.SetActive(false);
                    LaunchUI.SetActive(false);
                }
            }
            else if (hit.transform.name == "LaunchPad")
            {
                Cursor.SetCursor(pointCursur, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    if (!LaunchUI.activeSelf)
                        LaunchUI.SetActive(true);
                    else
                        LaunchUI.SetActive(false);
                    MechanicUI.SetActive(false);
                    ScientistUI.SetActive(false);
                }
                }
            else if (hit.transform.name == "MarsLauncher")
            {
                Cursor.SetCursor(pointCursur, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    if (!MarsLaunchUI.activeSelf)
                        MarsLaunchUI.SetActive(true);
                    else
                        MarsLaunchUI.SetActive(false);
                }
            }
            else
            {
                Cursor.SetCursor(mouseCursor, hotSpot, cursorMode);
            }
                
        }
        else
        {
            Cursor.SetCursor(mouseCursor, hotSpot, cursorMode);
            ScientistUI.SetActive(false);
            MechanicUI.SetActive(false);
        }
    }
    
}
