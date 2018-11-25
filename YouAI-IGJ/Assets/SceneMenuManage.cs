using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMenuManage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
