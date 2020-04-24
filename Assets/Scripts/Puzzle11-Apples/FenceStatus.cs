using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceStatus : MonoBehaviour
{
    public bool isShown = true;

    public void changeStateFalse()
    {
        isShown = false;
    }

    public void changeStateTrue()
    {
        isShown = true;
    }
}
