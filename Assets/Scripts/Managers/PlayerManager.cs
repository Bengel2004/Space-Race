using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats P_Stats;
    public MoonStats M_Stats;
    public ResearchProgress R_Stats;

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
    public float MonthToSeconds;

    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI incomeText;

    // SLA DEZE WAARDES OP IN EEN SCRIPTABLE OBJECT

    private void Awake()
    {
        defaultIncome = P_Stats.DefaultIncome;
        balance = P_Stats.Balance;
        scientistCount = P_Stats.Scientists;
        engineerCount = P_Stats.Engineers;
    }

    void Start()
    {
        timestamp = Time.time + 0f;
        income = 30000;
    }

    void Update()
    {
        P_Stats.Balance = balance;
        P_Stats.Scientists = scientistCount;
        P_Stats.Engineers = engineerCount;
        P_Stats.DefaultIncome = defaultIncome;

        if (Time.time > timestamp)
        {
            timestamp = Time.time + MonthToSeconds;
            income = (defaultIncome - ((scientistCount * scientistPrice) + (engineerCount * engineerPrice)));
            balance += Mathf.RoundToInt(income);
            Debug.Log(income);
            if (balance > 1000)
            {
                balanceText.text = "Balance: $" + (balance / 1000).ToString() + "k";
                incomeText.text = "Income: $" + (income / 1000).ToString() + "k";
            }
            else
            {
                balanceText.text = "Balance: $" + balance.ToString();
                incomeText.text = "Income: $" + (income).ToString();
            }
        }
    }

    public void updateBalance()
    {
        balanceText.text = "Balance: $" + (balance / 1000).ToString() + "k";
    }
}
