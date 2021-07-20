using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
 
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] float crashVolume = 1f;
    [SerializeField] float successVolume = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticle;

    Movement movement;
    AudioSource audioSource;

    private int currentScene;
    private int totalLevels; 
    private bool isTransitioning; 

   void Start()
   {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        movement.enabled = true;

        currentScene = SceneManager.GetActiveScene().buildIndex;
        totalLevels = SceneManager.sceneCountInBuildSettings;

        isTransitioning = false;
   }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning){ return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default: 
                Crash();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        movement.FreezeMovement();
        movement.StopSound();
        movement.allThrustParticlesStop();
        successParticle.Play();
        audioSource.PlayOneShot(successSound, successVolume);
        movement.enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void Crash()
    {
        isTransitioning = true;
        movement.StopSound();
        movement.allThrustParticlesStop();
        crashParticles.Play();
        audioSource.PlayOneShot(crashSound, crashVolume);
        movement.enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        //ReloadLevel();
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }
   
    void LoadNextLevel()
    {
        int nextScene = currentScene + 1;
        //If next level exists, load it else go to level 1
        if (nextScene == totalLevels)
        {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }
}
