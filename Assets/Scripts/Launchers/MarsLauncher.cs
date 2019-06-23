using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MarsLauncher : MonoBehaviour
{
    public PlayerManager thePlayer;
    public GameObject Rocket;
    public Transform RocketSpawnPoint;
    public int UltimateLaunchPrice = 500000;

    public Button LaunchRocketButton;
    public GameObject UIWarning;
    public GameObject UIGo;
    public GameObject MarsLaunchUI;
    // Start is called before the first frame update
    void Start()
    {
        LaunchRocketButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (SceneManager.GetActiveScene().name == "MoonScene")
        {
            if (RocketSpawnPoint == null && GameManager.LaunchPadBuilt)
                RocketSpawnPoint = GameObject.Find("RocketSpawnPoint").transform;

            if (thePlayer.balance >= UltimateLaunchPrice && GameManager.Colonists >= 150 && GameManager.happyness >= 50 && GameManager.MainHubBuilt == true
                && GameManager.TheatreBuilt == true && GameManager.LabBuilt == true && GameManager.StorageBuilt == true && GameManager.LaunchPadBuilt == true)
            {
                LaunchRocketButton.interactable = true;

                if (UIWarning.activeSelf)
                {
                    UIWarning.SetActive(false);
                    UIGo.SetActive(true);
                }
            }
            else
            {
                if (!UIWarning.activeSelf)
                {
                    UIWarning.SetActive(true);
                    UIGo.SetActive(false);
                }
            }
        }
        else
        {
            if (MarsLaunchUI.activeSelf)
            {
                MarsLaunchUI.SetActive(false);
            }
        }
    }

    public void Launch()
    {
        Instantiate(Rocket, RocketSpawnPoint.transform.position, RocketSpawnPoint.transform.rotation);
        thePlayer.balance = thePlayer.balance - UltimateLaunchPrice;
    }
}
