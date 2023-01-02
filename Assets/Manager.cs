using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    private PlayerCombat playerCombat;
    [Header("UI")]
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = player.GetComponent<PlayerCombat>();
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + playerCombat.getCurrentHealth();
        
    }
}
