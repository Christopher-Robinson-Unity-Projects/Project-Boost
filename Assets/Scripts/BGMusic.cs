using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private static BGMusic instance = null;
    public static BGMusic Instance{
        get { return instance; }
    }
    void Awake(){
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
