using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMovement2D : MonoBehaviour
{
    private Rigidbody2D     m_PlayerRB;
    private float           m_InputX;
    private float           m_InputY;
    private float           m_JumpTimeCounter;
    private bool            m_IsJumping;

    // Use these variable to the check if the player is grounded or not
    [Header                 ("Player Grounded check")]
    public Transform        feetPos;
    public LayerMask        whatIsGround;
    public float            checkRadius;
    public bool             isGrounded;

    [Space]
    [Header                 ("Locomotion Settings")]
    public float            speed = 5f;
    public float            jumpForce = 20f;
    public float            jumpTime = 0.25f;   

    void Start()
    {
        m_PlayerRB = GetComponent<Rigidbody2D>();
        m_PlayerRB.gravityScale = 4f;
    }

    void Update()
    {
        // Getting the Input
        m_InputX = Input.GetAxis("Horizontal");
        m_InputY = Input.GetAxis("Vertical");

        //Flip the player based on the Input
        if (m_InputX > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (m_InputX < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);


        // Check whether the player is grounded or not
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // If the player is grounded enable him to jump 
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            m_PlayerRB.velocity = Vector2.up * jumpForce;
            m_IsJumping = true;
            m_JumpTimeCounter = jumpTime;
        }

        /*
         * Enabling the player Mario like jump
         * As long as the player keeps holding the space button in this case the space key
         * we add a little bit of jump force in the time he presses it and immediately apply the gravity back
         */
        if (Input.GetKey(KeyCode.Space) && m_IsJumping)
        {
            if (m_JumpTimeCounter > 0)
            {
                m_PlayerRB.velocity = Vector2.up * jumpForce;
                m_JumpTimeCounter -= Time.deltaTime;
            }
            else
                m_IsJumping = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
            m_IsJumping = false;
    }

    private void FixedUpdate()
    {
        // Moving the player in X-axis using the InputX every physics cycle
        m_PlayerRB.velocity = new Vector2(m_InputX * speed, m_PlayerRB.velocity.y);
    }

}