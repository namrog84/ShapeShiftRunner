using UnityEngine;
using System.Collections;
using System;

public class MainPlayer : MonoBehaviour {

    Rigidbody2D MyRigidBody2d;
	// Use this for initialization
	void Start () {
        MyRigidBody2d = GetComponent<Rigidbody2D>();
    }

    public float horizontalAccel = 10.0f;
    private float gravity = -9.8f;
    public Vector3 speed = new Vector3(0, 0);
    private float maxSpeed = 13.0f;


    public Sprite SquareSprite;
    public Sprite TriangleSprite;
    public Sprite CircleSprite;
    private WorldSpawner world;
    
    public PhysicsMaterial2D BounceMaterial;
    public PhysicsMaterial2D NonBounceMaterial;

    enum PlayerShapeState { Square, Circle, Triangle};
    PlayerShapeState currentState;
    int groundTick = 0;



    // Update is called once per frame
    void Update ()
    {
        if (world == null)
        {
            world = GameObject.Find("GameManager").GetComponent<WorldSpawner>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<SpriteRenderer>().sprite = SquareSprite;
            GetComponent<BoxCollider2D>().sharedMaterial = NonBounceMaterial;
            currentState = PlayerShapeState.Square;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<SpriteRenderer>().sprite = TriangleSprite;
            GetComponent<BoxCollider2D>().sharedMaterial = NonBounceMaterial;
            currentState = PlayerShapeState.Triangle;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponent<SpriteRenderer>().sprite = CircleSprite;
            GetComponent<BoxCollider2D>().sharedMaterial = BounceMaterial;
            currentState = PlayerShapeState.Circle;
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            ActionButton();
        }

        if(MyRigidBody2d.velocity.y == 0)
        {
            groundTick++;
        }
        else
        {
            groundTick = 0;
        }
    }
    public void FixedUpdate()
    {
        var currVel = MyRigidBody2d.velocity;
        currVel.x = Mathf.Max(6, currVel.x);
        MyRigidBody2d.velocity = currVel;
    }

    private void ActionButton()
    {
        Debug.Log("what");
        switch (currentState)
        {
            case PlayerShapeState.Triangle:
                if (groundTick >= 3)
                {
                    var vel = MyRigidBody2d.velocity;
                    vel.y = 8;
                    MyRigidBody2d.velocity = vel;
                    //.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
                }
                break;
            case PlayerShapeState.Circle:
                {
                    MyRigidBody2d.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);
                }
                break;
            case PlayerShapeState.Square:
            default:
                {
                    var vel = MyRigidBody2d.velocity;
                    vel.y = -15;
                    MyRigidBody2d.velocity = vel;
                    //.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
                }
                break;
        }

        
    }
}
