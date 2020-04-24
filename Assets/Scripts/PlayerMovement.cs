using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;

    private SceneIntro portalScript;

    Vector3 velocity;
    bool isGrounded;

    public Text playerText;
    public Text playerSubText;
    private string introText;
    private string introSubText;

    public BowState bowStateScript;

    public Animator playerAnim;

    public bool hasBow = false;

    private void Awake() {
        portalScript = GameObject.Find("Portal").GetComponent<SceneIntro>();
    }

    public void Start() {
        speed = 0;
        introText = portalScript.introText;
        introSubText = portalScript.introSubText;
        EnableText(introText, introSubText);
        Invoke("DisableText", 2.5f);

    }
    public void EnableText(string introText, string introSubText) {
        playerText.text = introText;
        playerSubText.text = introSubText;
        playerText.enabled = true;
    }

    private void DisableText() {
        playerText.enabled = false;
        playerSubText.enabled = false;
        speed = 40;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0) {
            velocity.y = gravity*10;
        }
     //   Debug.Log("goind down with: " + velocity.y);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0f || z != 0f) {
            playerAnim.Play("Run_002");
        }
        else if(hasBow/*bowStateScript.GetComponent<BowState>().hasBow == true*/)
        {
            if(bowStateScript.GetComponent<BowState>().isShooting == true)
            {
                //shoot
                playerAnim.Play("Bow");
            }
        }
        else
        {
            playerAnim.Play("Idle_002");
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void PlayBowAnimation()
    {
        playerAnim.Play("Bow");

    }
}
