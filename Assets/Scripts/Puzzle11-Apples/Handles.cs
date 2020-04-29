using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Handles : NetworkReveiler
{
  /*  public ShowObjects fenceLarge;
    public HideObjects fenceLargeHide;
    public ShowObjects fenceMedium;
    public HideObjects fenceMediumHide;
    public ShowObjects fenceStart;
    public HideObjects fenceStartHide; */

    public FenceStatus fenceScript;
    public HandleCollision handleLCollisionScript;
    public HandleCollision handleRCollisionScript;
    public GameObject colorBoardL;
    public GameObject colorBoardR;
    public GameObject startBoardL;
    public GameObject startBoardR;
 //   public GameObject playerCollided;
    public bool handleOn;
    public bool serverIsLeft;
    public Animator fenceAnim;
    public AudioSource fenceSound;

    public override void ReveilPrice()
    {
        if (!isServer) return;
        if(!handleOn)
        {
            RpcHideFence();
            if (handleLCollisionScript.player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                    RpcServerHideR();

            }
            if (handleRCollisionScript.player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                    RpcServerHideL();
            }
        }
        else if(handleOn)
        {
            RpcReveil();
            RpcHideColor();
        }

    }


    [ClientRpc]
    public void RpcHideFence()
    {
        /*  fenceLarge.moveToPosition();
          fenceMedium.moveToPosition();
          fenceStartHide.moveToPosition(); */
        fenceAnim.Play("FenceHide");
        fenceSound.Play();
    }

    [ClientRpc]
    public void RpcReveil()
    {
        /*  fenceStart.moveToPosition();
         fenceMediumHide.moveToPosition();
         fenceLargeHide.moveToPosition(); */
        fenceAnim.Play("FenceShow");
        fenceSound.Play();
    }

    [ClientRpc]
    public void RpcServerHideL()
    {
        if (isServer)
        {
            colorBoardR.SetActive(true);
            Component[] tilesR = colorBoardR.GetComponentsInChildren(typeof(MeshRenderer), false);
            foreach (MeshRenderer tile in tilesR) {
                tile.enabled = true;
            }
            Component[] tilesL = colorBoardL.GetComponentsInChildren(typeof(MeshRenderer), false);
            foreach (MeshRenderer tile in tilesL) {
                tile.enabled= false;
            }
            startBoardR.SetActive(false);
            startBoardL.SetActive(true);

            //colorBoardR.SetActive(true);
            //colorBoardL.SetActive(false);
        }
        if (!isServer)
        {
            startBoardL.SetActive(false);
            colorBoardL.SetActive(true);
            startBoardR.SetActive(true);
            colorBoardR.SetActive(false);
        }
        handleOn = true;
        serverIsLeft = false;
    }

    [ClientRpc]
    public void RpcServerHideR()
    {
        
        
        if (isServer)
        {
            colorBoardL.SetActive(true);
            Component[] tilesL = colorBoardL.GetComponentsInChildren(typeof(MeshRenderer), false);
            foreach (MeshRenderer tile in tilesL) {
                tile.enabled = true;
            }
            Component[] tilesR = colorBoardR.GetComponentsInChildren(typeof(MeshRenderer), false);
            foreach (MeshRenderer tile in tilesR) {
                tile.enabled = false;
            }
            startBoardL.SetActive(false);
            startBoardR.SetActive(true);

        }
        if (!isServer)
        {
            startBoardR.SetActive(false);
            colorBoardR.SetActive(true);
            colorBoardL.SetActive(false);
            startBoardL.SetActive(true);
        }
        handleOn = true;
        serverIsLeft = true;
    }

    [ClientRpc]
    public void RpcHideColor()
    {
        startBoardL.SetActive(true);
        startBoardR.SetActive(true);
        colorBoardL.SetActive(false);
        colorBoardR.SetActive(false);
        handleOn = false;
    }
}
