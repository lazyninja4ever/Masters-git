using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraManager : MonoBehaviour
{
    [SerializeField] List<HydraSpawner> spawnersFree;
    [SerializeField] List<HydraSpawner> spawnersOccu;

    public void moveToFree(HydraSpawner moveHS) {
        spawnersOccu.Remove(moveHS);
        spawnersFree.Add(moveHS);
    }

    public void moveToOccu(HydraSpawner moveHS) {
        spawnersFree.Remove(moveHS);
        spawnersOccu.Add(moveHS);
    }

    public void chooseRandom() {
        if (spawnersFree.Count > 0) { 
            int randomSpawner = Random.Range(0, spawnersFree.Count);
            spawnersFree[randomSpawner].SpawnHydra();
        }
    }
}

