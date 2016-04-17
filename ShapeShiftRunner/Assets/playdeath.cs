using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playdeath : MonoBehaviour {

    public AudioClip hahaSound;
    public AudioClip ohnoSound;

	// Use this for initialization
	void Start () {
        StartCoroutine(LaughRoutine());
	}

    IEnumerator LaughRoutine()
    {

        while (true)
        {
            AudioSource.PlayClipAtPoint(ohnoSound, Camera.main.transform.position);
            yield return new WaitForSeconds(2.0f);
            AudioSource.PlayClipAtPoint(hahaSound, Camera.main.transform.position);
            yield return new WaitForSeconds(5.0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
	}
}
