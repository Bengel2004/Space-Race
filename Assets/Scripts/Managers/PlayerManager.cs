using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats P_Stats;
    public MoonStats M_Stats;
    public ResearchManager R_Stats;

    public int defaultIncome = 30000;
    public int income;
    public int balance;

    public int scientistCount;
    public int engineerCount;

    public int researchSpeed = 1;
    public int constructionSpeed = 1;

    public static int scientistPrice = 450;
    public static int engineerPrice = 250;


    float timestamp = 0.0f;
    public static float MonthToSeconds = 60f;
        
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI incomeText;
        
    // SLA DEZE WAARDES OP IN EEN SCRIPTABLE OBJECT


    public static PlayerManager Instance { get; private set; }

    private void Awake()
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
        timestamp = Time.time + 60f;
        income = 30000;

        defaultIncome = P_Stats.DefaultIncome;
        balance = P_Stats.Balance;
        scientistCount = P_Stats.Scientists;
        engineerCount = P_Stats.Engineers;
    }

    void Update()
    {
        if (Time.time > timestamp)
        {
            timestamp = Time.time + MonthToSeconds;
            balance += Mathf.RoundToInt(income);
        }
        income = (defaultIncome - ((scientistCount * scientistPrice) + (engineerCount * engineerPrice)));

        if (Mathf.Abs(balance) > 1000)
            balanceText.text = "Balance: $" + (balance / 1000).ToString() + "k";
        else
            balanceText.text = "Balance: $" + balance.ToString();

        if (Mathf.Abs(income) > 1000)
            incomeText.text = "Income: $" + (income / 1000).ToString() + "k";
        else
            incomeText.text = "Income: $" + (income).ToString();
    }
}
