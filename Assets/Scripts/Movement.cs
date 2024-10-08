using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float RotatingForce = 100f;
    [SerializeField] AudioClip thrustClip;
    
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W))
            StartThrusting();
        else
            StopThrusting();
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
            RotateLeft();
        else if (Input.GetKey(KeyCode.D))
            RotateRight();
        else
            StopRotating();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);

        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(thrustClip);

        if (!mainThrustParticles.isPlaying)
            mainThrustParticles.Play();
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-RotatingForce);
        if (!leftThrustParticles.isPlaying)
            leftThrustParticles.Play();
    }

    void RotateLeft()
    {
        ApplyRotation(RotatingForce);
        if (!rightThrustParticles.isPlaying)
            rightThrustParticles.Play();
    }

    void StopRotating()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   //freezing rotations so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  //Unfreezing rotations so that physics system can take over
    }
}
