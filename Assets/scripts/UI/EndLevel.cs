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
        gameObject.SetActive(false);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
