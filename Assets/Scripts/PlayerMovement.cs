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
    public RaycastHit groundHit;
    public bool newSurface;
    public string surfaceTagOld;
    public bool wasStopped;

    public Text playerText;
    public Text playerSubText;
    private string introText;
    private string introSubText;

    public BowState bowStateScript;

    public Animator playerAnim;

    public bool hasBow = false;

    public PlayerMovementSound playerSoundScript;

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
            PlayerSoundCheck();
            //check which surface the player is on
            
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
            wasStopped = true;
            playerSoundScript.CmdStopMovementSound();
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

    void PlayerSoundCheck() {
        if (Physics.Raycast(transform.localPosition - new Vector3(0, -2, 1), Vector3.down, out groundHit, 20, groundMask.value)) {
            string floortag = groundHit.collider.gameObject.tag;
            if (floortag == surfaceTagOld && !wasStopped) return;
            if (floortag == "hard") {
                //play concrete sound code
                surfaceTagOld = "hard";
                playerSoundScript.CmdPlayerMovementSound(1);
            }
            else if (floortag == "soft") {
                //play Water sound code
                surfaceTagOld = "soft";
                playerSoundScript.CmdPlayerMovementSound(0);
            }
            wasStopped = false;
        }
    }
}
