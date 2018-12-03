using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    public Button replay;
    public Button next;

    public Image star1;
    public Image star2;
    public Image star3;
    public Text message;

	void Start ()
    {
        next.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);

        replay.onClick.AddListener(replayLevel);
        next.onClick.AddListener(nextLevel);
	}
	
    public void levelOver(bool win, int starRating)
    {
        if(starRating >= 1)
        {
            star1.gameObject.SetActive(true);
        }
        if (starRating >= 2)
        {
            star2.gameObject.SetActive(true);
        }
        if(starRating >= 3)
        {
            star3.gameObject.SetActive(true);
        }

        //Failed to complete the level, must retry
        if(!win)
        {
            message.gameObject.SetActive(true);
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
    }



}
