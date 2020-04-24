using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkPlayerUseItem : NetworkBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public NetworkPlayerInteract interactionScript;
    public GameObject dropPoint;
    GameObject usedHand;
    

    //called from NetworkPlayerInteract when f is pressed
    public void UseItem(GameObject usedItem) {
        if (!usedItem.GetComponent<NetworkInteraction>().itemType) {
            CmdUseItem(usedItem);
        }
        else {
            NetworkIdentity itemId = usedItem.GetComponent<NetworkIdentity>();
            if (!usedItem.GetComponent<NetworkInteraction>().itemHeld) {
                if (itemId != null && !itemId.hasAuthority) {
                    if (CheckHands()) {
                        int hand = GetHand();
                        itemId.gameObject.GetComponent<AudioSource>().Play();
                        CmdClientRequest(itemId);
                        
                        CmdInvokeSetup(usedItem, hand);
                    }
                }
            }
            else {
                interactionScript.InteractionActive("someone already has this item");
            }
        }
    }

    //called from NetworkPlayerInteract when mouse is clicked
    public void UseHand(int hand, GameObject receiverItem) {
        GameObject playerHand = GetHandObject(hand);
        
        if (playerHand.transform.childCount > 0) {
            GameObject heldItem = playerHand.transform.GetChild(0).gameObject;
            if (heldItem.GetComponent<NetworkInteraction>().isPlaceable) {
                if (!receiverItem.GetComponent<NetworkDependant>().isOccupied) {
                    CmdClientRemove(heldItem.GetComponent<NetworkIdentity>());
                    CmdReceiveObject(heldItem, receiverItem);
                }
            } 
        }
    }

    //called from networkPlayerInteract when q or e is pressed
    public void DropItem(int hand) {
        GameObject playerHand = GetHandObject(hand);
        if (playerHand.transform.childCount > 0) {
            GameObject heldItem = playerHand.transform.GetChild(0).gameObject;
            CmdClientRemove(heldItem.GetComponent<NetworkIdentity>());
            CmdDropItem(heldItem);
        }
        
    }

    IEnumerator ChangeItemLayer(GameObject item, string layerName) {
        yield return new WaitForSeconds(2f);
        item.layer = LayerMask.NameToLayer(layerName);
    }

    //chech if hands are full, return true if hand is available
    bool CheckHands() {
        if (rightHand.transform.childCount > 0) {
            if (leftHand.transform.childCount > 0) {
                interactionScript.InteractionActive("your hands are full");
                return false;
            }
        }
        return true;
    }

    //return the first available hand, right, left
    int GetHand() {
        if (rightHand.transform.childCount > 0) {
            return 2;
        }
        return 1;
    }

    GameObject GetHandObject(int hand) {
        if (hand == 1) {
            return rightHand;
        }
        else {
            return leftHand;
        }
    }

    [Command]
    void CmdUseItem(GameObject interactObject) {
        interactObject.GetComponent<NetworkInteraction>().InterActionFuntion(this.gameObject);
    }

    [Command]
    void CmdClientRequest(NetworkIdentity itemId) {
        itemId.AssignClientAuthority(connectionToClient);
    }

    [Command]
    void CmdClientRemove(NetworkIdentity itemId) {
        itemId.RemoveClientAuthority();
    }

    [Command]
    void CmdDropItem(GameObject heldItem) {
        if (!isServer) return;
        RpcDropItem(heldItem);
    }

    [Command]
    void CmdInvokeSetup(GameObject usedItem, int hand) {
        if (!isServer) return;
        RpcSetupItem(usedItem, hand);
    }

    [Command]
    void CmdReceiveObject(GameObject heldItem, GameObject receiver) {
        if (!isServer) return;
        RpcReceiveItem(heldItem, receiver);
    }



    //this is when player puts item in other object
    [ClientRpc]
    void RpcReceiveItem(GameObject heldItem, GameObject receiver) {
        NetworkDependant receiverDepen = receiver.GetComponent<NetworkDependant>();
        NetworkInteraction heldInter = heldItem.GetComponent<NetworkInteraction>();

        receiverDepen.isOccupied = true;
        Vector3 spawnPos = receiver.transform.GetChild(0).transform.position;
        
        Quaternion spawnRot = receiver.transform.GetChild(0).transform.rotation;
        spawnPos.y += heldInter.placementOffsetY;

        heldItem.transform.parent = null;
        heldInter.holderItem = receiver;

        heldInter.itemHeld = false;
        heldInter.isInItem = true;

        heldItem.transform.position = spawnPos;
        heldItem.transform.localRotation = spawnRot;
        receiverDepen.CheckInteraction(heldItem);
    }

    //this is when item is put in players hand
    [ClientRpc]
    void RpcSetupItem(GameObject usedItem, int hand) {
        GameObject usedHand = GetHandObject(hand);
        NetworkInteraction usNI = usedItem.GetComponent<NetworkInteraction>();
        usNI.itemHeld = true;
        if (usNI.isInItem) {
            usNI.isInItem = false;
            usNI.holderItem.GetComponent<NetworkDependant>().isOccupied = false;
            usNI.holderItem.GetComponent<NetworkDependant>().isSolved = false;
            usNI.holderItem = null;
        }
        
        Rigidbody itemRB = usedItem.GetComponent<Rigidbody>();
        itemRB.useGravity = false;
        itemRB.isKinematic = true;
        usedItem.transform.parent = usedHand.transform;
        Vector3 spawnPos = usedHand.transform.localPosition;
        spawnPos.x += usNI.pickupOffsetX;
        spawnPos.y += usNI.pickupOffsetY;
        spawnPos.z += usNI.pickupOffsetZ;
        usedItem.transform.localPosition = spawnPos;
    }

    //this is when player drops an item
    [ClientRpc]
    void RpcDropItem(GameObject heldItem) {
        heldItem.layer = LayerMask.NameToLayer("NoPlayerCollision");
        Rigidbody itemRB = heldItem.GetComponent<Rigidbody>();
        itemRB.useGravity = true;
        itemRB.isKinematic = false;

        heldItem.transform.parent = null;

        NetworkInteraction usNI = heldItem.GetComponent<NetworkInteraction>();
        usNI.itemHeld = false;
        Vector3 spawnPos = dropPoint.transform.position;
        heldItem.transform.position = spawnPos;

        StartCoroutine(ChangeItemLayer(heldItem, "Interact"));
    }


}
