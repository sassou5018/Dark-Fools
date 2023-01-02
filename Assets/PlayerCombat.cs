using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    [Header("Attack Variables")]
    public float attackRange = 0.8f;
    public LayerMask whatIsEnemy;
    public int attackDamage = 20;
    public Transform attackPoint;

    [Header("Player Stats")]
    public int maxHealth = 100;
    public float attackSpeed = 2.0f;
    float nextAttackTime = 0f;
    private int currentHealth;
    private int enemiesKilled = 0;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextAttackTime){
            attack();
            nextAttackTime = Time.time + 1f/attackSpeed;
        }
        
    }

    void attack(){
        anim.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemy);
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected(){
        if(attackPoint == null)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        //anim.SetTrigger("hurt");
        if(currentHealth <= 0){
            Die();
        }
    }
    private void Die(){
        //anim.SetBool("isDead", true);
        //anim.SetTrigger("die");
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
        Debug.Log("Player Died");
    }

    public int getEnemiesKilled(){
        return enemiesKilled;
    }
    public void addEnemiesKilled(){
        enemiesKilled++;
    }
    public int getCurrentHealth(){
        return currentHealth;
    }
}
