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

    [SerializeField]
    private GameObject time_slider;
    Slider time_left;
    Text time_left_text;

    [SerializeField]
    private Text author;

    [SerializeField]
    private GameObject category_text;
    Text category_txt;

    [SerializeField]
    private GameObject AcceptDeny;

    public Video rendered_video;
    public AudioSource video_off_sound;
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

            if (rendered_video.is_trending)
                AcceptDeny.SetActive(false);
            else
                AcceptDeny.SetActive(true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(GetComponent<Canvas>().enabled == true)
            {
                video_off_sound.Play();
            }
            GetComponent<Canvas>().enabled = false;
            rendered_video.SetEntered(false);
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
    }

    public void SetCategory(int Category)//0 entertainment, 1 Comedy, 2 Critic
    {
        if (Category == 0)
        {
            category_text.GetComponent<Text>().text = "#Entretenimiento";
        }
        else if (Category == 1)
        {
            category_text.GetComponent<Text>().text = "#Comedia";
        }
        else if (Category == 2)
        {
            category_text.GetComponent<Text>().text = "#Critica";
        }
    }

    public void DenyVideo()
    {
        AIManager ai_manager = GameObject.FindGameObjectWithTag("AITracker").GetComponent<AIManager>();

        ai_manager.SendReport(rendered_video, false);
        rendered_video.EraseVideo();
    }

    public void AcceptVideo()
    {
        AIManager ai_manager = GameObject.FindGameObjectWithTag("AITracker").GetComponent<AIManager>();

        ai_manager.SendReport(rendered_video, true);
        rendered_video.EraseVideo();
    }
}
