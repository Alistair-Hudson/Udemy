using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrusterSpeed = 5f;
    [SerializeField] float rotationalSpeed = 5f;

    bool hasPackage = false;

    Rigidbody rigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Thrust();
        RotateZ();
        RotateX();
    }

    private void RotateZ()
    {
        rigidbody.freezeRotation = true; //take manual control of rotation
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationalSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationalSpeed * Time.deltaTime);
        }
        rigidbody.freezeRotation = false; //release manual control of rotation
    }

    private void RotateX()
    {
        rigidbody.freezeRotation = true; //take manual control of rotation
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(-Vector3.left * rotationalSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.left * rotationalSpeed * Time.deltaTime);
        }
        rigidbody.freezeRotation = false; //release manual control of rotation

    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrusterSpeed * Time.deltaTime);
            if (!audioSource.isPlaying)// avoid audio layering
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                if (hasPackage)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
                }
                break;
            case "Package":
                hasPackage = true;
                break;
            default:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }
}
