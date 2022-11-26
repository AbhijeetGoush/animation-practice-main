using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    HelperScript helper;
    Animator anim;
    Rigidbody2D rb;
    private string currentState;

    const string PLAYER_IDLE = "playerIdle";
    const string PLAYER_RUN = "playerRun";
    const string PLAYER_JUMP = "playerJump";
    const string PLAYER_ATTACK = "playerAttack";
    bool touchingplatform;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.Play("playerIdle");
        helper = gameObject.AddComponent<HelperScript>();
        touchingplatform = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector2 vel = rb.velocity;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = 7;
            ChangeAnimationState(PLAYER_RUN);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            vel.x = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -7;
            ChangeAnimationState(PLAYER_RUN);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            vel.x = 0;
        }
        if (vel.x == 0)
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
        if (Input.GetKey("left"))
        {
            helper.FlipObject(true);
        }
        if (Input.GetKey("right"))
        {
            helper.FlipObject(false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && (touchingplatform == true))
        {
            vel.y = 6;

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeAnimationState(PLAYER_ATTACK);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
        
        rb.velocity = vel;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingplatform = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingplatform = false;


        }
    }
}
