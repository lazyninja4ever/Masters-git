using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraSpawner : MonoBehaviour
{
    public GameObject hydraHead;
    public GameObject hydraNeck;
    [SerializeField] private DependantInteract setSolveState;
    public bool spawnHydraAtStart;
    public HydraManager manager;
    // Start is called before the first frame update
    private void Awake() {
        setSolveState = GetComponent<DependantInteract>();
        if (!spawnHydraAtStart)
            setSolveState.isSolved = true;
        if (spawnHydraAtStart)
            SpawnHydra();

            
    }

    public void SpawnHydra() {
        //add new hydra head as child
        //set as not solved
        setSolveState.isSolved = false;
        GameObject newHead = Instantiate(hydraHead, this.gameObject.transform);
        manager.moveToOccu(GetComponent<HydraSpawner>());
    }

    public void Decapitate() {
        // add new hydraneck
        GameObject newNeck = Instantiate(hydraNeck, this.gameObject.transform);
        
    }

    public void KillHydra() {
        //set as solved
        setSolveState.isSolved = true;
        setSolveState.solutionChecker.PuzzleState();
        manager.moveToFree(GetComponent<HydraSpawner>());
    }

    public void SpawnNewHead() {
        manager.chooseRandom();
    }
}
