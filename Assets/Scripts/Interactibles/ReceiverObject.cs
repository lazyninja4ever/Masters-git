using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverObject : DependantInteract
{
    public override void CheckInteraction(GameObject hand, GameObject player) {
        base.CheckInteraction(hand, player);
        heldItem = hand.transform.GetChild(0).gameObject;
        if (heldItem != null && !isOccupied) {

            //make ref to script on item in hand
            PickupInteract heldItemPI = heldItem.GetComponent<PickupInteract>();
            if (heldItemPI.isPlaceable) {
                ReceiveItem(heldItemPI);
            }
            else {
                Debug.Log("Is not placeable, white");
            }
        }
        else {
            Debug.Log("No item equiped");
        }

    }

    private void ReceiveItem(PickupInteract heldItemPI) {
        isOccupied = true;

        //PickupInteract heldItemPI = heldItem.GetComponent<PickupInteract>();
        //define new position, scale and rot for object
        Vector3 itemPos = this.gameObject.transform.GetChild(0).transform.position;
        Vector3 newItemScale = heldItemPI.defaultScale;
        Quaternion itemRot = this.gameObject.transform.rotation;

        //instantiate new object (clone of held item)
        GameObject go = Instantiate(heldItem, new Vector3(0, 0, 0), itemRot, this.gameObject.transform.GetChild(0).transform);
        //refer to script on new item and define values
        PickupInteract goPI = go.GetComponent<PickupInteract>();
        goPI.defaultScale = heldItemPI.defaultScale;
        goPI.isHeld = false;
        goPI.setDefaultScale = false;
        goPI.isInItem = true;

        //set new scale in relation to parent
        go.transform.localScale = newItemScale;
        Vector3 newScale = new Vector3();
        newScale.x = go.transform.localScale.x / this.transform.localScale.x;
        newScale.y = go.transform.localScale.y / this.transform.localScale.y;
        newScale.z = go.transform.localScale.z / this.transform.localScale.z;
        go.transform.localScale = newScale;

        //move item to top of parent
        itemPos.y += go.GetComponent<MeshRenderer>().bounds.max.y;
        go.transform.position = itemPos;

        //destroy item in hand
        Destroy(heldItem);

        isOccupied = true;

        if (heldItemPI.isSolution) {
            isSolved = true;
            solutionChecker.PuzzleState();
        }
    }
}
