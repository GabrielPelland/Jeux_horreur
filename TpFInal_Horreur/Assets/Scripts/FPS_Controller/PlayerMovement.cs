/* Adapte de: 
 * https://www.youtube.com/watch?v=LqnPeqoJRFY
 * https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
 * https://docs.unity3d.com/2019.3/Documentation/ScriptReference/Rigidbody.AddForce.html
 * https://docs.unity3d.com/ScriptReference/Vector3-normalized.html
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //VARIABLES

    //Parametres mouvements
    [Header("Movement")]
    float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    float groundDrag = 6f;
    float airDrag = 2f;
    float jumpForce = 15f;

    [Header("Vitesse")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    //Mouvements
    float horizontalMovement;
    float verticalMovement;
    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    //Saut
    [Header("GroundDetection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;
    float playerHeight = 2f;

    //Pentes
    RaycastHit slopeHit;

    //Orientation
    [SerializeField] Transform orientation;

    //Elements
    Rigidbody rb;

    //Touches
    [Header("Touches")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    //Commencement du jeu
    private void Start()
    {
        //Assignation des elements
        rb = GetComponent<Rigidbody>();

        //Bloquer la rotation
        rb.freezeRotation = true;
    }

    //Sur updates
    private void Update()
    {
        //Verification au sol
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Appeler methodes
        TouchInput();
        ControlDrag();
        ControlSpeed();

        //Verifiaction touche saut
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    //Controle de la vitesse
    void ControlSpeed ()
    {
        if(Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    //Rigidbody drag
    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    //Detection des touches de mouvement
    void TouchInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    //Mouvement du joueur gauche et droite
    void MovePlayer()
    {
        if(isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        
    }

    //Mouvement du joueur saut
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    //Pentes
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            //Verification pentes
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

}
