using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependantInteract : MonoBehaviour
{
    protected GameObject heldItem;
    public bool isOccupied = false;
    public bool isSolved = false;
    
    public SolutionChecker solutionChecker;

    public virtual void CheckInteraction(GameObject hand, GameObject player) {

    }


}
