using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkReceiverItem : NetworkDependant
{
    public AudioSource receiverSound;

    public override void CheckInteraction(GameObject item) {
        if (!isServer) return;
        if (item.GetComponent<NetworkInteraction>().solutionNmbr == solutionItem) {
            receiverSound.Play();
            isSolved = true;
        }

        solutionChecker.PuzzleState();
        //check if received item is the solution 
        //ask solution checker to check puzzle,
    }

}
