using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScaffoldingRotate : NetworkInteraction
{
    public bool isRotating;
    public int currentAngle = 0;
    public ScaffoldingSolution scaffSolutionScript;
  //  public AudioSource brickSound;

    public override void InterActionFuntion(GameObject player)
    {
        if (!isServer) return;
        base.InterActionFuntion(player);
        if (isRotating == false)
        {
            RpcRunRotate(90);
            scaffSolutionScript.Invoke("RpcCheckboardScaf", 1f);
        }
    }

    public IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        isRotating = true;
    //    brickSound.Play();
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis.x, axis.y, angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
        currentAngle++;
        isRotating = false;

        if (currentAngle == 4)
        {
            currentAngle = 0;
        }
    }

    [ClientRpc]
    public void RpcRunRotate(int degrees)
    {
        StartCoroutine(Rotate(Vector3.left, degrees, 0.2f));
    }
}
