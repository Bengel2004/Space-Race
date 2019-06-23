using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerManager thePlayer;
    public static int supplies;
    public static int Colonists;
    public static int happyness;

    public GameObject GameOverScreen;
    public TextMeshProUGUI GameOverText;

    public GameObject YouWonScreen;


    public static bool MainHubBuilt;
    public static bool TheatreBuilt;
    public static bool LabBuilt;
    public static bool StorageBuilt;
    public static bool LaunchPadBuilt;

    private float Timestamp = 0.0f;
    bool IsPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        GameOverScreen.SetActive(false);
        YouWonScreen.SetActive(false);
        supplies = thePlayer.M_Stats.Supplies;
        Colonists = thePlayer.M_Stats.Colonists;
        happyness = thePlayer.M_Stats.Happyness;
        Timestamp = Time.time + 60f;
    }

    // Update is called once per frame
    void Update()
    {
        /*       
         // Cheats ZET ZE NIET UIT
        thePlayer.balance = 5000000;
        GameManager.Colonists = 180;
        GameManager.happyness = 100;
        GameManager.supplies = 1000000; 
        GameManager.MainHubBuilt = true;
        GameManager.LaunchPadBuilt = true;
        GameManager.StorageBuilt = true;
        GameManager.LabBuilt = true;
        GameManager.TheatreBuilt = true;  
        */

        if (IsPlaying)
        {

            happyness = Mathf.Clamp(happyness, 0, 100);
            supplies = Mathf.Clamp(supplies, 0, 3000);

            if (happyness <= 0 || thePlayer.balance <= 0)
            {
                Time.timeScale = 0;
                GameOverScreen.SetActive(true);
                if(thePlayer.balance <= 0)
                    GameOverText.text = "You have gone bankrupt and can no longer provide food for your colonists and employees.";
                else if (happyness <= 0)
                    GameOverText.text = "The people on the Moon have rioted against you and now everything is lost.";
            }



            if (Colonists > 0 && supplies > 0)
            {
                if (Time.time > Timestamp)
                {
                    Timestamp = Time.time + 60;
                    supplies -= Colonists / 4;
                    happyness += (happyness / 4);
                }
            }
            else if (Colonists > 0 && supplies == 0)
            {
                if (Time.time > Timestamp)
                {
                    Timestamp = Time.time + 60;
                    happyness -= (Colonists / 4);
                    thePlayer.defaultIncome -= Colonists * 100;
                }
            }
        }

}

    public void GameOver()
    {
        // dit is gewoon een makkelijke reset
        IsPlaying = false;
        supplies = thePlayer.M_Stats.Supplies;
        Colonists = thePlayer.M_Stats.Colonists;
        happyness = thePlayer.M_Stats.Happyness;
        MainHubBuilt = false;
        TheatreBuilt = false;
        LabBuilt = false;
        StorageBuilt = false;
        LaunchPadBuilt = false;
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }

    public void GameWon()
    {
        StartCoroutine(OpenGameWonUI(15f));

    }
    IEnumerator OpenGameWonUI(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        YouWonScreen.SetActive(true);
    }
}
