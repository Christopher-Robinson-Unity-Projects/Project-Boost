using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float vThrust = 1f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] float soundVolume = 1f;
    [SerializeField] AudioClip thrusterSound;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem thrustLeftParticles;
    [SerializeField] ParticleSystem thrustRightParticles;

    Rigidbody rigBody;
    AudioSource audioSource;

    void Start()
    {
        rigBody = GetComponent<Rigidbody>();
        rigBody.WakeUp();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            //ToggleThrustSound(false);
            StopThrusting();
        }


    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Pressed A - Rotate Left");
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }   
    }

    void StartThrusting()
    {
        rigBody.AddRelativeForce(Vector3.up * Time.deltaTime * vThrust);
        PlaySound(thrusterSound, soundVolume);
        PlayParticles(thrustParticles);
    }

    void StopThrusting()
    {
        StopSound();
        StopParticles(thrustParticles);
    }
    
    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        PlayParticles(thrustLeftParticles);
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        PlayParticles(thrustRightParticles);
    }

    void StopRotating()
    {
        StopParticles(thrustLeftParticles);
        StopParticles(thrustRightParticles);
        
    }

    void ApplyRotation(float rotationThrust)
    {
        rigBody.freezeRotation = true; // freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThrust);
        rigBody.freezeRotation = false; // unfreeze rotation so physics system takes over
    }

    public void FreezeMovement()
    {
        rigBody.Sleep();
    }

    public void PlaySound(AudioClip clip, float clipVolume)
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(clip, clipVolume);
    }

    public void StopSound()
    {
        if (audioSource.isPlaying) audioSource.Stop();
    }

    public void PlayParticles(ParticleSystem ps)
    {
        if (!ps.isPlaying) ps.Play();
    }

    public void StopParticles(ParticleSystem ps)
    {
        if (ps.isPlaying) ps.Stop();
    }


    public void allThrustParticlesStop()
    {
        thrustParticles.Stop();
        thrustLeftParticles.Stop();
        thrustRightParticles.Stop();
    }
}
