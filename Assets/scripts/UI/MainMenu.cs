using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button start;
    public Dropdown dropdown;

	// Use this for initialization
	void Start ()
    {
        start.onClick.AddListener(startAction);
        
	}
	
    void startAction()
    {
        if (dropdown.value == 0)
        {
            SceneManager.LoadScene("level1");
        }
        else if(dropdown.value == 1)
        {
            SceneManager.LoadScene("level2");
        }
        else if (dropdown.value == 2)
        {
            SceneManager.LoadScene("level3");
        }
    }



}
