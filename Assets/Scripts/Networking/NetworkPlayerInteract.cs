using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayerInteract : MonoBehaviour {

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
    private float viewDistance = 15f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private LayerMask layerMaskDependent;

    public NetworkPlayerUseItem userItemScript;

    // Start is called before the first frame update
    void Start() {

        showText.enabled = false;
        //rightArmUseAnim = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update() {
        fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, viewDistance, layerMaskDependent.value)) {
            //fill in the raycast object 
            raycastedObj = hit.collider.gameObject;
            string msg = raycastedObj.GetComponent<NetworkDependant>().interactMsg;
            //if (hasLooked == false)
            InteractionActive(msg);

            Vector3 namePos = Camera.main.WorldToScreenPoint(raycastedObj.transform.position);
            showText.transform.position = namePos;


        } else if (Physics.Raycast(transform.position, fwd, out hit, viewDistance, layerMaskInteract.value)) {
            //fill in the raycast object 
            raycastedObj = hit.collider.gameObject;
            string msg = raycastedObj.GetComponent<NetworkInteraction>().InteractMsg;

            //if (hasLooked == false)
                InteractionActive(msg);
            Vector3 namePos = Camera.main.WorldToScreenPoint(raycastedObj.transform.position);
            showText.transform.position = namePos;

            if (Input.GetKeyDown("e")) {
                userItemScript.UseItem(raycastedObj);

            }

        } else {
            if (hasLooked == true)
                InteractionInactive();
        }

        if (Input.GetMouseButtonDown(0)) {
            UseItem();
        }

    }

    void UseItem() {

        if (Physics.Raycast(transform.position, fwd, out hit, viewDistance, layerMaskDependent.value)) {
            if (hit.collider.CompareTag("Dependent")) {
                raycastedObj = hit.collider.gameObject;
                userItemScript.UseHand(raycastedObj);
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
