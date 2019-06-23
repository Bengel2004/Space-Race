using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Playerstats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int Balance = 30000;
    public int DefaultIncome = 30000;
    public int Scientists = 1;
    public int Engineers = 1;
    public int rocketCarryWeight = 30;

}
