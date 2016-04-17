using UnityEngine;
using System.Collections;

public class winGame : MonoBehaviour
{


    public AudioClip woohoo;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(theEnd());
    }

    IEnumerator theEnd()
    {
        AudioSource.PlayClipAtPoint(woohoo, Camera.main.transform.position);
        yield return new WaitForSeconds(2);
        AudioSource.PlayClipAtPoint(woohoo, Camera.main.transform.position);
        yield return new WaitForSeconds(2);
        AudioSource.PlayClipAtPoint(woohoo, Camera.main.transform.position);
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {


    }
}
