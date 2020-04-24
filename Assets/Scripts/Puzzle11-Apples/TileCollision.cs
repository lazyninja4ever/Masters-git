﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TileCollision : TileCollisionDetektion
{
    public string colour;
    public TileHandler tileHandlerScript;

    public override void ReactToCollision()
    {
        base.ReactToCollision();
        if (!isServer) return;
        Debug.Log("collided with at tile");
        tileHandlerScript.CheckColour(colour);
    }
}