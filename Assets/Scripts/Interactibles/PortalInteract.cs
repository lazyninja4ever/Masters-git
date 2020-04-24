using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteract : InteractibleObject
{
    public override void InterActionFuntion(GameObject activePlayer, GameObject rightHand, GameObject leftHand) {
        base.InterActionFuntion(activePlayer, rightHand, leftHand);
        Debug.Log("I interacted with the portal");
    }
}
