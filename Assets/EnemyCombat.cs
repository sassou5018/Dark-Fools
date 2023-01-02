using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyCombat : MonoBehaviour
{
    private Animator anim;
    [Header("Enemy Stats")]
    public int maxHealth = 50;
    private int currentHealth;
    [Header("Attack Variables")]
    public float attackRange = 0.8f;
    public float AggroRange = 5f;
    public LayerMask whatIsPlayer;
    public int attackDamage = 20;
    public Transform attackPoint;
    public float attackSpeed = 1.0f;
    float nextAttackTime = 0f;
    public Rigidbody2D rb;
    private SpriteRenderer renderer;
    [Header("Player")]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, whatIsPlayer);
        Collider2D hitPlayerAggro = Physics2D.OverlapCircle(attackPoint.position, AggroRange, whatIsPlayer);
        if(hitPlayerAggro != null){
            if(Mathf.Abs(hitPlayerAggro.transform.position.x - this.transform.position.x) > attackRange){
                anim.SetBool("isRunning", true);
                rb.velocity= new Vector2((hitPlayerAggro.transform.position.x - this.transform.position.x) * 1f, rb.velocity.y);
                if(hitPlayerAggro.transform.position.x - this.transform.position.x < 0){
                    renderer.flipX = true;
                } else {
                    renderer.flipX = false;
                }
            }else{
                anim.SetBool("isRunning", false);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if(hitPlayer != null && Time.time >= nextAttackTime){
                anim.SetTrigger("attack");
                hitPlayer.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
                Debug.Log("Attacked Player");
                nextAttackTime = Time.time + 1f/attackSpeed;
            }
        }
        
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        anim.SetTrigger("hit");
        if(currentHealth <= 0){
            Die();
        }
    }
    private void Die(){
        anim.ResetTrigger("hit");
        anim.ResetTrigger("attack");
        anim.SetBool("isRunning", false);
        anim.SetTrigger("die");
        player.GetComponent<PlayerCombat>().addEnemiesKilled();
        
        GetComponent<Collider2D>().enabled = false;
        Debug.Log(this.name+" Died");
        this.enabled = false;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
{
    //Wait for 2 seconds
    yield return new WaitForSeconds(0.8f);
    //Destroy the GameObject
    Destroy(gameObject);
}

    private void OnDrawGizmosSelected(){
        if(attackPoint == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, AggroRange);
    }

}
