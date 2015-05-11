using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
    // Exposed Character Variables
    public float m_Speed          = 5.0f;
    public float m_JumpForce      = 10.0f;
    public int   m_JumpCooldown   = 30;
    public float m_GroundedHeight = 5.0f;
    public float m_BlowCooldown   = 1.0f;

    private float m_BlowTimer;
    private bool m_IsBlowing = false;

    public GameObject m_BlowObject;
    private GameObject m_TempBlowZone;

    // Exposed Sprite Variables
    public Sprite[] m_Sprites;

    void Awake()
    {
        // Player Stuff
        m_PlayerBody = GetComponent<Rigidbody2D>();
        m_PlayerSprite = GetComponent<SpriteRenderer>();
        m_PlayerPos = m_PlayerBody.transform.position;
        m_PlayerPos = Vector2.zero;

        // Sprite Stuff
        m_Sprites = Resources.LoadAll<Sprite>("LibrarianSprites");
    }

	void Start () 
    {
        // The player starts grounded
        m_IsGrounded   = true;

        // Set the player to Idle
        m_ELibrarianState = LibrarianState.e_IDLE;

        m_BlowTimer = m_BlowCooldown;

        if (m_Sprites == null)
        {
            Debug.Log("WARNING: Sprites aren't initialized");
        }
	}

	void Update () 
    {
        // What State Am I In?
        Debug.Log(m_ELibrarianState);
        // Increment Timers
        m_JumpTimer++;
        m_SpriteTimer++;
        // Update the Player's movement after checking if it's grounded
        UpdatePlayerMovement(m_IsGrounded);
        UpdatePlayerSprite();
        if(m_IsBlowing)
        {
            if (m_BlowTimer <= 0)
            {
                m_BlowTimer = m_BlowCooldown;
                m_IsBlowing = false;
                Destroy(m_TempBlowZone);
            }
            m_BlowTimer -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !m_IsBlowing)
        {
            m_IsBlowing = true;
            m_TempBlowZone = (GameObject)Instantiate(m_BlowObject, transform.position, Quaternion.identity);

            if (m_ELibrarianState == LibrarianState.e_RUNNING_LEFT)
            {
                m_TempBlowZone.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0.0f);
            }
            else
            {
                m_TempBlowZone.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0.0f);
            }  
        }
	}

    // Physics updates are done here
    void FixedUpdate ()
    {
        PlayerIsGroundedCheck();
    }

    void UpdatePlayerSprite()
    {
        switch(m_ELibrarianState)
        {
            case LibrarianState.e_IDLE:
                m_PlayerSprite.sprite = m_Sprites[7];
                break;

            case LibrarianState.e_RUNNING_RIGHT:
                    if(m_SpriteTimer >= 0)
                        m_PlayerSprite.sprite = m_Sprites[0];

                    if(m_SpriteTimer >= 30)
                        m_PlayerSprite.sprite = m_Sprites[1];

                    if (m_SpriteTimer >= 60)
                        m_PlayerSprite.sprite = m_Sprites[2];

                    if (m_SpriteTimer >= 90)
                        m_PlayerSprite.sprite = m_Sprites[3];

                    else if (m_SpriteTimer >= 120)
                        m_SpriteTimer = 0;
                break;

            case LibrarianState.e_RUNNING_LEFT:
                m_PlayerSprite.sprite = m_Sprites[1];
                break;

            case LibrarianState.e_JUMPING:
                m_PlayerSprite.sprite = m_Sprites[2];
                break;
            default:
                Debug.Log("I hit the default!");
                break;
        }
    }

    void UpdatePlayerMovement(bool isGrounded)
    {
        // Move Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            m_PlayerBody.AddForce(transform.right * m_Speed);
            m_ELibrarianState = LibrarianState.e_RUNNING_RIGHT;
        }
        // Move Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            m_PlayerBody.AddForce(-transform.right * m_Speed);
            m_ELibrarianState = LibrarianState.e_RUNNING_LEFT;
        }
        // Jump
        if (Input.GetAxis("Vertical") > 0 && 
            isGrounded &&
            m_JumpTimer > m_JumpCooldown)
        {
            m_JumpTimer = 0;
            m_PlayerBody.AddForce(transform.up * m_JumpForce);
            m_ELibrarianState = LibrarianState.e_JUMPING;
        }
        // Idle
        else
        {
            Debug.Log("i hit my idle");
            m_ELibrarianState = LibrarianState.e_IDLE;
        }
    }

    void PlayerIsGroundedCheck()
    {
        // Player position x, y, modify the x value to be the minimum bounds of the player
        Vector2 playerFeet = new Vector2(m_PlayerSprite.transform.localPosition.x,
                                         m_PlayerSprite.transform.localPosition.y);

        RaycastHit2D aHit = Physics2D.Raycast(playerFeet, -transform.up);

        if(aHit.collider != null &&
           aHit.collider.tag == "Ground" && 
           aHit.distance <= m_GroundedHeight)
        {
            m_IsGrounded = true;
        }

        else
        {
            m_IsGrounded = false;
        }
    }

    // private variables
    Rigidbody2D    m_PlayerBody;
    SpriteRenderer m_PlayerSprite;
    Vector2        m_PlayerPos;
    bool           m_IsGrounded;
    Ray2D          m_Ray;

    int m_JumpTimer = 30;
    int m_SpriteTimer = 0;

    LibrarianState m_ELibrarianState;

    enum LibrarianState
    {
        e_IDLE,
        e_RUNNING_RIGHT,
        e_RUNNING_LEFT,
        e_JUMPING,
        e_FALLING,
        e_BLOWING,  
    }
}
