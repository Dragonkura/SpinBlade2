using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepWave : MonoBehaviour
{
    public CreepWaveConfigEditor creepConfig;
    public float distance;
    public WaveTier waveTier;
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<Creep> creeps = new List<Creep>();
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
            var  creep = Instantiate(config[i].creepPrefab, spawnPoint);
            creep.transform.SetLocalPosFromAngelAndDistance((360f / creepAmount) * (i + 1) * Mathf.Deg2Rad, distance);
            creep.startPos = creep.transform.position;
            creeps.Add(creep);
        }
    }
    [SerializeField] bool isArgoing;
    private void OnTriggerStay(Collider other)
    {
        if (isArgoing) return;
        if (other.gameObject.tag == TagNameConst.PLAYER)
        {
            foreach (var cp in creeps)
            {
                cp.target = other.gameObject.transform;
                cp.isArgo = true;
            }
            isArgoing = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagNameConst.PLAYER)
        {
            foreach (var cp in creeps)
            {
                cp.isArgo = false;
            }
            isArgoing = false;
        }
    }
  
    
}
