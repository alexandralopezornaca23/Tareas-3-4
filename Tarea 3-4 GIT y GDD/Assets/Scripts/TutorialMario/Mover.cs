using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Mover : MonoBehaviour
{
    enum Direction { Left = -1, None = 0, Right = 1 };
    Direction currentDirection = Direction.None; //El valor por defecto.

    public float speed;
    public float aceleration;
    public float maxVelocity;
    public float friction;
    float currentVelocity = 0f;

    public float jumpForce;
    public float maxJumpingTime = 1f;
    bool isJumping;
    float jumpTimer = 0f;
    float defaultGravity;


    Rigidbody2D rb2D;
    Collisions collisions;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        collisions = GetComponent<Collisions>();
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultGravity = rb2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (isJumping) 
        {
            if (rb2D.velocity.y < 0f ) 
            {
                rb2D.gravityScale = defaultGravity;
                if (collisions.Grounded())
                {
                    isJumping = false;
                    jumpTimer = 0f;
                }
            }
            else if (rb2D.velocity.y > 0f)
            {
                if (Input.GetKey(KeyCode.Space)) 
                {
                    jumpTimer += Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.Space)) 
                {
                    if (jumpTimer < maxJumpingTime)
                    {
                        rb2D.gravityScale = defaultGravity * 3f;
                    }
                }
            }
        }

        currentDirection = Direction.None; //para que no se desplace infinitamente.

        /* para moverse:
        
        transform.Translate(1, 0, 0);
        transform.Translate(speed, 0, 0);
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Translate(-speed * Time.deltaTime, 0, 0);
            //MoveLeft();
            currentDirection = Direction.Left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Translate(speed * Time.deltaTime, 0, 0);
            //MoveRight();
            currentDirection = Direction.Right;
        }
    }

    private void FixedUpdate()
    {
        /*
        Vector2 forceAcceleration = new Vector2((int)currentDirection * aceleration, 0f);
        rb2D.AddForce(forceAcceleration);
        //controlar que la velocidad no pase de la maxima velocidad:
        float velocityX = Mathf.Clamp(rb2D.velocity.x, -maxVelocity, maxVelocity);

        //Vector2 velocity = new Vector2((int)currentDirection * speed, rb2D.velocity.y);
        Vector2 velocity = new Vector2(velocityX, rb2D.velocity.y);
        rb2D.velocity = velocity;
        */

        currentVelocity = rb2D.velocity.x;

        if (currentDirection > 0f) //si estas tocandola tecla de dirección hacia la derecha
        {
            if (currentVelocity < 0f) 
            {
                currentVelocity += (aceleration + friction) * Time.deltaTime;
            }
            else if (currentVelocity < maxVelocity)
            {
                currentVelocity += aceleration * Time.deltaTime;
            }
        }
        else if (currentDirection < 0f) //si estas tocandola tecla de dirección hacia la izquierda
        {
            if (currentVelocity > 0f)
            {
                currentVelocity -= (aceleration + friction) * Time.deltaTime;
            }
            else if (currentVelocity > -maxVelocity)
            {
                currentVelocity -= aceleration * Time.deltaTime;
            }
        }
        else //si ya estoy cerca del 0 en velocidad (entre 1 i -1)
        {
            if (currentVelocity > 1f)
            {
                currentVelocity -= friction * Time.deltaTime;
            }
            else if (currentVelocity < -1f)
            {
                currentVelocity += friction * Time.deltaTime;
            }
            else
            {
                currentVelocity = 0f;
            }
        }

        Vector2 velocity = new Vector2(currentVelocity, rb2D.velocity.y);
        rb2D.velocity = velocity;
    }

    void Jump()
    {
        if (collisions.Grounded() && !isJumping)
        {
            isJumping = true;
            Vector2 force = new Vector2(0f, jumpForce); //las fuerzas en x - y para saltar
            rb2D.AddForce(force, ForceMode2D.Impulse);
        }        
    }

    /*Moverse por impulso:
    void MoveRight()
    {
        //Vector2 force = new Vector2(10f, 0f); //las fuerzas en x - y para moverse a la derecha
        //rb2D.AddForce(force, ForceMode2D.Impulse);
        Vector2 velocity = new Vector2(1f, 0f);
        rb2D.velocity = velocity;
    }

    void MoveLeft()
    {
        Vector2 force = new Vector2(-10f, 0f); //las fuerzas en x - y para moverse a la izquierda
        rb2D.AddForce(force, ForceMode2D.Impulse);
    }
    */
}
