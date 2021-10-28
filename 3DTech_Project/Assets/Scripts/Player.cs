using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private bool isDead = false;            //Has the player collided with a wall?

    private Animator anim;
    private Rigidbody2D rb;

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 1.0f;
    public bool startBlinking = false;

    public GameObject CanvasGameOver;
    public Text hitText;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if(startBlinking == true)
      {
          StartBlinkingEffect();
      }
    }

/*
    void OnCollisionEnter2D(Collision2D other)
    {
      //  Debug.Log("test");

        // If the bird collides with something set it to dead...
        if(PlayerStats.Instance.Health <= 0){
          // Zero out the bird's velocity
          rb.velocity = Vector2.zero;
          isDead = true;
          //...tell the Animator about it...
          anim.enabled = false;
          //...and tell the game control about it.
          GameControl.instance.ChickenDied ();
        }
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        PDollarRecognizer.Instance.StartRecognize();
        if (!PDollarRecognizer.Instance.isRecognized())
        {
            PlayerStats.Instance.TakeDamage(1.0f);
            startBlinking = true;
            hitText.text = "RATÉ !";
        }else{
          hitText.text = "BIEN JOUÉ !";
        }

        hitText.enabled = true;
        Invoke("EnabledText", 1.2f);

        if(PlayerStats.Instance.Health <= 0){
          // Zero out the bird's velocity
          rb.velocity = Vector2.zero;
          isDead = true;
          //...tell the Animator about it...
          anim.enabled = false;
          //...and tell the game control about it.
          GameControl.instance.ChickenDied ();
          //Destroy(PlayerStats.Instance);
          CanvasGameOver.SetActive(true);
        }
    }

    private void StartBlinkingEffect()
    {
    //  hitText.text = "HITTEN";
      spriteBlinkingTotalTimer += Time.deltaTime;
      if(spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
      {
            startBlinking = false;
           spriteBlinkingTotalTimer = 0.0f;
           this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
           return;
        }

      spriteBlinkingTimer += Time.deltaTime;
      if(spriteBlinkingTimer >= spriteBlinkingMiniDuration)
      {
         spriteBlinkingTimer = 0.0f;
         if (this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
             this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;  //make changes
         } else {
             this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   //make changes
         }
      }
    }

    public void EnabledText()
    {
      hitText.enabled =false;
    }
}
