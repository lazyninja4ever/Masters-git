using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SolutionCheckerHandles : NetworkReveiler
{
    public ShowObjects fenceShow;
    public HideObjects fenceHide;
    public bool fenceIsShown = false;
    public ShowObjects fountainStreamLShow;
    public ShowObjects fountainSteamRShow;
    public HideObjects fountainStreamLHide;
    public HideObjects fountainStreamRHide;
    public ShowObjects waterTileLShow;
    public ShowObjects waterTileRShow;
    public HideObjects waterTileLHide;
    public HideObjects waterTileRHide;
    public bool waterRunning;
    public TileState tileStateScriptL;
    public TileState tileStateScriptR;
    public AudioSource waterLeft;
    public AudioSource waterRight;
    public AudioSource waterDropL;
    public AudioSource waterDropR;
    public AudioSource fenceSound;
    public Animator anim;
    public GameObject[] tilesMessage;

    private void Start()
    {
        if (!isServer) return;
        RpcHideMessage();
    }


    public override void ReveilPrice()
    {
        if (!isServer) return;

        if (fenceIsShown == false)
        {
            RpcReveil();
            RpcShowMessage();
        }
        else if (fenceIsShown == true)
        {
            RpcHide();
            RpcHideMessage();
        }
    }

    [ClientRpc]
    void RpcReveil()
    {
        fountainSteamRShow.moveToPosition();
        fountainStreamLShow.moveToPosition();
        waterTileLShow.moveToPosition();
        waterTileRShow.moveToPosition();
    //    fenceShow.moveToPosition();
        fenceIsShown = true;
        anim.Play("FenceUp");
        fenceSound.Play();
        waterRunning = true;
        tileStateScriptL.hasWater = true;
        tileStateScriptR.hasWater = true;
        waterLeft.Play();
        waterRight.Play();
        waterDropL.Stop();
        waterDropR.Stop();
    }

    [ClientRpc]
    void RpcHide()
    {
    //    fenceHide.moveToPosition();
        fenceIsShown = false;
        anim.Play("FenceDown");
        fenceSound.Play();
        fountainStreamLHide.moveToPosition();
        fountainStreamRHide.moveToPosition();
        waterTileLHide.moveToPosition();
        waterTileRHide.moveToPosition();
        waterRunning = false;
        tileStateScriptL.hasWater = false;
        tileStateScriptR.hasWater = false;
        waterLeft.Stop();
        waterRight.Stop();
        waterDropL.Play();
        waterDropR.Play();
    }


    [ClientRpc]
    public void RpcHideMessage()
    {
        for (int i = 0; i < tilesMessage.Length; i++)
        {
            if (tilesMessage[i].GetComponent<TileState>().isSingle == true)
            {
                tilesMessage[i].GetComponent<TileSingleClick>().InteractMsg = "";
            }
            if (tilesMessage[i].GetComponent<TileState>().isPopOthers == true)
            {
                tilesMessage[i].GetComponent<TilePopOtherClick>().InteractMsg = "";
            }
            else if (tilesMessage[i].GetComponent<TileState>().isTimer == true)
            {
                tilesMessage[i].GetComponent<TimedTiles>().InteractMsg = "";
            }
        }
    }

    [ClientRpc]
    public void RpcShowMessage()
    {
        for (int i = 0; i < tilesMessage.Length; i++)
        {
            if (tilesMessage[i].GetComponent<TileState>().isSingle == true)
            {
                tilesMessage[i].GetComponent<TileSingleClick>().InteractMsg = "F to interact";
            }
            if (tilesMessage[i].GetComponent<TileState>().isPopOthers == true)
            {
                tilesMessage[i].GetComponent<TilePopOtherClick>().InteractMsg = "F to interact";
            }
            else if (tilesMessage[i].GetComponent<TileState>().isTimer == true)
            {
                tilesMessage[i].GetComponent<TimedTiles>().InteractMsg = "F to interact";
            }
        }
    }
}
