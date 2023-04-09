using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepWave : MonoBehaviour
{
    public CreepWaveConfigEditor creepConfig;
    public float distance;
    public WaveTier waveTier;
    [SerializeField] Transform spawnPoint;
    private void Start()
    {
        SpawnCreepWave();
    }
    public enum WaveTier
    {
        Tier1,
        Tier2,
        Tier3,
    }
    public void SpawnCreepWave()
    {
        var config = creepConfig.waveConfigs.Find(x => x.waveTier == waveTier).creeps;
        var creepAmount = config.Count;
        for (int i = 0; i < config.Count; i++)
        {
            var  obj = Instantiate(config[i].creepPrefab, spawnPoint);
            obj.transform.SetLocalPosFromAngelAndDistance((360f / creepAmount) * (i + 1) * Mathf.Deg2Rad, distance);
        }
    }
}
