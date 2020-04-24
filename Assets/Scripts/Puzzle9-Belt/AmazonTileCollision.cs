using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmazonTileCollision : TileCollisionDetektion
{
    public AmazonHandler handler;

    public override void ReactToCollision() {
        base.ReactToCollision();
        handler.TakeStep();
    }
}
