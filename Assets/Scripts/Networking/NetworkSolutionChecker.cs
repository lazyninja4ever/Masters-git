using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkSolutionChecker : NetworkBehaviour
{
    public List<GameObject> solutionItems;
    public NetworkReveiler solvedRun;

    public void PuzzleState() {
        if (!isServer) return; //only check solution on server


        bool solutionDone = CheckSolution();
        if (solutionDone) {
            Debug.Log("Puzzle is solved");
            solvedRun.ReveilPrice();
        }
        else {
            Debug.Log("Puzzle is NOT solved");
        }

    }

    public virtual bool CheckSolution() {
        foreach (var item in solutionItems) {
            if (!item.GetComponent<DependantInteract>().isSolved) {
                return false;
            }
        }
        return true;
    }
}
