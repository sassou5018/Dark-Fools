using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public QuestClass quest;
    private EnemyCombat attackScript;
    [Header("Dialogue Variables")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    [Header("Player Game Object")]
    public GameObject player;
    [Header("Interaction Distance Variables")]
    public float interactionRange = 1.3f;
    public Transform interactionPoint;
    public LayerMask whatIsInteractable;
    [Header("Set Quest Vars")]
    public int questId;
    public string questName;
    public string questDescription;
    public string questDialogue;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        quest = new QuestClass(questId, questName, questDescription, questDialogue);
        attackScript = GetComponent<EnemyCombat>();
        anim = GetComponent<Animator>();
        anim.SetBool("sit", true);
        dialogueText.text = quest.questDialogue;
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D interactable = Physics2D.OverlapCircle(interactionPoint.position, interactionRange, whatIsInteractable);
        if(interactable != null && quest.isStarted == false){
            startInteraction();
        } else {
            endInteraction();
        }
        
        
    }

    public void startCombat(){
        anim.SetBool("sit", false);
        attackScript.enabled = true;
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    public void startInteraction(){
        anim.SetBool("sit", false);
        dialogueBox.SetActive(true);
    }
    public void endInteraction(){
        anim.SetBool("sit", true);
        dialogueBox.SetActive(false);
    }



    private void OnDrawGizmosSelected(){
        if(interactionPoint == null)
            return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }
}
