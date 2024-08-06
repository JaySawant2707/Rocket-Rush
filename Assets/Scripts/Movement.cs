using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float RotatingForce = 100f;
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        processThrust();
        processRotate();
    }

    void processThrust()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);

            if (!audioSource.isPlaying){
                audioSource.Play();
            }  
        }
        else{
             audioSource.Stop();
        }
    }

    void processRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            applyRotation(RotatingForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            applyRotation(-RotatingForce);
        }
    }

    void applyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   //freezing rotations so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  //Unfreezing rotations so that physics system can take over
    }
}
