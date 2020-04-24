using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TileSingleClick : NetworkInteraction
{
    public GameObject neighbourTile;
    public GameObject waterTile;
    public SolutionCheckerHandles handlesScript;
    public bool isSingle = true;
    public AudioSource brickSound;
    public WaterStateR updateScript;

    public override void InterActionFuntion(GameObject player)
    {
        base.InterActionFuntion(player);
        if (!isServer) return;
        if(handlesScript.waterRunning == true)
        {
            InteractMsg = "F to interact";
            if (this.GetComponent<TileState>().isPressed == false)
            {
                if(neighbourTile.GetComponent<TileState>().hasWater == true)
                {
                    RpcTileDownWater();
                    brickSound.Play();
                }
                else if(neighbourTile.GetComponent<TileState>().hasWater == false)
                {
                    RpcTileDown();
                }
            }else if(this.GetComponent<TileState>().isPressed == true)
            {
                RpcTileUp();
            }
            brickSound.Play();
        }
        else if(handlesScript.waterRunning == false)
        {
            InteractMsg = " ";
        }
        
    }



    [ClientRpc]
    public void RpcTileDownWater()
    {
        this.gameObject.GetComponent<TileState>().hasWater = true;
        waterTile.GetComponent<Renderer>().enabled = true;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<TileState>().isPressed = true;
        updateScript.update = true;
    }

    [ClientRpc]
    public void RpcTileDown()
    {
        Debug.Log("called this");
        Debug.Log(this.name);
        this.gameObject.GetComponent<TileState>().hasWater = false;
        waterTile.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<TileState>().isPressed = true;
        updateScript.update = true;

    }

    [ClientRpc]
    public void RpcTileUp()
    {
        this.gameObject.GetComponent<TileState>().hasWater = false;
        waterTile.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.gameObject.GetComponent<TileState>().isPressed = false;
        updateScript.update = true;
    }


}
