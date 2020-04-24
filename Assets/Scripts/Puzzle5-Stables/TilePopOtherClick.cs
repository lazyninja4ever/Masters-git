using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TilePopOtherClick : NetworkInteraction
{
    public GameObject[] tileToPop;
    public GameObject neighbourTile;
    public GameObject waterTile;
    public bool popOthers = true;
    public SolutionCheckerHandles handleScript;
    public AudioSource brickSound;
    public WaterStateR updateScript;

    public void Start()
    {
        brickSound = brickSound.GetComponent<AudioSource>();
    }

    public override void InterActionFuntion(GameObject player)
    {
        base.InterActionFuntion(player);
        if (!isServer) return;
        if(handleScript.waterRunning == true)
        {
            InteractMsg = "F to interact";
            if(this.GetComponent<TileState>().isPressed == false)
            {
                RpcPopOthers();
                if (neighbourTile.GetComponent<TileState>().hasWater == true)
                {
                    RpcTileDownWater();
                    
                }
                else if(neighbourTile.GetComponent<TileState>().hasWater == false)
                {
                    RpcTileDown();
                }
            }
            else if(this.GetComponent<TileState>().isPressed == true)
            {
                RpcTileUp();
            }
            brickSound.Play();
        }
    }

    [ClientRpc]
    public void RpcPopOthers()
    {
        brickSound.Play();
        for (int i = 0; i < tileToPop.Length; i++)
        {
            if(tileToPop[i].GetComponent<TileState>().isPressed == true)
            {
                brickSound.Play();
            }

            tileToPop[i].GetComponent<Renderer>().enabled = true;
            tileToPop[i].GetComponent<TileState>().isPressed = false;
            tileToPop[i].GetComponent<TileState>().hasWater = false;
        }
        updateScript.update = true;
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
