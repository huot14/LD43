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
    public Image backdrop;
    public Text message;
	public Text storyText;
	public Text traps;

	void Start ()
    {
        next.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);
		storyText.gameObject.SetActive (false);
        backdrop.gameObject.SetActive(false);

        replay.onClick.AddListener(replayLevel);
        next.onClick.AddListener(nextLevel);
	}

	void Update() {
		if (this.traps != null && Level.instance != null) {
			this.traps.text = Level.instance.nearbyTraps + " Trap(s) Nearby";
		}
	}
	
    public void levelOver(bool win, int starRating)
    {
		if (Level.instance != null && Level.instance.endText != null) {
			var text = ((EndLevelText)Level.instance.endText).message ();
			this.storyText.text = text;
			this.storyText.gameObject.SetActive (true);
            backdrop.gameObject.SetActive(true);
        }

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
