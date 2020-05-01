using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkPlayerUseItem : NetworkBehaviour
{

    public NetworkPlayerInteract interactionScript;
    public GameObject dropPoint;
    public GameObject usedHand;

    public Image itemImage;
    public Sprite noItemSprite;

    public Text carryText;

    public bool hasItem;

    private void Start() {
        carryText.enabled = false;
    }
    //called from NetworkPlayerInteract when f is pressed
    public void UseItem(GameObject usedItem) {
        if (!usedItem.GetComponent<NetworkInteraction>().itemType) {
            CmdUseItem(usedItem);
        }
        else {
            NetworkIdentity itemId = usedItem.GetComponent<NetworkIdentity>();
            if (!usedItem.GetComponent<NetworkInteraction>().itemHeld) {
                if (itemId != null && !itemId.hasAuthority) {
                    if (!hasItem) {
                        if (SceneManager.GetActiveScene().name == "Puzzle10-geryon") {
                            CmdUseItem(usedItem);
                        }

                        itemId.gameObject.GetComponent<AudioSource>().Play();
                        CmdClientRequest(itemId);
                        
                        CmdInvokeSetup(usedItem);
                    }else if (hasItem) {
                        CarryOverloadActive("you cannot carry anymore items");
                        Invoke("CarryOverloadInactive", 2.5f);
                    }
                }
            }
            else {
                interactionScript.InteractionActive("someone already has this item");
            }
        }
    }

    //called from NetworkPlayerInteract when mouse is clicked
    public void UseHand(GameObject receiverItem) {
        if (hasItem) {
            GameObject heldItem = usedHand.transform.GetChild(0).gameObject;
            if (heldItem.GetComponent<NetworkInteraction>().isPlaceable) {
                if (!receiverItem.GetComponent<NetworkDependant>().isOccupied) {
                    CmdClientRemove(heldItem.GetComponent<NetworkIdentity>());
                    CmdReceiveObject(heldItem, receiverItem);
                }
            } 
        }
    }

    public void CarryOverloadActive(string msg) {
        carryText.text = msg;
        carryText.enabled = true;
        //crosshair.enabled = false;
    }

    public void CarryOverloadInactive() {

        carryText.enabled = false;
        //crosshair.enabled = true;
    }

    IEnumerator ChangeItemLayer(GameObject item, string layerName) {
        yield return new WaitForSeconds(2f);
        item.layer = LayerMask.NameToLayer(layerName);
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
    void CmdInvokeSetup(GameObject usedItem) {
        if (!isServer) return;
        RpcSetupItem(usedItem);
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

        itemImage.sprite = noItemSprite;

        heldInter.itemHeld = false;
        heldInter.isInItem = true;

        hasItem = false;

        heldItem.transform.position = spawnPos;
        heldItem.transform.localRotation = spawnRot;
        receiverDepen.CheckInteraction(heldItem);
    }

    //this is when item is put in players hand
    [ClientRpc]
    void RpcSetupItem(GameObject usedItem) {
        NetworkInteraction usNI = usedItem.GetComponent<NetworkInteraction>();
        usNI.itemHeld = true;
        if (usNI.isInItem) {
            usNI.isInItem = false;
            usNI.holderItem.GetComponent<NetworkDependant>().isOccupied = false;
            usNI.holderItem.GetComponent<NetworkDependant>().isSolved = false;
            usNI.holderItem.GetComponent<NetworkDependant>().heldItem = null;
            usNI.holderItem = null;
        }
        hasItem = true;

        if (isServer) {
            itemImage.sprite = usNI.serverImage;
        } else {
            itemImage.sprite = usNI.clientImage;
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



}
