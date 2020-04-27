using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solutionChecker10 : NetworkSolutionChecker
{
    public ClueHighLighter highLighter;
    public bool checkHighLight;

    public override bool CheckSolution() {
        if (checkHighLight) {
            highLighter.CheckHighLight();
        }
       
        foreach (var item in solutionItems) {
            if (!item.GetComponent<NetworkDependant>().isSolved) {
                return false;
            }
        }
        return true;
    }
}
