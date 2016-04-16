using UnityEngine;
using System.Collections;
using System;

public class MainPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public float horizontalAccel = 10.0f;
    private float gravity = -9.8f;
    public Vector3 speed = new Vector3(0, 0);
    private float maxSpeed = 13.0f;


    public Sprite SquareSprite;
    public Sprite TriangleSprite;
    public Sprite CircleSprite;

    // Update is called once per frame
    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<SpriteRenderer>().sprite = SquareSprite;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<SpriteRenderer>().sprite = TriangleSprite;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponent<SpriteRenderer>().sprite = CircleSprite;
        }

        //pos.x += speed * Time.deltaTime;

        //pos.y += gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        speed.x += horizontalAccel * Time.deltaTime;
        speed.x = Mathf.Min(speed.x, maxSpeed);

        speed.y += 2.5f * gravity * Time.deltaTime;

        var pos = transform.position;
        pos += speed * Time.deltaTime;
        transform.position = pos;
    }

    private void Jump()
    {
        speed.y = 12f;
    }
}
