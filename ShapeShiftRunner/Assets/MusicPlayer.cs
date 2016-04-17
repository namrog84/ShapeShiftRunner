using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {


    static MusicPlayer singleton;
    
	// Use this for initialization
	void Start () {
	    if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
