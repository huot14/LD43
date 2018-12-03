using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    public Button replay;
    public Button next;

    public Text message;

	void Start ()
    {
        next.gameObject.SetActive(false);
        message.gameObject.SetActive(false);

        replay.onClick.AddListener(replayLevel);
        next.onClick.AddListener(nextLevel);
	}
	
    public void levelOver(bool win, int starRating)
    {
        //TODO: make this graphical
        message.text = starRating + " Stars";

        //Failed to complete the level, must retry
        if(!win)
        {
            next.interactable = false;
        }
    }

    void replayLevel()
    {     
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void nextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("main");
        }
    }


    public void activateEndUI()
    {
        next.gameObject.SetActive(true);
        message.gameObject.SetActive(true);
    }



}
