using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;

public class Player : MonoBehaviour
{
    //Configs
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;


    //Constants
    string HORIZONTAL_MASK = "Horizontal";
    string VERTICAL_MASK = "Vertical";
    string GROUND_MASK = "Ground";
    string LADDER_MASK = "Ladders";
    string HAZARDS_MASK = "Hazards";
    string ENEMY_MASK = "Enemies";

    string RUNNING_BOOL = "isRunning";
    string CLIMBING_BOOL = "isClimbing";

    string DEATH = "Death";
    string JUMP = "Jump";



    //State
    bool isAlive = true;


    //Cached Data
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    BoxCollider2D myCollider;
    float defaultGravityScale;


    //Messages


    //Methods
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        defaultGravityScale = myRigidbody2D.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
    }

    private void Run()
    {
        var directionX = Input.GetAxis(HORIZONTAL_MASK);
        myRigidbody2D.velocity = new Vector2(directionX*runSpeed, myRigidbody2D.velocity.y);

        myAnimator.SetBool(RUNNING_BOOL, Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon);
    }

    private void FlipSprite()
    {
        bool playerHasHorSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown(JUMP) && 
            myCollider.IsTouchingLayers(LayerMask.GetMask(GROUND_MASK)))
        {
            myAnimator.SetTrigger(JUMP);
            myRigidbody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void ClimbLadder()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask(LADDER_MASK)))
        {
            myRigidbody2D.gravityScale = 0;

            var directionY = Input.GetAxis(VERTICAL_MASK);
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, directionY * runSpeed);

            myAnimator.SetBool(CLIMBING_BOOL, Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon);
        }
        else
        {
            myRigidbody2D.gravityScale = defaultGravityScale;
            myAnimator.SetBool(CLIMBING_BOOL, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask(HAZARDS_MASK, ENEMY_MASK)))
        {
            isAlive = false;
            myAnimator.SetTrigger(DEATH);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

    }
}
