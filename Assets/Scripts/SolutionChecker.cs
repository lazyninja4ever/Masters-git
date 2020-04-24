using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionChecker : MonoBehaviour
{
    public List<GameObject> solutionItems;
    public PriceReveiler solvedRun;
    
    public void PuzzleState() {
        bool solutionDone = CheckSolution();
        if (solutionDone) {
            Debug.Log("Puzzle is solved");
            solvedRun.ReveilPrice();

        } else {
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
