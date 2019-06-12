using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsResetManager : MonoBehaviour
{
    public PlayerStats P_Current;
    public MoonStats M_Current;
    public ResearchProgress R_Current;

    public PlayerStats P_Default;
    public MoonStats M_Default;
    public ResearchProgress R_Default;



    public static StatsResetManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

    }
    
    public void ResetPlayerStats()
    {
        P_Current.Balance = P_Default.Balance;
        P_Current.DefaultIncome = P_Default.DefaultIncome;
        P_Current.Scientists = P_Default.Scientists;
        P_Current.Engineers = P_Default.Engineers;
        P_Current.rocketCarryWeight = P_Default.rocketCarryWeight;

        M_Current.Colonists = M_Default.Colonists;
        M_Current.Happyness = M_Default.Happyness;
        M_Current.Supplies = M_Default.Supplies;
        M_Current.MainHubBuilt = M_Default.MainHubBuilt;
        M_Current.TheatreBuilt = M_Default.TheatreBuilt;
        M_Current.LabBuilt = M_Default.LabBuilt;
        M_Current.StorageBuilt = M_Default.StorageBuilt;
        M_Current.LaunchPadBuilt = M_Default.LaunchPadBuilt;

        R_Current.hullLevel = R_Default.hullLevel;
        R_Current.rocketLevel = R_Default.rocketLevel;
    }
}
