using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{

    //uses raycast to detect interactible(layermask and tag) objects, and call their action funtion
    public Text showText;

    public Image crosshair;
    public GameObject rightHand;
    public GameObject leftHand;
    RaycastHit hit;
    Vector3 fwd;

    public Animation rightArmUseAnim;

    private bool hasLooked;
    private Vector3 playerPosition;

    private GameObject raycastedObj;
    private float viewDistance = 20f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private LayerMask layerMaskDependent;

    // Start is called before the first frame update
    void Start()
    {

        showText.enabled = false;
        //rightArmUseAnim = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        fwd = transform.TransformDirection(Vector3.forward);
        //cast forward 
        if (Physics.Raycast(transform.position, fwd, out hit, viewDistance, layerMaskInteract.value)) {
            //fill in the raycast object 
            raycastedObj = hit.collider.gameObject;
            string msg  = raycastedObj.GetComponent<InteractibleObject>().InteractMsg;

            if (hasLooked == false)
                InteractionActive(msg);
                Vector3 namePos = Camera.main.WorldToScreenPoint(raycastedObj.transform.position);
                showText.transform.position = namePos;

            if (Input.GetKeyDown("f")) {
                raycastedObj.GetComponent<InteractibleObject>().InterActionFuntion(this.gameObject, rightHand, leftHand);

            }

        } else {
            if(hasLooked == true)
                InteractionInactive();
        }

        if (Input.GetKeyDown("q")) {
            DropItem(leftHand);
        }

        if (Input.GetKeyDown("e")) {
            DropItem(rightHand);
        }

        if (Input.GetMouseButtonDown(0)) {
            UseItem(leftHand);
        }
        if (Input.GetMouseButtonDown(1)) {
            UseItem(rightHand);
        }

    }

    void DropItem(GameObject hand) {
        hand.transform.GetChild(0).transform.GetComponent<PickupInteract>().Drop();
    }

    void UseItem(GameObject hand) {
        //call use function for weapons here
        //hand.getChild(0).UseWeapon();
        //check what item is hit and call its checkinteraction function
        //check hand
        if (hand == leftHand) {
            rightArmUseAnim.Play("leftArmUse");
        } else if (hand == rightHand) {
            rightArmUseAnim.Play("rightArmUse");
        }
        

        if (Physics.Raycast(transform.position, fwd, out hit, viewDistance, layerMaskDependent.value)) {
            if (hit.collider.CompareTag("Dependent")) {
                raycastedObj = hit.collider.gameObject;
                raycastedObj.GetComponent<DependantInteract>().CheckInteraction(hand, this.gameObject);
            }
        }
    }

    public void InteractionActive(string msg) {

        showText.text = msg;
        showText.enabled = true;
        //crosshair.enabled = false;
        hasLooked = true;
    }

     public void InteractionInactive() {

        showText.enabled = false;
        //crosshair.enabled = true;
        hasLooked = false;
    }

    

}
