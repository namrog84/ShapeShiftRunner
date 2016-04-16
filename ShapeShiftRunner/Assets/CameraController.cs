using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
    
    private float offsetX = 14;
    private float damping = 1.0f;
    private float velocity = 0;

    // Update is called once per frame
    void Update () {
        var pos = transform.position;
        float currentX = pos.x - offsetX;
        float targetX = player.transform.position.x;
        
        currentX = Mathf.SmoothDamp(currentX, targetX, ref velocity, 0.05f, 15);

        pos.x = currentX + offsetX;


        transform.position = pos;
        
	}
}
