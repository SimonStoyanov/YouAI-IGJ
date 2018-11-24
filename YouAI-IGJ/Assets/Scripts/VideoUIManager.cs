using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoUIManager : MonoBehaviour {
    [SerializeField]
    private GameObject QualityPanel;
    [SerializeField]
    private GameObject quality_1_obj;
    [SerializeField]
    private GameObject quality_2_obj;
    [SerializeField]
    private GameObject quality_3_obj;
    [SerializeField]
    private GameObject quality_4_obj;

    [SerializeField]
    private GameObject time_slider;

    [SerializeField]
    private GameObject category_text;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToogglePanel()
    {
        if(QualityPanel.activeSelf)
            QualityPanel.SetActive(false);
        else
            QualityPanel.SetActive(true);
    }

    public void SetQualities(int Qualities)//0 es Low, 1 es High, 2 es Neutral
    {
        if(Qualities == 0)
        {
            quality_3_obj.SetActive(false);
            quality_4_obj.SetActive(false);
        }
        else if(Qualities == 1)
        {
            quality_3_obj.SetActive(false);
            quality_4_obj.SetActive(false);
            quality_1_obj.GetComponent<Text>().text = "720p";
            quality_2_obj.GetComponent<Text>().text = "1080p";
        }
        else if(Qualities == 2)
        {
            //Do nothing
        }
    }

    public void SetCategory(int Category)//0 entertainment, 1 Comedy, 2 Critic
    {
        if (Category == 0)
        {
            category_text.GetComponent<Text>().text = "#Entertainment";
        }
        else if (Category == 1)
        {
            category_text.GetComponent<Text>().text = "#Comedy";
        }
        else if (Category == 2)
        {
            category_text.GetComponent<Text>().text = "#Critic";
        }
    }
}
