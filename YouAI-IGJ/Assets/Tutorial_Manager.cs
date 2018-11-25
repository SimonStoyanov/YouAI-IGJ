using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Manager : MonoBehaviour {

    public Canvas tutorial_1;
    public Canvas tutorial_2;
    public Canvas tutorial_3;
    public Canvas tutorial_4;
    public Canvas tutorial_5;
    public Canvas tutorial_6;
    public Canvas tutorial_7;
    public Canvas tutorial_8;
    public Canvas tutorial_9;
    public Canvas tutorial_10;
    public Canvas tutorial_11;
    public Canvas tutorial_12;
    public Canvas tutorial_13;
    public Canvas tutorial_14;
    public Canvas tutorial_15;
    public Canvas tutorial_16;
    int i = 0;
    List<Canvas> canvas_list;
    // Use this for initialization
    void Start ()
    {
        canvas_list = new System.Collections.Generic.List<Canvas>();

        canvas_list.Add(tutorial_1);
        canvas_list.Add(tutorial_2);
        canvas_list.Add(tutorial_3);
        canvas_list.Add(tutorial_4);
        canvas_list.Add(tutorial_5);
        canvas_list.Add(tutorial_6);
        canvas_list.Add(tutorial_7);
        canvas_list.Add(tutorial_8);
        canvas_list.Add(tutorial_9);
        canvas_list.Add(tutorial_10);
        canvas_list.Add(tutorial_11);
        canvas_list.Add(tutorial_12);
        canvas_list.Add(tutorial_13);
        canvas_list.Add(tutorial_14);
        canvas_list.Add(tutorial_15);
        canvas_list.Add(tutorial_16);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void next_tutorial()
    {
        i++;
        canvas_list[i - 1].enabled = false;
        canvas_list[i].enabled = true;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
