using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ResearchProgress", menuName = "ResearchProgress")]
public class ResearchProgress : ScriptableObject
{
    public int rocketLevel = 1;
    public int hullLevel = 1;
}

