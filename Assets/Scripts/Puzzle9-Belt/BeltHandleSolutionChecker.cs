using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BeltHandleSolutionChecker : NetworkSolutionChecker
{
    public override bool CheckSolution() {
        foreach (var item in solutionItems) {
            if (!item.GetComponent<HandleButtonInteract>().isSolved) {
                return false;
            }
        }
        return true;
    }
}
