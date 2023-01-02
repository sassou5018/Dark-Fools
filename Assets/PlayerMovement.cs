using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private new SpriteRenderer renderer;
    [Header("Movement Variables")]
    public float speed = 2.0f;
    public float rollRate = 1.0f;
    float nextRoll = 0.0f;
    [Header("Current Axis Value")]
    public float horizMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizMovement = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.LeftControl) && Time.time >= nextRoll){
            anim.SetTrigger("roll");
            if(renderer.flipX){
                rb.AddForce(new Vector2(-1500, 0));
            }else{
                rb.AddForce(new Vector2(1500, 0));
            }
            nextRoll = Time.time + 1f/rollRate;
        }
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizMovement * speed, rb.velocity.y);
        Flip(horizMovement);
        anim.SetFloat("speed", Mathf.Abs(horizMovement));
        
    }

    void Flip(float horizontal){
        if(horizontal > 0 && renderer.flipX || horizontal < 0 && !renderer.flipX){
            renderer.flipX = !renderer.flipX;
        }
    }
}
