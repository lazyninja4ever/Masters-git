using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimedTiles : HandleButtonInteract
{
    public NetworkSolutionChecker solutionChecker;
    public GameObject tile;
    public GameObject twinTile;
    public GameObject tileWater;
    public GameObject twintileWater;
    public AudioSource tileDownSound;

    public override void InterActionFuntion(GameObject player)
    {
        base.InterActionFuntion(player);
        if (!isServer) return;
        if (isOn)
        {
            RpcFlipOff();
        }else if (!isOn)
        {
            RpcFlipOn();
            Invoke("RpcFlipOff", 1f);
        }
    }


    [ClientRpc]
    void RpcFlipOn()
    {
        isOn = true;
        isSolved = true;
        solutionChecker.PuzzleState();
        tile.GetComponent<Renderer>().enabled = false;
        twinTile.GetComponent<Renderer>().enabled = false;
        tileWater.GetComponent<Renderer>().enabled = false;
        twintileWater.GetComponent<Renderer>().enabled = false;
        tileDownSound.Play();
    }

    [ClientRpc]
    void RpcFlipOff()
    {
        if (isOn)
        {
            isOn = false;
            isSolved = false;
            tile.GetComponent<Renderer>().enabled = true;
            twinTile.GetComponent<Renderer>().enabled = true;
            tileDownSound.Play();
        }
    }
}
