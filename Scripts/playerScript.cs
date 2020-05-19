using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public GameObject ambulanceSound;

    public static bool gamePaused;

    public GameObject sitTip;
    public GameObject meowTip;
    public GameObject moveTip;

    [SerializeField]
    private float playerSpeed;

    public static bool facingLeft;
    [SerializeField]
    private Transform[] Groundpoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    public float playerMaxSpeed;

    public static GameObject player;
    [SerializeField]
    private bool isGrounded;

    public static float carryingItemDelay;
    public static float sitDelay;
    [SerializeField]
    public static bool isWalking;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private bool isPickingUp;
    [SerializeField]
    private float jumpForce;

    public static bool isSitting;
    public static bool isInScene;
    public static bool isFalling;


    [SerializeField]
    public static bool carryingItem;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        facingLeft = true;
        player = gameObject;
        isSitting = false;
        gamePaused = true;
        playerRigidBody.velocity = new Vector2(0, 0);
        isSitting = true;
        playerAnimator.SetTrigger("SitDown");
        sitDelay = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.x >= 1205)
        {
            playerAnimator.SetBool("isScared", true);
            ambulanceSound.SetActive(true);
        }

        if (!playerScript.gamePaused)
        {

            moveTip.SetActive(true);
            meowTip.SetActive(true);
            sitTip.SetActive(true);
            if (!isInScene)
            {
                resetValues();
                carryingItemDelay += Time.deltaTime;
                isGrounded = isGroundedTest();

                float horizontal = Input.GetAxis("Horizontal");
                if (!isPickingUp)
                {
                    handleInput();
                    if (!isSitting)
                    {
                        handleMovement(horizontal);

                        Flip(horizontal);
                    }
                }
                handleLayers();
            }
        }
        if (isInScene)
        {
            handleMovement(0);
        }
    }

    private void handleInput()
    {
        if (!isSitting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
            if (!carryingItem)
            {
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        handleMovement(0);


                    }
                }
            }
        }
        else
        {
            sitDelay += Time.deltaTime;
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle_sitting_anim"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerAnimator.SetTrigger("Meow");
                    player.GetComponent<AudioSource>().pitch = Random.Range(0.7f, 1.2f);
                    player.GetComponent<AudioSource>().Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            if (!isSitting)
            {
                if (isGrounded)
                {
                    playerRigidBody.velocity = new Vector2(0, 0);
                    isSitting = true;
                    playerAnimator.SetTrigger("SitDown");
                    sitDelay = 0;
                }
            }
            else if (isSitting)
            {
                if (sitDelay >= 1)
                {
                    playerAnimator.SetTrigger("GetUp");
                }
            }
        }
    }
    private void handleMovement(float horizontal)
    {
        if (!isGrounded && !isFalling)
        {
            playerRigidBody.velocity = new Vector2(horizontal * playerSpeed * 1.3f, playerRigidBody.velocity.y);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(horizontal * playerSpeed, playerRigidBody.velocity.y);
        }

        playerAnimator.SetFloat("WalkSpeed", Mathf.Abs(horizontal));

        if (playerRigidBody.velocity.y < 0)
        {
            playerAnimator.SetBool("Land", true);
        }

        if (isGrounded && isJumping)
        {
            isGrounded = false;
            playerRigidBody.AddForce(new Vector2(0, jumpForce));
            playerAnimator.ResetTrigger("TouchGround");
            playerAnimator.SetTrigger("Jump");
        }
    }

    private void resetValues()
    {

        isJumping = false;
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("PickUp"))
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                isPickingUp = false;
            }
        }
        if (isSitting)
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle_standingg"))
            {
                isSitting = false;
                playerAnimator.ResetTrigger("SitDown");
                playerAnimator.ResetTrigger("GetUp");
            }
        }
    }

    private void Flip(float horizontal)
    {

        if (horizontal > 0 && facingLeft || horizontal < 0 && !facingLeft)
        {
            facingLeft = !facingLeft;
            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }

    }

    private bool isGroundedTest()
    {
        if (playerRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in Groundpoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        playerAnimator.ResetTrigger("Jump");
                        playerAnimator.SetBool("Land", false);
                        playerAnimator.SetTrigger("TouchGround");

                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void handleLayers()
    {
        Debug.Log("LayerIndex " + playerAnimator.GetLayerIndex("Air Layer"));
        Debug.Log("Current CLip " + playerAnimator.GetCurrentAnimatorClipInfoCount(0));
        if (!isGrounded)
        {
            playerAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            if (playerAnimator.GetCurrentAnimatorClipInfoCount(1) != 1)
                playerAnimator.SetLayerWeight(1, 0);

        }


    }


    public void pickUp(GameObject pickUp)
    {
        if (!playerScript.gamePaused)
        {
            if (!playerScript.isSitting)
            {
                if (Vector2.Distance(playerScript.player.transform.position, pickUp.transform.position) < 4)
                {
                    Debug.Log("Close Enough");
                    if (playerScript.carryingItem == false)
                    {
                        if (!isInScene)
                        {
                            Debug.Log("Not Carrying ITem");
                            if (playerScript.carryingItemDelay > 0.5f)
                            {
                                Debug.Log("Long enough since click");

                                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                                {
                                    if (Input.GetKeyDown(KeyCode.E))
                                    {
                                        isPickingUp = true;
                                        Debug.Log("Pressed E");
                                        playerRigidBody.velocity = new Vector2(0, 0);
                                        playerScript.player.GetComponent<Animator>().SetTrigger("PickUp");
                                        pickUp.GetComponent<Rigidbody2D>().isKinematic = true;
                                        pickUp.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                                        pickUp.GetComponent<Rigidbody2D>().angularVelocity = 0;
                                        pickUp.GetComponent<CircleCollider2D>().enabled = false;
                                        pickUp.transform.position = player.transform.GetChild(0).transform.position;
                                        pickUp.transform.SetParent(player.transform.GetChild(0).transform, true);
                                        pickUp.transform.localScale = new Vector2(1, 1);
                                        playerScript.carryingItem = true;
                                        playerScript.carryingItemDelay = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void dropPickUp(GameObject pickUp)
    {
        if (!playerScript.gamePaused)
        {
            if (!playerScript.isSitting)
            {
                if (playerScript.carryingItem == true)
                {
                    if (!isInScene)
                    {
                        Debug.Log("Trying to drop pickup: CarryingItem = true");
                        if (playerScript.carryingItemDelay > 0.5f)
                        {
                            Debug.Log("Trying to drop pickup: itemdelay > 0.2");
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                player.transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody2D>().isKinematic = false;
                                player.transform.GetChild(0).transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;

                                player.transform.GetChild(0).transform.GetChild(0).transform.localScale = new Vector2(1, 1);
                                playerScript.carryingItem = false;
                                playerScript.carryingItemDelay = 0;
                                Debug.Log("Trying to drop pickup: Pressed E");
                                player.transform.GetChild(0).transform.DetachChildren();

                            }
                        }
                    }
                }
            }
        }
    }

    public void kickPickUp(GameObject pickUp)
    {
        if (!playerScript.gamePaused)
        {
            if (playerScript.carryingItem == false)
            {
                if (!isInScene)
                {
                    if (Vector2.Distance(player.transform.position, pickUp.transform.position) < 4)
                    {
                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle_standingg"))
                            {
                                playerScript.player.GetComponent<Animator>().SetTrigger("Kick");
                            }
                            if (playerScript.facingLeft == true)
                            {
                                pickUp.GetComponent<Rigidbody2D>().AddForce(new Vector2(-150, 200));
                            }
                            else
                            {
                                pickUp.GetComponent<Rigidbody2D>().AddForce(new Vector2(150, 200));
                            }
                        }
                    }
                }
            }
        }
    }

}
