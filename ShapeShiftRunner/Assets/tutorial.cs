using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public int skipCount = 0;
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            skipCount++;
        }

        if(skipCount >= 2)
        {
            SceneManager.LoadScene(1);
        }
	
	}
}
