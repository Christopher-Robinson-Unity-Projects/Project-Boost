using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTimer : MonoBehaviour
{
    [SerializeField] float delayTime = 0f;
    float runTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf) {
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer(){

        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(true);
    }
}
