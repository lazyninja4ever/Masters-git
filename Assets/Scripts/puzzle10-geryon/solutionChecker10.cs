using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class solutionChecker10 : NetworkSolutionChecker
{
    public ClueHighLighter highLighter;
    public bool checkHighLight;
    public AudioSource wrongSolution;

    public override bool CheckSolution() {
        if (checkHighLight) {
            highLighter.CheckHighLight();
        }
       
        foreach (var item in solutionItems) {


            if (!item.GetComponent<NetworkDependant>().isSolved) {
                if(CheckWrongSoultion()) RpcWrongSoultionSound();
                return false;
            }
        }
        return true;
    }

    bool CheckWrongSoultion() {
        foreach (var item in solutionItems) {

            if (!item.GetComponent<NetworkDependant>().isOccupied) {
                return false;
            }
        }
        return true;
    }

    [ClientRpc]
    void RpcWrongSoultionSound() {

        wrongSolution.Play();
    }


}
