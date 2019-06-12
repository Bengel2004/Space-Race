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
    public static float MonthToSeconds = 60f;

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
        P_Stats.Scientists = scientistCount;
        P_Stats.Engineers = engineerCount;
        P_Stats.DefaultIncome = defaultIncome;

        if (Time.time > timestamp)
        {
            timestamp = Time.time + MonthToSeconds;
            income = (defaultIncome - ((scientistCount * scientistPrice) + (engineerCount * engineerPrice)));
            P_Stats.Balance += Mathf.RoundToInt(income);

            if (balance > 1000)
                balanceText.text = "Balance: $" + (P_Stats.Balance / 1000).ToString() + "k";
            else
                balanceText.text = "Balance: $" + P_Stats.Balance.ToString();

            if (income > 1000)
                incomeText.text = "Income: $" + (income / 1000).ToString() + "k";
            else
                incomeText.text = "Income: $" + (income).ToString();
        }
    }

    public void updateBalance()
    {
        if (balance > 1000)
            balanceText.text = "Balance: $" + (P_Stats.Balance / 1000).ToString() + "k";
        else
            balanceText.text = "Balance: $" + P_Stats.Balance.ToString();

        if (income > 1000)
            incomeText.text = "Income: $" + (income / 1000).ToString() + "k";
        else
            incomeText.text = "Income: $" + (income).ToString();
    }
}
