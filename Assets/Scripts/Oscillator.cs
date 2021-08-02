using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    Vector3 offset;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    [SerializeField] float delayTime = 0f;
    const float tau = Mathf.PI * 2;  // constant value of 2pi
    string objectName;
    float movementFactor;
    float cycles;
    float rawSinWave;
    float startTime, elapsedTime;
    bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        objectName = gameObject.name;
        startingPosition = transform.position;
        startTime = Time.time;
        elapsedTime = 0;
        isRunning = false;
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        //MoveObjectSin();
        MoveObjectPingPong();
    }

    IEnumerator Timer(){
        yield return new WaitForSeconds(delayTime);
        elapsedTime = 0;
        isRunning = true;
    }

    private void MoveObjectSin()
    {
        if (isRunning){
        if (period <= Mathf.Epsilon) { return; }
        if (elapsedTime == 0 || elapsedTime <= Mathf.Epsilon) cycles = 0f;
        else cycles = elapsedTime / period; // continually growing over time
        rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f; //recalculates to go from 0 to 1
        offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
        }
    }

    private void MoveObjectPingPong()
    {
        if (isRunning){
        if (period <= Mathf.Epsilon) { return; }
        if (elapsedTime == 0 || elapsedTime <= Mathf.Epsilon) cycles = 0f;
        else cycles = elapsedTime / period; // continually growing over time
        rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to 1
        movementFactor = Mathf.PingPong(cycles, 1); //recalculates to go from 0 to 1
        offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
        }
    }
}
