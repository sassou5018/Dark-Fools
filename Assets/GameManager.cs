using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public GameObject player;
    public int enemiesCount = 2;
    private Collider2D playerCollider;
    private Collider2D myCollider;
    private int enemiesKilled = 0;
    private PlayerCombat playerCombat;
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
        if(enemiesKilled >= enemiesCount){
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
}
