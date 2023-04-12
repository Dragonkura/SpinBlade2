using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreepWave;

[CreateAssetMenu(fileName = "CreepWaveConfig", menuName = "ScriptableObjects/CreepWaveConfig", order = 1)]

public class CreepWaveConfigEditor : ScriptableObject
{
    public List<WaveConfig> waveConfigs;
}
[System.Serializable]
public class WaveConfig
{
    public WaveTier waveTier;
    public List<CreepConfig> creeps;
}
[System.Serializable]
public class CreepConfig
{
    public CreepType creepType;
    public Creep creepPrefab;
}
