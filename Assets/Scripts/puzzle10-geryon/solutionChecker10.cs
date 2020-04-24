using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solutionChecker10 : NetworkSolutionChecker
{
    public override bool CheckSolution() {
        foreach (var item in solutionItems) {
            if (!item.GetComponent<NetworkDependant>().isSolved) {
                return false;
            }
        }
        return true;
    }
}
