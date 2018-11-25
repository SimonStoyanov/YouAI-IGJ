using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoUIManager : MonoBehaviour {
    [SerializeField]
    Image video_to_render;

    [SerializeField]
    private GameObject QualityPanel;
    [SerializeField]
    private GameObject quality_1_obj;
    private Text quality_1_txt;
    [SerializeField]
    private GameObject quality_2_obj;
    private Text quality_2_txt;
    [SerializeField]
    private GameObject quality_3_obj;
    private Text quality_3_txt;
    [SerializeField]
    private GameObject quality_4_obj;
    private Text quality_4_txt;
    bool do_once_quality = true;

    [SerializeField]
    private GameObject time_slider;
    Slider time_left;
    Text time_left_text;

    [SerializeField]
    private Text author;

    [SerializeField]
    private GameObject category_text;
    Text category_txt;

    public Video rendered_video;

    // Use this for initialization
    void Start () {
        time_left = time_slider.GetComponent<Slider>();
        time_left_text = GameObject.FindGameObjectWithTag("SecondsText").GetComponent<Text>();
        category_txt = category_text.GetComponent<Text>();

        quality_1_txt = quality_1_obj.GetComponent<Text>();
        quality_2_txt = quality_2_obj.GetComponent<Text>();
        quality_3_txt = quality_3_obj.GetComponent<Text>();
        quality_4_txt = quality_4_obj.GetComponent<Text>();
    }

// Update is called once per frame
void Update () {
        if (rendered_video != null)
        {
            category_txt.text = rendered_video.category.ToString();
            time_left.maxValue = rendered_video.global_time;
            time_left.value = rendered_video.timer;
            time_left_text.text = rendered_video.time_left.ToString();
            author.text = rendered_video.author;
            video_to_render.sprite = rendered_video.sprite_in_use;

            SetQualities(rendered_video.rand_quality);
        }

	}

    public void ToogglePanel()
    {
        if (QualityPanel.activeSelf)
        {
            QualityPanel.SetActive(false);
        }
        else
        {
            QualityPanel.SetActive(true);
            do_once_quality = true;
        }
    }

    public void SetQualities(int Qualities)//0 es Low, 1 es High, 2 es Neutral
    {
        if(Qualities == 0)
        {
            quality_1_txt.color = Color.white;
            quality_2_txt.color = Color.white;
            quality_3_txt.color = Color.grey;
            quality_4_txt.color = Color.grey;
        }
        else if(Qualities == 1)
        {
            quality_1_txt.color = Color.grey;
            quality_2_txt.color = Color.grey;
            quality_3_txt.color = Color.white;
            quality_4_txt.color = Color.white;
        }
        else if(Qualities == 2)
        {
            quality_1_txt.color = Color.white;
            quality_2_txt.color = Color.white;
            quality_3_txt.color = Color.white;
            quality_4_txt.color = Color.white;
        }

        do_once_quality = false;
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
