using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Playerstats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int Balance = 30000;
    public int DefaultIncome = 30000;
    public int Scientists = 0;
    public int Engineers = 0;
    public int rocketCarryWeight = 30;

}
