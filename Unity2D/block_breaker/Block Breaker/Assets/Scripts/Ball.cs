using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // configuration paramters
    [SerializeField] Paddle paddle1;
    [SerializeField] float velocityX = 2f;
    [SerializeField] float velocityY = 3f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randFactor = 0.2f;

    // state
    Vector2 paddle2Ball;

    // Cached Componets
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody;
    GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        paddle2Ball = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody = GetComponent<Rigidbody2D>();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStatus.GetHasStarted())
        {
            LockBAll2Paddle();
            LaunchBallOnMouseClick();
        }
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody.velocity = new Vector2(velocityX, velocityY);
            gameStatus.SetHasStarted(true);
        }
    }

    private void LockBAll2Paddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x,
                                        paddle1.transform.position.y);
        transform.position = paddlePos + paddle2Ball;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(0f, randFactor), 
                                            UnityEngine.Random.Range(0f, randFactor));
        if (gameStatus.GetHasStarted())
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody.velocity += velocityTweak;
        }
    }
}
