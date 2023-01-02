using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestClass : ScriptableObject
{
    public int questID;
    public string questName;
    public string questDescription;
    public string questDialogue;
    public bool isDone;
    public bool isStarted;

    public QuestClass(int id, string name, string description, string dialogue){
        questID = id;
        questName = name;
        questDescription = description;
        questDialogue = dialogue;
        isDone = false;
        isStarted = false;
    }

    public void StartQuest(){
        isStarted = true;
    }
    public void CompleteQuest(){
        isDone = true;
    }
}
