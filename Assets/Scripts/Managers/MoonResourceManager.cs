using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonResourceManager : MonoBehaviour
{
    public MoonStats theMoon;

    public int consumptionRate;

    public float Timestamp = 0.0f;

    public static MoonResourceManager Instance { get; private set; }

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

    void Start()
    {
        Timestamp = Time.time + 0.0f;
    }

    void Update()
    {
        if(Time.time > Timestamp)
        {
            Timestamp = Time.time + PlayerManager.MonthToSeconds;
            if (theMoon.Colonists > 0)
            {
                if (theMoon.Supplies > 0)
                {
                    theMoon.Supplies = theMoon.Supplies - (theMoon.Colonists * consumptionRate);
                    theMoon.Happyness += theMoon.Colonists / 4;
                }
                else
                {
                    theMoon.Happyness -= theMoon.Colonists / 2;
                }
            }
        }
    }
}
