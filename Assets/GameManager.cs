using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public GameObject player;
    public int enemiesCount = 2;
    private Collider2D playerCollider;
    private Collider2D myCollider;
    private int enemiesKilled = 0;
    private PlayerCombat playerCombat;
    public Transform endGamePoint;
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI victoryText;
    public float endGamePointRange = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = player.GetComponent<Collider2D>();
        playerCombat = player.GetComponent<PlayerCombat>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilled = playerCombat.getEnemiesKilled();
        Scene currentScene = SceneManager.GetActiveScene();
        Collider2D playerCollide = Physics2D.OverlapCircle(endGamePoint.position, endGamePointRange, whatIsPlayer);
        if(playerCollide != null && enemiesKilled < enemiesCount){
            victoryText.text = "You need to kill " +(enemiesCount - enemiesKilled) + " more enemies";
        } else {
            victoryText.text = "";
        }
        if(enemiesKilled >= enemiesCount && playerCollide != null){
            myCollider.enabled = false;
            playerCollider.enabled = false;
            playerCombat.isDead = true;
            SceneManager.LoadScene("Win");
        }
        if(playerCombat.getCurrentHealth()<=0){
            myCollider.enabled = false;
            playerCollider.enabled = false;
            playerCombat.isDead = true;
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        
    }

    private void OnDrawGizmosSelected(){
        if(endGamePoint == null)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(endGamePoint.position, endGamePointRange);
    }
}
