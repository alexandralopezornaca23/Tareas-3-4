using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    enum State { Default = 0, Big = 1, Fire = 2 }
    State currentState = State.Default;
    
    public GameObject stompBox;
    //public GameObject headBox;
    Mover mover;
    Collisions collisions;
    Animations animations;
    Rigidbody2D rb2D;

    bool isDead;

    public void Awake()
    {
        mover = GetComponent<Mover>();
        collisions = GetComponent<Collisions>();
        animations = GetComponent<Animations>();    
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if(rb2D.velocity.y < 0 && !isDead)
        {
            stompBox.SetActive(true);
        }
        else 
        {
            stompBox.SetActive(false);
        }

        //if (rb2D.velocity.y > 0f && !isDead)
        //{
        //    headBox.SetActive(true);
        //}
        //else
        //{
        //    headBox.SetActive(false);
        //}

        //Para comprobar que las transformaciones funcionan
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
            animations.PowerUp();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            animations.Hit();
        }
    }

    public void Hit()
    {
        //Debug.Log("Hit");
        if (currentState == State.Default) 
        {
            Dead();
        }
        else
        {
            Time.timeScale = 0f;
            animations.Hit();
        }        
    }

    public void Dead()
    {
        if (!isDead) 
        {
            //mover.inputMoveEnabled = false;
            isDead = true;
            collisions.Dead();
            mover.Dead();
            animations.Dead();
        }        
    }

    void ChangeState(int newState)
    {
        currentState = (State)newState;
        animations.NewState(newState);
        Time.timeScale = 1f;
    }

    public void CatchItem (ItemType type)
    {
        /*Con metodo IF:
        if (type == ItemType.MagicMushroom)
        {
            //MagicMushroom
        }
        else if (type == ItemType.FirePower)
        {
            //FirePower
        }
        else if (type == ItemType.Coin)
        {
            //Coin
        }
        else if (type == ItemType.Life)
        {
            //Life
        }
        else if (type == ItemType.Star)
        {
            //Star
        }
        */

        //Con Switch (+ optimizado):
        switch (type) 
        {
            case ItemType.MagicMushroom:
                //MagicMushroom
                if (currentState == State.Default)
                {
                    animations.PowerUp();
                    Time.timeScale = 0f;
                }
                break;
            case ItemType.FirePower:
                //FirePower
                if (currentState != State.Fire)
                {
                    animations.PowerUp();
                    Time.timeScale = 0f;
                }
                break;
            case ItemType.Coin:
                //Coin
                break;
            case ItemType.Life:
                //Life
                break;
            case ItemType.Star:
                //Star
                break;
            //default:
            //    //caso default
            //    break;
        }
    }
}
