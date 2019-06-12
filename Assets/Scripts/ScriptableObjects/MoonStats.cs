using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MoonStats", menuName = "MoonStats")]
public class MoonStats : ScriptableObject
{
    public int Colonists = 0;
    public int Happyness = 50;
    public int Supplies = 0;

    public bool MainHubBuilt;
    public bool TheatreBuilt;
    public bool LabBuilt;
    public bool StorageBuilt;
    public bool LaunchPadBuilt;
}
