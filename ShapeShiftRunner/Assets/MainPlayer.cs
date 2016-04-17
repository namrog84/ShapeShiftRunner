using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

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

    public AudioClip woosh;
    public AudioClip woohoo;
    public AudioClip aghh;
    public AudioClip deadsound;

    // Update is called once per frame
    void Update()
    {
        if (world == null)
        {
            world = GameObject.Find("GameManager").GetComponent<WorldSpawner>();
        }

        if (!isAlive)
        {
            MyRigidBody2d.velocity = Vector3.zero;
            return;
        }
        if(transform.position.x > 370)
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<SpriteRenderer>().sprite = SquareSprite;
            GetComponent<CircleCollider2D>().sharedMaterial = NonBounceMaterial;
            MyRigidBody2d.freezeRotation = true;
            transform.rotation = Quaternion.identity;
            currentState = PlayerShapeState.Square;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<SpriteRenderer>().sprite = TriangleSprite;
            GetComponent<CircleCollider2D>().sharedMaterial = NonBounceMaterial;
            MyRigidBody2d.freezeRotation = true;
            transform.rotation = Quaternion.identity;
            currentState = PlayerShapeState.Triangle;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //GetComponent<BoxCollider2D>()
            GetComponent<SpriteRenderer>().sprite = CircleSprite;
            GetComponent<CircleCollider2D>().sharedMaterial = BounceMaterial;
            MyRigidBody2d.freezeRotation = false;
            currentState = PlayerShapeState.Circle;
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            ActionButton();
        }

        if (MyRigidBody2d.velocity.y == 0)
        {
            groundTick++;
        }
        else
        {
            groundTick = 0;
        }
        if(groundTick >= 2)
        {
            canJump = true;
        }

        if (transform.position.y < 5)
        {
            isAlive = false;
            StartCoroutine(Death());

        }

        if (isAlive)
        {
            if (MyRigidBody2d.velocity.x <= 0)
            {
                deathCounter++;
            }
            else
            {
                deathCounter = 0;
            }
            if (deathCounter > 3)
            {
                isAlive = false;
                StartCoroutine(Death());
            }
        }

    }

    bool canJump = true;
    bool isAlive = true;
    int deathCounter = 0;
    IEnumerator Death()
    {
        AudioSource.PlayClipAtPoint(aghh, Camera.main.transform.position);
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(2);
    }

    public void FixedUpdate()
    {

        if(!isAlive)
        {
            //do nothing when dead
            return;
        }
        var currVel = MyRigidBody2d.velocity;
        currVel.x = Mathf.Max(6, currVel.x);
        MyRigidBody2d.velocity = currVel;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.transform.tag);

        if (other.transform.tag == "Ground")
        {
            GetComponentInChildren<ParticleSystem>().Clear();
            GetComponentInChildren<ParticleSystem>().Play();
            if(Time.time - lastWoosh > 1)
            {
                AudioSource.PlayClipAtPoint(woosh, Camera.main.transform.position);
            }
            canJump = true;
            lastWoosh = Time.time;
        }
        if (other.transform.tag == "Baddie" && currentState != PlayerShapeState.Square)
        {
            
            AudioSource.PlayClipAtPoint(woohoo, Camera.main.transform.position);
            isAlive = false;
            StartCoroutine(Death());
        }
        if (other.transform.tag == "Baddie" && currentState == PlayerShapeState.Square)
        {
            AudioSource.PlayClipAtPoint(deadsound, Camera.main.transform.position);
            
            Destroy(other.gameObject);
        }
    }
    float lastWoosh;
    private void ActionButton()
    {
        switch (currentState)
        {
            case PlayerShapeState.Triangle:
                if (canJump)
                {
                    canJump = false;
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
