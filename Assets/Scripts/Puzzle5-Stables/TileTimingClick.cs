using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class TileTimingClick : NetworkReveiler
{
    public GameObject neighborTile;
    public GameObject waterTile;
    public GameObject tile;
    public GameObject twinTile;
    public GameObject twinTileNeighbor;
    public GameObject twinTileWater;
    public AudioSource brickSound;


    public override void ReveilPrice()
    {
        if (!isServer) return;
        if(neighborTile.GetComponent<TileState>().hasWater == true && twinTileNeighbor.GetComponent<TileState>().hasWater == true)
        {
            RpcReveilWithWaterBoth();
        }
        else if(neighborTile.GetComponent<TileState>().hasWater == true && twinTileNeighbor.GetComponent<TileState>().hasWater == false)
        {
            RpcTileWater();
        }
        else if (neighborTile.GetComponent<TileState>().hasWater == false && twinTileNeighbor.GetComponent<TileState>().hasWater == true)
        {
            RpcTwinWater();

        }else if(neighborTile.GetComponent<TileState>().hasWater == false && twinTileNeighbor.GetComponent<TileState>().hasWater == false)
        {
            RpcNoWater();
        }
    }

    public void UpdateTiles()
    {
        if (!isServer) return;
        if(tile.GetComponent<TileState>().isPressed == true && twinTile.GetComponent<TileState>().isPressed == true)
        {
            if (neighborTile.GetComponent<TileState>().hasWater == true && twinTileNeighbor.GetComponent<TileState>().hasWater == true)
                    {
                        RpcReveilWithWaterBoth();
                    }
                    else if (neighborTile.GetComponent<TileState>().hasWater == true && twinTileNeighbor.GetComponent<TileState>().hasWater == false)
                    {
                        RpcTileWater();
                    }
                    else if (neighborTile.GetComponent<TileState>().hasWater == false && twinTileNeighbor.GetComponent<TileState>().hasWater == true)
                    {
                        RpcTwinWater();

                    }
                    else if (neighborTile.GetComponent<TileState>().hasWater == false && twinTileNeighbor.GetComponent<TileState>().hasWater == false)
                    {
                        RpcNoWater();
                    }
        }else if(tile.GetComponent<TileState>().isPressed == false && twinTile.GetComponent<TileState>().isPressed == false)
        {

        }
        
    }

    [ClientRpc]
    void RpcReveilWithWaterBoth()
    {
        tile.gameObject.GetComponent<Renderer>().enabled = false;
        waterTile.gameObject.GetComponent<Renderer>().enabled = true;
        twinTile.gameObject.GetComponent<Renderer>().enabled = false;
        twinTileWater.gameObject.GetComponent<Renderer>().enabled = true;
        tile.GetComponent<TileState>().hasWater = true;
        twinTile.GetComponent<TileState>().hasWater = true;
        twinTile.GetComponent<TileState>().isPressed = true;
        tile.GetComponent<TileState>().isPressed = true;
    }

    [ClientRpc]
    void RpcTileWater()
    {
        tile.gameObject.GetComponent<Renderer>().enabled = false;
        waterTile.gameObject.GetComponent<Renderer>().enabled = true;
        twinTile.gameObject.GetComponent<Renderer>().enabled = false;
        twinTileWater.gameObject.GetComponent<Renderer>().enabled = false;
        tile.GetComponent<TileState>().hasWater = true;
        twinTile.GetComponent<TileState>().hasWater = false;
        twinTile.GetComponent<TileState>().isPressed = true;
        tile.GetComponent<TileState>().isPressed = true;
    }

    [ClientRpc]
    void RpcTwinWater()
    {
        tile.gameObject.GetComponent<Renderer>().enabled = false;
        waterTile.gameObject.GetComponent<Renderer>().enabled = false;
        twinTile.gameObject.GetComponent<Renderer>().enabled = false;
        twinTileWater.gameObject.GetComponent<Renderer>().enabled = true;
        tile.GetComponent<TileState>().hasWater = false;
        twinTile.GetComponent<TileState>().hasWater = true;
        twinTile.GetComponent<TileState>().isPressed = true;
        tile.GetComponent<TileState>().isPressed = true;
    }

    [ClientRpc]
    void RpcNoWater()
    {
        tile.gameObject.GetComponent<Renderer>().enabled = false;
        waterTile.gameObject.GetComponent<Renderer>().enabled = false;
        twinTile.gameObject.GetComponent<Renderer>().enabled = false;
        twinTileWater.gameObject.GetComponent<Renderer>().enabled = false;
        tile.GetComponent<TileState>().hasWater = false;
        twinTile.GetComponent<TileState>().hasWater = false;
        twinTile.GetComponent<TileState>().isPressed = true;
        tile.GetComponent<TileState>().isPressed = true;
    }


}
