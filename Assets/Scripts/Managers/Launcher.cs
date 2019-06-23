using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public Button ToMoonButton;

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

    int SliderValue;
    // Start is called before the first frame update
    void Start()
    {
        LaunchUI.SetActive(false);
        OtherMenu.SetActive(false);
        AudioSource = GetComponent<AudioSource>();
        ExplosionUI.SetActive(false);
        SuccessfullLaunchUI.SetActive(false);
        defaultLaunchPrice = (45000 + (2000 * thePlayer.R_Stats.RocketLevel) + (500 * thePlayer.R_Stats.HullLevel));


        RocketSpawnPoint = GameObject.Find("RocketSpawnPoint").transform;
        //LETOP LETOP LETOP !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // ZET DE LAUNCH BUTTON UIT ALS DE SPELER NIET GENOEG MECHANICS HEEFT OM DE RAKET TE MAKEN. DIT SCHEELT WEER WAT AI PROGRAMMEER WERK.
    }

    // Update is called once per frame
    void Update()
    {
        SliderValue = Mathf.RoundToInt(Amount.value);
        if (SceneManager.GetActiveScene().name == "EarthScene")
        {
            if(RocketSpawnPoint == null)
                RocketSpawnPoint = GameObject.Find("RocketSpawnPoint").transform;
        } 

        AmountText.text = Amount.value.ToString();
        if (Input.GetKeyDown(KeyCode.L))
        {
            LaunchUI.SetActive(!LaunchUI.activeSelf);
            Amount.maxValue = thePlayer.R_Stats.CarryWeight;
        }
        if (LaunchUI.activeSelf)
        {

            ConstructionMenu.SetActive(true);
            OtherMenu.SetActive(false);
            defaultLaunchPrice = (45000 + (2000 * thePlayer.R_Stats.RocketLevel) + (500 * thePlayer.R_Stats.HullLevel));
            float CheckPrice = defaultLaunchPrice;
            if (thePlayer.balance > CheckPrice && canLaunchAgain)
                LaunchButton.interactable = true;
            else
                LaunchButton.interactable = false;


            if (launchTypeDropdown.options[launchTypeDropdown.value].text == "Construct")
            {
                if (!ConstructionMenu.activeSelf)
                {
                    /*
                    ConstructionMenu.SetActive(true);
                    OtherMenu.SetActive(false);
                    defaultLaunchPrice = (45000 + (2000 * thePlayer.R_Stats.rocketLevel) + (500 * thePlayer.R_Stats.hullLevel));
                    float CheckPrice = defaultLaunchPrice;
                    if (thePlayer.P_Stats.Balance > CheckPrice && canLaunchAgain)
                        LaunchButton.interactable = true;
                    else
                        LaunchButton.interactable = false; */
                }
            }
            else
            {
                if (!OtherMenu.activeSelf)
                {
                    OtherMenu.SetActive(true);
                    ConstructionMenu.SetActive(false);
                }
                if (launchTypeDropdown.options[launchTypeDropdown.value].text == "Supply")
                {
                    if (AmountType.text != "Supplies")
                        AmountType.text = "Supplies";
                    Amount.maxValue = Mathf.Clamp((thePlayer.defaultIncome / pricePercargoTons), 0, thePlayer.R_Stats.CarryWeight);
                /*    float CheckPrice = defaultLaunchPrice + pricePercargoTons * Amount.value;
                    if (thePlayer.P_Stats.Balance > CheckPrice && canLaunchAgain)
                        LaunchButton.interactable = true;
                    else
                        LaunchButton.interactable = false; */
                }
                else if (launchTypeDropdown.options[launchTypeDropdown.value].text == "Colonize")
                {
                    if (AmountType.text != "Colonists")
                        AmountType.text = "Colonists";
                    Amount.maxValue = Mathf.Clamp((thePlayer.defaultIncome / pricePerColonist), 0, thePlayer.R_Stats.CarryWeight);
                /*    float CheckPrice = defaultLaunchPrice + pricePerColonist * Amount.value;
                    if (thePlayer.P_Stats.Balance > CheckPrice && canLaunchAgain)
                        LaunchButton.interactable = true;
                    else
                        LaunchButton.interactable = false; */
                }
            }
        }
    }
   
    public void Launch()
    {
        rocketCarryWeight = thePlayer.R_Stats.CarryWeight + (thePlayer.engineerCount);
        float TypeOf = launchTypeDropdown.value;


        explosionDetermination = Random.Range(0, 100f);

        Debug.Log(TypeOf);

        defaultLaunchPrice = (45000 + (2000 * thePlayer.R_Stats.RocketLevel) + (500 * thePlayer.R_Stats.HullLevel));
        if (TypeOf == 1)
        {
            LaunchRocket(LaunchType.Supply, SliderValue, "", explosionDetermination, 60f); 
        }
        else if (TypeOf == 0)
        {
            string BuildingType = buildingTypeDropdown.options[buildingTypeDropdown.value].text;
            LaunchRocket(LaunchType.Construct, 0, BuildingType, explosionDetermination, 60f); // 60 dagen

        }
        else if (TypeOf == 2)
        {
            LaunchRocket(LaunchType.Colonize, SliderValue, "", explosionDetermination, 60f);
        }
        LaunchButton.interactable = false;
        canLaunchAgain = false;
        ToMoonButton.gameObject.SetActive(false);
        StartCoroutine(ReadyToLaunch(45));
    }

    void LaunchRocket(LaunchType type, int Value, string Building, float explosionNumber, float speed)
    {
        Instantiate(Rocket, RocketSpawnPoint.transform.position, RocketSpawnPoint.transform.rotation);
        explosionCounter = 7;
        if (explosionDetermination > (((chanceOfExplosion / 2) / thePlayer.R_Stats.HullLevel) + ((chanceOfExplosion / 2) / (thePlayer.R_Stats.RocketLevel / 2)) + 10) || explosionCounter > Random.Range(2, 5))
        {
            explosionCounter = 0;
            if (type == LaunchType.Supply)
            {
                LaunchPrice = defaultLaunchPrice + pricePercargoTons * Value;
                thePlayer.balance = thePlayer.balance - Mathf.RoundToInt(LaunchPrice);
                GameManager.supplies += Value;
                Debug.Log(GameManager.supplies + " Supplies");
                StartCoroutine(RewardPlayerTimer( speed));
            }
            else if (type == LaunchType.Construct)
            {
                LaunchPrice = defaultLaunchPrice + Value;
                thePlayer.balance = thePlayer.balance - Mathf.RoundToInt(LaunchPrice);
                StartCoroutine(RewardPlayerTimer( speed));
                for (int x = 0; x < buildingTypeDropdown.options.Count; x++)
                {
                    if (buildingTypeDropdown.options[x].text == Building)
                    {
                        buildingTypeDropdown.options.Remove(buildingTypeDropdown.options[x]);
                        break;
                    }
                }
                buildingTypeDropdown.value = 0;
                buildingTypeDropdown.RefreshShownValue();

                switch (Building)
                {
                    case "MainHub":
                        GameManager.MainHubBuilt = true;
                        break;
                    case "Theatre":
                        GameManager.TheatreBuilt = true;
                        break;
                    case "Lab":
                        GameManager.LabBuilt = true;
                        break;
                    case "Storage":
                        GameManager.StorageBuilt = true;
                        break;
                    case "LaunchPad":
                        GameManager.LaunchPadBuilt = true;
                        break;
                    default:
                        Debug.Log("NO BUILDING FOUND");
                        break;
                }
            }
            else if (type == LaunchType.Colonize)
            {
                LaunchPrice = defaultLaunchPrice + pricePerColonist * Value;
                thePlayer.balance = thePlayer.balance - Mathf.RoundToInt(LaunchPrice);
                GameManager.Colonists += Value;
                StartCoroutine(RewardPlayerTimer(speed));
            }
        }
        else
        {
            StartCoroutine(Explosion(40));
            explosionCounter++;
        }
    }

    private IEnumerator ReadyToLaunch(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        LaunchButton.interactable = true;
        canLaunchAgain = true;
    }

   


    private IEnumerator RewardPlayerTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RewardPlayer();
    }

    void RewardPlayer()
    {
        thePlayer.defaultIncome += 6000;
        AudioSource.PlayOneShot(CongratulationsSound);
        SuccessfullLaunchUI.SetActive(true);
        ToMoonButton.gameObject.SetActive(true);
    }

    private IEnumerator Explosion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ExplosionUI.SetActive(true);
        AudioSource.PlayOneShot(ExplosionSound);
        thePlayer.defaultIncome -= 2000;
        ToMoonButton.gameObject.SetActive(true);
        // doe hier t explosie geluid ding, doe gewoon iets met UI of zo en een knal
    }
}

public enum LaunchType{
    Colonize,
    Supply,
    Construct
}
