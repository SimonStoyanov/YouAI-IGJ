using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour {

    public Text total_videos;
    public Text good_videos;
    public Text bad_videos;
    public Text days_videos;

    public Text entertainment;
    public Text critique;
    public Text comedy;

    private void Start()
    {
        int _days = PlayerPrefs.GetInt("Days");
        int _bad_videos = PlayerPrefs.GetInt("BadVideos");
        int _good_videos = PlayerPrefs.GetInt("GoodVideos");
        int _all_videos = _bad_videos + _good_videos;

        int _entertainment = PlayerPrefs.GetInt("Entertainment");
        int _critique = PlayerPrefs.GetInt("Critique");
        int _comedy  = PlayerPrefs.GetInt("Comedy");

        total_videos.text = _all_videos.ToString();
        good_videos.text = _good_videos.ToString();
        bad_videos.text = _bad_videos.ToString();
        days_videos.text = _days.ToString();
        entertainment.text = _entertainment.ToString();
        critique.text = _critique.ToString();
        comedy.text = _comedy.ToString();
    }

}
