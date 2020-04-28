using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class p10Collectible : NetworkCollectible
{
    public int clueSize;
    public ClueHighLighter highLighter;

    public override void InterActionFuntion(GameObject player) {
        base.InterActionFuntion(player);
        highLighter.Invoke("CheckHighLight", 0.2f);
        Debug.Log("ran highlighter");
    }
}
