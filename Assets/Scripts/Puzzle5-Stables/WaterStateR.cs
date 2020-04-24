using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaterStateR : NetworkBehaviour
{
    public GameObject[] allTiles;
    public GameObject endingTileL;
    public GameObject endingTileR;

    public HideObjects priceHide;
    public ShowObjects priceShow;
    public HideObjects fence;
    public bool puzzleSolved = false;
    public GameObject finalWaterTile;

    public TileTimingClick timingScript;
    public TileTimingClick timingScript2;

    public GameObject particleEffect;
    public AudioSource reveilSound;
    public Animator anim;
    public bool update;

    private void Start()
    {
        finalWaterTile.GetComponent<Renderer>().enabled = false;
    }

    public void Update()
    {
        if(update == true)
        {
            if (!isServer) return;
            RpcUpdateWaterTiles();
        }
    }

    [ClientRpc]
    public void RpcUpdateWaterTiles()
    {
        if (!isServer) return;
        foreach (GameObject i in allTiles)
        {
            Debug.Log("i is :" + i);
           if(i.GetComponent<TileState>().isSingle == true)
            {
                GameObject getNeighbour = i.GetComponent<TileSingleClick>().neighbourTile;
                if(getNeighbour.GetComponent<TileState>().hasWater == true)
                {
                   if(i.GetComponent<TileState>().isPressed == true)
                    {
                        i.GetComponent<TileSingleClick>().RpcTileDownWater();
                        Debug.Log("neighbor water + single");
                    }
                }else if(getNeighbour.GetComponent<TileState>().hasWater == false)
                {
                    if (i.GetComponent<TileState>().isPressed == true)
                    {
                        i.GetComponent<TileSingleClick>().RpcTileDown();
                        Debug.Log("neighbor no water + single");
                    }
                }
            }if(i.GetComponent<TileState>().isPopOthers == true)
            {
                GameObject getNeighbour = i.GetComponent<TilePopOtherClick>().neighbourTile;
                if (getNeighbour.GetComponent<TileState>().hasWater == true)
                {
                    if (i.GetComponent<TileState>().isPressed == true)
                    {
                        i.GetComponent<TilePopOtherClick>().RpcTileDownWater();
                        Debug.Log("neighbor water + pop");
                    }
                }
                else if (getNeighbour.GetComponent<TileState>().hasWater == false)
                {
                    if (i.GetComponent<TileState>().isPressed == true)
                    {
                        i.GetComponent<TilePopOtherClick>().RpcTileDown();
                        Debug.Log("neighbor no water + pop");
                    }
                }
            }
           else if (i.GetComponent<TileState>().isTimer == true)
            {
                timingScript.UpdateTiles();
                timingScript2.UpdateTiles();
            } 
        }
        if (endingTileL.GetComponent<TileState>().hasWater == true && endingTileR.GetComponent<TileState>().hasWater == true && puzzleSolved == false)
        {
            if (!isServer) return;
            RpcEndPuzzle();
        }
        Debug.Log("updated tiles");
        update = false;
    }

    [ClientRpc]
    public void RpcEndPuzzle()
    {
        finalWaterTile.GetComponent<Renderer>().enabled = true;
        particleEffect.GetComponent<ParticleSystem>().Play();
        reveilSound.Play();
        priceShow.moveToPosition();
        puzzleSolved = true;
        fence.moveToPosition();
        anim.Play("FenceDown");
    }
} 
