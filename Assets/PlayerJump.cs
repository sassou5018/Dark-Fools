using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [Header("Jump Variables")]
    public float jumpForce = 5.0f;
    public bool grounded;
    private Rigidbody2D rb;
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [Header("Animator")]
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(grounded && rb.velocity.y == 0.0){
            anim.ResetTrigger("jump");
            anim.SetBool("falling", false);
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if(Input.GetButtonDown("Jump") && grounded){
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
        if(rb.velocity.y < 0){
            anim.SetBool("falling", true);
        }
    }

    void FixedUpdate(){
        handleLayers();
    }
    private void OnDrawGizmosSelected(){
        if(groundCheck == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
    private void handleLayers(){
        if(!grounded || rb.velocity.y < -0.01 || rb.velocity.y > 0.01){
            anim.SetLayerWeight(1, 1);
        }else{
            anim.SetLayerWeight(1, 0);
        }
    }

}
