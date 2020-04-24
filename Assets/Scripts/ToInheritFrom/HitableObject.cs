using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableObject : DependantInteract
{
    public string reactable;
    

    public override void CheckInteraction(GameObject hand, GameObject player) {
        base.CheckInteraction(hand, player);
        heldItem = hand.transform.GetChild(0).gameObject;
        if (heldItem != null) {
            //make ref to script on item in hand
            WeaponScript heldItemPI = heldItem.GetComponent<WeaponScript>();
            if (heldItem.CompareTag(reactable)) {
                //heldItemPI.UseWeapon();
                ReactionFunction();
            }
            else {
                Debug.Log("Is not placeable, white");
            }
        }
        else {
            Debug.Log("No item equiped");
        }
    }

    public virtual void ReactionFunction() {

    }

}
