using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimedTiles : HandleButtonInteract
{
    public NetworkSolutionChecker solutionChecker;

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
    }

    [ClientRpc]
    void RpcFlipOff()
    {
        if (isOn)
        {
            isOn = false;
            isSolved = false;
        }
    }
}
