using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public bool playerAlive = true;
    Vector3 Velocity = Vector3.zero;

    public Rigidbody playerRigidBody;
    public Camera playerCamera;
    public Animator playerAnimator;

    public float m_GroundCheckDistance = 3.2f;
    public bool m_IsGrounded;
    public bool m_IsSprinting;
    public bool m_IsCrouching;
    public Vector3 m_GroundNormal;

    public float speed;
    public float normalSpeed = 25.0f;
    public float sprintingSpeed = 40.0f;
    public float crouchingSpeed = 15.0f;
    public float horizontalTurnSpeed = 3f;
    public float verticalTurnSpeed = 3f;
    public float jumpSpeed = 150.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (playerAlive)
        {
            Vector3 pos = gameObject.transform.position;

            Velocity = playerCamera.transform.forward * Input.GetAxis("Forward") * speed;
            Velocity += playerCamera.transform.right * Input.GetAxis("Horizontal") * speed;

            ApplyPlayerRotation();
            ApplyCameraRotation();
            CheckGroundStatus();

            Velocity = Vector3.ProjectOnPlane(Velocity, m_GroundNormal);

            if (!m_IsGrounded)
            {
                playerRigidBody.AddForce(Vector3.down * 9f, ForceMode.Impulse);
            }

            //MOVEMENT CONTROLS//

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //could be based on is grounded, but leaving it like this to allow level completion
                playerRigidBody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Crouching();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("isSprinting", true);
                speed = sprintingSpeed;
                m_IsSprinting = true;
            }

            else
            {
                playerAnimator.SetBool("isSprinting", false);
                speed = normalSpeed;
                m_IsSprinting = false;
            }

            playerRigidBody.velocity = Velocity;
        }
    }

    void ApplyPlayerRotation()
    {
        gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * horizontalTurnSpeed, 0);
    }

    void ApplyCameraRotation()
    {
        playerCamera.transform.Rotate(-Input.GetAxis("Mouse Y") * verticalTurnSpeed, 0, 0);
    }

    void CheckGroundStatus()
    {
        #if UNITY_EDITOR
                //helper to visualize the ground check ray in the scene view
                Debug.DrawLine(transform.position + (Vector3.up * 0.1f), 
                    transform.position + (Vector3.down * 0.1f) + 
                    (Vector3.down * m_GroundCheckDistance), 
                    Color.red);
        #endif

        RaycastHit hitinfo;
        bool isHit = Physics.Raycast(transform.position + (Vector3.up * 0.01f), Vector3.down, out hitinfo, m_GroundCheckDistance);
        //bool isHit = Physics.Raycast(transform.position, -Vector3.up)

        if (isHit)
        {
            m_GroundNormal = hitinfo.normal;
            m_IsGrounded = true;
        }

        else
        {
            m_GroundNormal = Vector3.up;
            m_IsGrounded = false;
        }
    }

    //JUMPING
    //fix this, make it smooth

    void Jumping()
    {
        //if(is_Grounded)
        //jumping animation

        //else{ nothing };
    }

    void Crouching()
    {
        if (!m_IsCrouching)
        {
            m_IsCrouching = true;
            speed = crouchingSpeed;
            playerAnimator.SetBool("isCrouching", true);
            //set animator to crouch animation
            //move camera down

        }

        else
        {
            m_IsCrouching = false;
            speed = normalSpeed;
            playerAnimator.SetBool("isCrouching", false);
        }
    }

}
