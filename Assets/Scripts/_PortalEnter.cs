using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortalEnter : MonoBehaviour
{

    public Text showText;
    public Image showImage1;
    public Image showImage2;
    // Start is called before the first frame update
    void Start()
    {
        showImage1.enabled = false;
        showImage2.enabled = false;
        showText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Collided with player");
            showText.enabled = true;
            showImage1.enabled = true;
            showImage2.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player exit");
            showText.enabled = false;
            showImage1.enabled = false;
            showImage2.enabled = false;
        }
    }
}
