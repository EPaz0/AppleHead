using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{    
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        // cursor locking code - not needed for now
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.activeInHierarchy){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else{
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void gameOver(){
        Time.timeScale = 0f; // Freeze the game
        gameOverUI.SetActive(true);
    }

    public void restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
    }

    public void mainMenu(){
        SceneManager.LoadScene("MainMenu");
        Debug.Log("MainMenu");
    }

    public void quit(){
        Application.Quit();
        Debug.Log("Quit");
    }
}
