using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class pipeRotator : NetworkInteraction
{
    public bool hasOther = false;
    private bool isRotating = false;
    private bool wasReversed = false;
    private bool wasTrigger = false;
    public pipeRotator otherPipe;
    public Quaternion from;
    public Quaternion to;
    public int pipePosition = 0;
    public int numPosPos = 2;

    public AudioSource pipeRotationSound;

    public override void InterActionFuntion(GameObject player) {
        //base.InterActionFuntion(player);
        if (!isServer) return;
        if (isRotating == false) {
            wasTrigger = true;
            RpcRunRotate(90);
            if (hasOther) {
                otherPipe.RpcRunRotate(-90);
            }
        } else if (wasReversed == false && wasTrigger == false) {
            RpcRunRotateBack(from);
        }
    }
    public IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f) {
        Debug.Log("is rotating");
        isRotating = true;
        from = transform.rotation;
        to = transform.rotation;
        to *= Quaternion.Euler(axis.x, axis.y, angle);

        float elapsed = 0.0f;
        while (elapsed < duration) {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;

        if (angle < 0) {
            ChangePosition(-1);
        } else {
            ChangePosition(1);
        }

        isRotating = false;
        wasTrigger = false;
    }

    public IEnumerator RotateBack(Vector3 axis, Quaternion toPlace, float duration = 1.0f) {
        isRotating = true;
        wasReversed = true;
        to = toPlace;
        from = transform.rotation;

        float elapsed = 0.0f;
        while (elapsed < duration) {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
        isRotating = false;
        wasReversed = false;
    }

    [ClientRpc]
    public void RpcRunRotate(int degrees) {
        pipeRotationSound.Play();
        StartCoroutine(Rotate(Vector3.left, degrees, 0.2f));
    }

    [ClientRpc]
    public void RpcRunRotateBack(Quaternion toPos) {
        pipeRotationSound.Play();
        StopAllCoroutines();
        StartCoroutine(RotateBack(Vector3.left, toPos, 0.2f));
    }


    public void ChangePosition(int position) {
        pipePosition += position;
        if (pipePosition < 0) {
            pipePosition = numPosPos-1;
        } else if (pipePosition > numPosPos-1) {
            pipePosition = 0;
        }
    }

}
