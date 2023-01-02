using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
