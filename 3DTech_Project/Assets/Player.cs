using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float upForce;                    //Upward force of the "flap".
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


}
