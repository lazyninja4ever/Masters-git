using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkDependant : NetworkBehaviour
{
    public bool isSolved = false;
    public bool isOccupied = false;
    public int solutionItem;
    public string interactMsg = "place item";
    public GameObject heldItem;

    public NetworkSolutionChecker solutionChecker;

    public virtual void CheckInteraction(GameObject item) {
        
    }
}
