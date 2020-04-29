using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : TileCollisionDetektion
{
    public ResetPlayerPositionP9 resetScript;
    

    public override void ReactToCollision() {
        
        if (!isServer) return;
        resetScript.ResetPosition();
        
    }
}
