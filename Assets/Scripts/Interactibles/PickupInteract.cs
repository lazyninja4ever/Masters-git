using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : InteractibleObject
{
    private Rigidbody m_ThisRigidbody = null;
    public Vector3 defaultScale;
    public bool setDefaultScale = true;
    public bool isInItem = false;
    public bool isHeld = false;
    public bool isPlaceable;
    public bool isWeapon;
    public bool isSolution;
    [SerializeField] private Vector3 heldRotation;
    [SerializeField] private Vector3 heldPosition;

    private void Start() {
        m_ThisRigidbody = GetComponent<Rigidbody>();
        if (setDefaultScale) {
            defaultScale = this.gameObject.transform.localScale;
        }
    }
    public override void InterActionFuntion(GameObject activePlayer, GameObject rightHand, GameObject leftHand) {
        base.InterActionFuntion(activePlayer, rightHand, leftHand);

        if (!isHeld) {

            if (rightHand.transform.childCount > 0) {
                if (leftHand.transform.childCount > 0) {
                    activePlayer.GetComponent<PlayerInteract>().InteractionActive("YOUR HANDS ARE FULL");
                } else {
                    PickupItem(leftHand);
                    
                }
                
            } else {
                PickupItem(rightHand);
                
            }

        } else {
            activePlayer.GetComponent<PlayerInteract>().InteractionActive("SOMEONE ELSE HAS THIS OBJECT");
        }

    }

    public void PickupItem(GameObject hand) {
        if (isInItem) {
            DependantInteract itemParentDI = this.transform.parent.transform.parent.GetComponent<DependantInteract>();
            itemParentDI.isOccupied = false;
            itemParentDI.isSolved = false;
            isInItem = false;
        }
        this.gameObject.transform.parent = hand.transform;
        this.gameObject.transform.localPosition = heldPosition;
        this.gameObject.transform.localRotation = Quaternion.Euler(heldRotation);
        m_ThisRigidbody.useGravity = false;
        m_ThisRigidbody.isKinematic = true;
        isHeld = true;
    }

    public void Drop() {
        gameObject.layer = LayerMask.NameToLayer("NoPlayerCollision");
        m_ThisRigidbody.useGravity = true;
        m_ThisRigidbody.isKinematic = false;
        this.gameObject.transform.parent = null;
        this.gameObject.transform.localScale = defaultScale;
        isHeld = false;
        Invoke("ChangeLayer", 2.0f);
    }

    private void ChangeLayer() {
        gameObject.layer = LayerMask.NameToLayer("Interact");
    }

}
