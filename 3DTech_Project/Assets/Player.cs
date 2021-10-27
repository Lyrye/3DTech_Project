using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isDead = false;            //Has the player collided with a wall?
    public int lifeCount;

    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody2D>();
        lifeCount = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
      //  Debug.Log("test");

        // If the bird collides with something set it to dead...
        lifeCount = lifeCount - 1 ;
        if(lifeCount == 0){
          // Zero out the bird's velocity
          rb.velocity = Vector2.zero;
          isDead = true;
          //...tell the Animator about it...
          anim.enabled = false;
          //...and tell the game control about it.
          GameControl.instance.ChickenDied ();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test" + lifeCount);

        // If the bird collides with something set it to dead...
        lifeCount = lifeCount - 1 ;
        PlayerStats.Instance.TakeDamage(1.0f); 
        if(lifeCount == 0){
          // Zero out the bird's velocity
          rb.velocity = Vector2.zero;
          isDead = true;
          //...tell the Animator about it...
          anim.enabled = false;
          //...and tell the game control about it.
          GameControl.instance.ChickenDied ();
        }
    }
}
