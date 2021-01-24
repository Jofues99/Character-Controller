using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //MI NOMBRE
    public enum MiName {
        Freya,
        Jack,
    }

    public MiName miName;

    //VARIABLES PARA EL MOVIMIENTO DEL JUGADOR
    [Header("HEALTH SETTINGS")]
    [Range(0, 100)] public int hp = 100;
    [Range(100, 150)] public int maxHp = 150;


    [Header("MOVEMENT SETTINGS")]

    public CharacterController player;
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;
    public float playerSpeed;
    public Vector3 movePlayer;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public bool onMovement;
    public bool canMove = true;
    public bool onGround;
    public bool running = true;


    [Header("ANIMATOR")]
    public Animator anim;


    //GRAVEDAD
    [Header("GRAVITY SETTINGS")]
    public float gravity = 9.8f;
    public float fallVelocity;
    public float lastMaxFallVelocity;


    //ROLLING
    [Header("ROLLING SETTINGS")]
    public bool rolling = false;
    public Transform mesh;
    public float minFallToRoll;

    //SALTO
    [Header("JUMP SETTINGS")]
    public float jumpForce;
    private bool firstJumpDone = false;

    //SLOPE
    [Header("SLOPE SETTINGS")]
    public bool isOnSlope = false;
    private Vector3 hitNormal;
    private float slideVelocity = 2; //2
    private float slopeForceDown = -2; //-2

    //GROUNDED
    private float distToGround = 0.3f; // 0.3


    //DRINKING
    [Header("DRINKING")]
    public bool drinking = false;

    //SITTING
    [Header("SITTING")]
    public bool sitting = false;

    public Transform raycastdown;

    [Header("ATTACKING")]
    public bool attacking = false;

    [Header("PUSH SETTINGS")]
    public bool pushAndDragging;


    //BLOCKING
    public bool blocking = false;


    //ROPE
    [Header("ROPE SETTINGS")]
    public bool ropping = false;
    public GameObject ropeObject;

    public float timeInteraction;
    public float minTimeInteraction = 0.25f;
    public float ropeSpeed = 2;

    /*[Header("DASH SETTINGS")]
    public bool dashing = false;
    public float dashTime = 0;
    public float dashMaxTime = 1;
    public int dashSpeed = 4;*/


    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        pushAndDragging = GetComponent<PushBox>().pushAndDragging;

        Rolling();
        SpeedController();
        Rope();

        anim.SetBool("OnGround", onGround);


        //MOVIMIENTO
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);

            camDirection();

            movePlayer = playerInput.x * camRight + playerInput.z * camForward;

            movePlayer = movePlayer * playerSpeed;
        if (canMove)
        {
            player.transform.LookAt(player.transform.position + movePlayer);
        }
        
            SetGravity();

            Jump();
        //Dash();

        if (canMove)
        {
            player.Move(movePlayer * Time.deltaTime);
        }
        
        //SE ESTA MOVIENTO EL JUGADOR?
        if (horizontalMove != 0 || verticalMove != 0 || onGround == false)
        {
            onMovement = true;
        }
        else
        {
            onMovement = false;
        }


        //ANIMATOR

        if (onMovement && canMove)
        {
            anim.SetInteger("Speed", 1);
        }
        else
        {
            anim.SetInteger("Speed", 0);
        }
    }

    //DIRECCION DE LA CAMERA
    void camDirection()
    {

        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;


        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    //APLICAMOR LA GRAVEDAD AL JUGADOR
    void SetGravity()
    {

        GroundCheck();

        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            firstJumpDone = false;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            lastMaxFallVelocity = fallVelocity;
        }
        SlideDown();
    }


    //SALTO


    void AttackCJump()
    {
        fallVelocity = jumpForce;
        movePlayer.y = fallVelocity;
    }



        void Jump()
    {
        //PRIMER SALTO

        if (onGround && Input.GetButtonDown("Jump") && canMove && !rolling && !drinking)
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            firstJumpDone = true;
            anim.SetTrigger("Jump");
        }

        //SEGUNDO SALTO
        if (onGround == false && Input.GetButtonDown("Jump") && firstJumpDone && canMove)
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            anim.SetTrigger("FrontFlip");
            firstJumpDone = false;
        }
    }


    public void SlideDown()
    {
        if(Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit && Vector3.Angle(Vector3.up, hitNormal) < 80  && fallVelocity < 0)
        {
            isOnSlope = true;

        }
        else
        {
            isOnSlope = false;
        }

        if (isOnSlope)
        {
            movePlayer.x += hitNormal.x * slideVelocity;
            movePlayer.z += hitNormal.z * slideVelocity;
            playerSpeed = 0;
            movePlayer.y += slopeForceDown;
        }
        else
        {
            SpeedController();
        }

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }


    public void SpeedController()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canMove && onGround  && !sitting && onMovement && !isOnSlope && !rolling && !pushAndDragging && !ropping && !attacking && !drinking)
        {
            playerSpeed = 8;
            anim.speed = 1.25f;
            running = true;
        }
        else if(canMove && !sitting && onMovement && !isOnSlope && !ropping)
        {
            playerSpeed = 4;
            anim.speed = 1;
            running = false;
        }
        else if (pushAndDragging)
        {
            playerSpeed = 8;
            running = false;
        }
        else if (ropping)
        {
            playerSpeed = 0;
            running = false;
        }
        else
        {
            playerSpeed = 0;
            anim.speed = 1;
            running = false;
        }
    }


    void GroundCheck()
    {
        if (Physics.Raycast(raycastdown.position, Vector3.down, distToGround,-1, QueryTriggerInteraction.Ignore)){
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }


    void Rolling()
    {
        anim.SetBool("Rolling", rolling);
        anim.SetFloat("FallSpeed", lastMaxFallVelocity);


        if (lastMaxFallVelocity <= -minFallToRoll && onGround && !anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))
        {
            rolling = true;
            anim.Play("Rolling");
        }


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            rolling = false;
            lastMaxFallVelocity = 0;
        }
    }



    void Block()
    {
        if (Input.GetMouseButtonDown(1) && canMove && onGround && rolling == false)
        {
            blocking = true;
            anim.SetBool("Block", true);
            canMove = false;
        }

        if (Input.GetMouseButtonUp(1) && canMove == false && onGround && rolling == false && blocking)
        {

            blocking = false;
            canMove = true;
            anim.SetBool("Block", false);
        }
    }
  
    /*
    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.E) && !drinking && !rolling && canMove && !blocking && !dashing)
        {
            dashing = true;
            anim.Play("Dash");
            anim.SetBool("Dash", dashing);
            dashTime = 0;
        }

        if(dashing && dashTime <= dashMaxTime)
        {
            dashTime += 1 * Time.deltaTime;
            rolling = false;
            player.Move(transform.forward*dashSpeed*Time.deltaTime);
        }

        if(dashing && dashTime > dashMaxTime)
        {
            dashing = false;
            anim.SetBool("Dash", dashing);
        }
    }
    */

    public void Rope()
    {
        if (ropping)
        {
            timeInteraction += 1 * Time.deltaTime;
            canMove = false;
            transform.position = new Vector3(ropeObject.transform.position.x, transform.position.y, ropeObject.transform.position.z);


            if (verticalMove > 0)
            {
                transform.Translate(0, verticalMove * Time.deltaTime, 0);
            }

            if (verticalMove < 0)
            {
                transform.Translate(0, verticalMove * 2 * Time.deltaTime, 0);
            }

            if (horizontalMove != 0)
            {
                transform.Rotate(0, 200 * -horizontalMove * Time.deltaTime, 0);
            }
        }

        if (ropping && Input.GetKeyDown(KeyCode.Q) && timeInteraction >= minTimeInteraction)
        {
            ropping = false;
            canMove = true;
            lastMaxFallVelocity = 0;
            fallVelocity = 0;
            lastMaxFallVelocity = 0;
        }

        if (ropping && Input.GetButtonDown("Jump") && timeInteraction >= minTimeInteraction)
        {
            ropping = false;
            canMove = true;
            lastMaxFallVelocity = 0;
            fallVelocity = 0;
            lastMaxFallVelocity = 0;
        }

        anim.SetBool("Ropping", ropping);
        anim.SetFloat("RopeMov", verticalMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rope" && canMove && !attacking && !rolling && !drinking && !sitting && Input.GetKeyDown(KeyCode.Q) && !ropping)
        {
            ropeObject = other.gameObject;
            timeInteraction = 0;
            ropping = true;
        }

        if (other.gameObject.tag == "Rope" && ropping && Input.GetKeyDown(KeyCode.Q) && timeInteraction >= minTimeInteraction)
        {
            ropping = false;
            canMove = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rope" && canMove && Input.GetKeyDown(KeyCode.Q) && !ropping)
        {
            ropeObject = other.gameObject;
            timeInteraction = 0;
            ropping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rope" && ropping)
        {
            ropping = false;
            canMove = true;
            lastMaxFallVelocity = 0;
            fallVelocity = 0;
            lastMaxFallVelocity = 0;
        }
    }
}
