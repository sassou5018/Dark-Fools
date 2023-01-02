using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractWithNPC : MonoBehaviour
{
    [Header("Interaction Distance Variables")]
    public float interactionRange = 1.3f;
    public Transform interactionPoint;
    public LayerMask whatIsInteractable;
    [Header("Quests")]
    public int questNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D interactable = Physics2D.OverlapCircle(interactionPoint.position, interactionRange, whatIsInteractable);
        if(interactable != null){
            if(Input.GetKeyDown(KeyCode.E)){
                acceptInteraction(interactable);
            } else if (Input.GetKeyDown(KeyCode.A)){
                declineInteraction(interactable);
            }
        }
        
    }
    private void OnDrawGizmosSelected(){
        if(interactionPoint == null)
            return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }
    void acceptInteraction(Collider2D interactable){
        if(interactable != null){
            interactable.GetComponent<NPCInteraction>().quest.StartQuest();
            interactable.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            interactable.GetComponent<Collider2D>().enabled = false;
            interactable.GetComponent<CircleCollider2D>().enabled = false;
            interactable.GetComponent<NPCInteraction>().endInteraction();
        }
    }

    void declineInteraction(Collider2D interactable){
        if(interactable != null){
            interactable.GetComponent<NPCInteraction>().startCombat();
        }
    }

    
}
