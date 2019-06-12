using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Launcher : MonoBehaviour
{
    public PlayerManager thePlayer;
    public LaunchType launchType;

    public static int defaultLaunchPrice;
    public int rocketCarryWeight = 30;
    public static float LaunchPrice;

    public int pricePerColonist;
    public int pricePercargoTons;

    public GameObject Rocket;
    public Transform RocketSpawnPoint;

    [Header("UI Components")]
    public GameObject LaunchUI;
    public GameObject ConstructionMenu;
    public GameObject OtherMenu;
    public Button LaunchButton;

    public TMP_Dropdown launchTypeDropdown;
    public TMP_Dropdown buildingTypeDropdown;
    public Slider Amount;
    public TextMeshProUGUI AmountText;
    public TextMeshProUGUI AmountType;


    AudioSource AudioSource;

    [Header("Explosion Components")]
    public GameObject ExplosionUI;
    public AudioClip ExplosionSound;
    float explosionDetermination;
    public float chanceOfExplosion = 50;
    int explosionCounter;

    [Header("Success Components")]
    public GameObject SuccessfullLaunchUI;
    public AudioClip CongratulationsSound;

    bool canLaunchAgain = true;

    // Start is called before the first frame update
    void Start()
    {
        LaunchUI.SetActive(false);
        OtherMenu.SetActive(false);
        rocketCarryWeight = thePlayer.P_Stats.rocketCarryWeight;
        AudioSource = GetComponent<AudioSource>();
        ExplosionUI.SetActive(false);
        SuccessfullLaunchUI.SetActive(false);

        //LETOP LETOP LETOP !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // ZET DE LAUNCH BUTTON UIT ALS DE SPELER NIET GENOEG MECHANICS HEEFT OM DE RAKET TE MAKEN. DIT SCHEELT WEER WAT AI PROGRAMMEER WERK.
    }

    // Update is called once per frame
    void Update()
    {

        AmountText.text = Amount.value.ToString();
        if (Input.GetKeyDown(KeyCode.L))
        {
            LaunchUI.SetActive(!LaunchUI.activeSelf);
            Amount.maxValue = rocketCarryWeight;
        }

        if(launchTypeDropdown.options[launchTypeDropdown.value].text == "Construct")
        {
            if (!ConstructionMenu.activeSelf)
            {
                ConstructionMenu.SetActive(true);
                OtherMenu.SetActive(false);
                float CheckPrice = defaultLaunchPrice;
                if (thePlayer.P_Stats.Balance > CheckPrice && canLaunchAgain)
                    LaunchButton.interactable = true;
                else
                    LaunchButton.interactable = false;
            }
        }
        else
        {
            if (!OtherMenu.activeSelf)
            {
                OtherMenu.SetActive(true);
                ConstructionMenu.SetActive(false);
            }
            if(launchTypeDropdown.options[launchTypeDropdown.value].text == "Supply")
            {
                if (AmountType.text != "Supplies")
                    AmountType.text = "Supplies";
                Amount.maxValue = Mathf.Clamp((thePlayer.defaultIncome / pricePercargoTons), 0, rocketCarryWeight);
                float CheckPrice = defaultLaunchPrice + pricePercargoTons * Amount.value;
                if (thePlayer.P_Stats.Balance > CheckPrice && canLaunchAgain)
                    LaunchButton.interactable = true;
                else
                    LaunchButton.interactable = false;
            }
            else if (launchTypeDropdown.options[launchTypeDropdown.value].text == "Colonize")
            {
                if (AmountType.text != "Colonists")
                    AmountType.text = "Colonists";
                Amount.maxValue = Mathf.Clamp((thePlayer.defaultIncome / pricePerColonist), 0, rocketCarryWeight);
                float CheckPrice = defaultLaunchPrice + pricePerColonist * Amount.value;
                if (thePlayer.P_Stats.Balance > CheckPrice && canLaunchAgain)
                    LaunchButton.interactable = true;
                else
                    LaunchButton.interactable = false;
            }
        }
    }
   
    public void Launch()
    {
        rocketCarryWeight = thePlayer.P_Stats.rocketCarryWeight;
        string TypeOf = launchTypeDropdown.options[launchTypeDropdown.value].text;

        explosionDetermination = Random.Range(0, 100f);

        defaultLaunchPrice = (75000 + (2000 * thePlayer.R_Stats.rocketLevel) + (500 * thePlayer.R_Stats.hullLevel));
        if (TypeOf == "Supply")
        {
            LaunchRocket(LaunchType.Supply, Amount.value, "", explosionDetermination, 60f); 
        }
        else if (TypeOf == "Construct")
        {
            string BuildingType = buildingTypeDropdown.options[buildingTypeDropdown.value].text;
            LaunchRocket(LaunchType.Construct, 0, BuildingType, explosionDetermination, 60f); // 60 dagen

        }
        else if (TypeOf == "Colonize")
        {
            LaunchRocket(LaunchType.Colonize, Amount.value, "", explosionDetermination, 60f);
        }
        LaunchButton.interactable = false;
        canLaunchAgain = false;
        StartCoroutine(ReadyToLaunch(45));
    }

    void LaunchRocket(LaunchType type, float Value, string Building, float explosionNumber, float speed)
    {
        Instantiate(Rocket, RocketSpawnPoint.transform.position, RocketSpawnPoint.transform.rotation);
        if (explosionDetermination > (((chanceOfExplosion / 2) / thePlayer.R_Stats.hullLevel) + ((chanceOfExplosion / 2) / (thePlayer.R_Stats.rocketLevel / 2)) + 10) || explosionCounter > Random.Range(2, 5))
        {
            explosionCounter = 0;
            if (type == LaunchType.Supply)
            {
                // Value == Aantal ton cargo
                LaunchPrice = defaultLaunchPrice + pricePercargoTons * Value;
                thePlayer.balance -= Mathf.RoundToInt(LaunchPrice);
                thePlayer.M_Stats.Supplies =+ Mathf.RoundToInt(Value);
                StartCoroutine(Resource_ETA(type, Value, speed));
            }
            else if (type == LaunchType.Construct)
            {
                LaunchPrice = defaultLaunchPrice + Value;
                thePlayer.balance -= Mathf.RoundToInt(LaunchPrice);
                StartCoroutine(Resource_ETA(type, Value, speed));
                switch (Building)
                {
                    case "MainHub":
                        thePlayer.M_Stats.MainHubBuilt = true;
                        break;
                    case "Theatre":
                        thePlayer.M_Stats.TheatreBuilt = true;
                        break;
                    case "Lab":
                        thePlayer.M_Stats.LabBuilt = true;
                        break;
                    case "Storage":
                        thePlayer.M_Stats.StorageBuilt = true;
                        break;
                    case "LaunchPad":
                        thePlayer.M_Stats.LaunchPadBuilt = true;
                        break;
                    default:
                        Debug.Log("NO BUILDING FOUND");
                        break;
                }
            }
            else if (type == LaunchType.Colonize)
            {
                LaunchPrice = defaultLaunchPrice + pricePerColonist * Value;
                thePlayer.balance -= Mathf.RoundToInt(LaunchPrice);
                StartCoroutine(Consruction_ETA(Building, speed));
                thePlayer.M_Stats.Colonists = +Mathf.RoundToInt(Value);
            }
        }
        else
        {
            StartCoroutine(Explosion(40));
            explosionCounter++;
        }
        thePlayer.updateBalance();
    }

    private IEnumerator ReadyToLaunch(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        LaunchButton.interactable = true;
        canLaunchAgain = true;
    }

    private IEnumerator Consruction_ETA(string Building, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RewardPlayer();
        switch (Building)
        {
            case "MainBuilding":

                break;
            default:
                Debug.Log("ERROR NO BUILDING SELECTED");
                break;
        }
        // bouw hier dat ene gebouw. Misschien kan je dat gewoon met een bool doen en scriptable object en gebouwen "Uitzetten".
    }


    private IEnumerator Resource_ETA(LaunchType type, float value, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RewardPlayer();
        if (type == LaunchType.Supply)
        {
            // check ook nog of t ruimteschip bestaat na deze timer, zo niet dan moet hij deleted worden. Of pak t anders aan.
            // Voeg aan het scriptable Object van de maan de nieuwe waardes toe aan supplies;
            // Voeg ook happyness toe omdat ze weer te eten krijgen.
        }
        else if (type == LaunchType.Construct)
        {
            // Voeg aan het scriptable object van de maan de nieuwe colonisten waardes toe.
        }
    }

    void RewardPlayer()
    {
        thePlayer.defaultIncome += 6000;
        AudioSource.PlayOneShot(CongratulationsSound);
        SuccessfullLaunchUI.SetActive(true);
    }

    private IEnumerator Explosion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ExplosionUI.SetActive(true);
        AudioSource.PlayOneShot(ExplosionSound);
        thePlayer.defaultIncome -= 2000;
        // doe hier t explosie geluid ding, doe gewoon iets met UI of zo en een knal
    }
}

public enum LaunchType{
    Colonize,
    Supply,
    Construct
}
