﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIManager : MonoBehaviour {

    public List<Video> trending_videos;
    public Slider popularity_slider;
    public Slider quality_slider;
    public Slider copyright_slider;

    public int tick = 10;
    bool playing = true;

    int ticks_done = 0;
    int ticks_a_day = 30;
    int days = 0;

    bool recovering_infringement = false;
    int infringement_ticks = 0;
    int ticks_to_recover = 90;
    int days_left = 3;

    private void Start()
    {
        //CreateVideo(false, 30);

    }

    public void AddToTrending(Video video)
    {
        trending_videos.Add(video);
    }

	public void SendReport(Video video)
    {
        float pop_value = popularity_slider.value;

        // Popularity
        if (pop_value >= 0 && pop_value <= 10)
            popularity_slider.value += 4;
        else if (pop_value >= 11 && pop_value <= 30)
            popularity_slider.value += 3;
        else if (pop_value >= 31 && pop_value <= 70)
            popularity_slider.value += 2;
        else if (pop_value >= 71 && pop_value <= 100)
            popularity_slider.value += 3;

        // Quality
        if (video.quality[0] && video.quality[1])
            quality_slider.value -= 3;
        else if (video.quality[2] && video.quality[3])
            quality_slider.value += 3;

        // Copyright
        if (video.copyrighted)
        {
            copyright_slider.value += 1;
            infringement_ticks = 0;
            recovering_infringement = true;
            days_left = 3;
        }
    }

    void CreateVideo(bool is_trending, float global_time)
    {
        Video video = gameObject.AddComponent(typeof(Video)) as Video;
        video.is_trending = is_trending;
        video.global_time = global_time;
        video.author = "JAPO";
    }

    public int ExcludingRandom(int min, int max)
    {
        int num = 0;
        int list_size = trending_videos.Count * 3;
        int[] id_pool = new int[list_size];
        int id = 0;

        foreach (Video v in trending_videos)
        {
            for (int i = 0; i < 3; ++i)
            {
                id_pool[id++] = v.sprite_id[i];
            }
        }

        while (true)
        {
            bool is_repeated = false;
            num = Random.Range(min, max);
            
            foreach (int i in id_pool)
            {
                if (num == i)
                    is_repeated = true;
            }

            if (!is_repeated)
                break;
        }

        return num;
    }

    IEnumerator Tick()
    {
        while (playing)
        {
            yield return new WaitForSecondsRealtime(tick);

            TickPopularity();
            TickCopyright();
        }
    }

    void TickPopularity()
    {
        float quality_value = quality_slider.value;

        if (quality_value >= 0 && quality_value <= 20)
            popularity_slider.value -= 3;
        else if (quality_value >= 21 && quality_value <= 40)
            popularity_slider.value -= 2;
        else if (quality_value >= 61 && quality_value <= 71)
            popularity_slider.value -= 1;
        else if (quality_value >= 71 && quality_value <= 100)
            popularity_slider.value -= 2;
    }

    void TickCopyright()
    {
        ticks_done++;

        if (ticks_done % ticks_a_day == 0)
            days++;

        if (recovering_infringement)
        {
            infringement_ticks++;

            if (infringement_ticks % ticks_a_day == 0)
            {
                days_left -= 1;
            }

            if (infringement_ticks % ticks_to_recover == 0)
            {
                copyright_slider.value -= 1;
                if (copyright_slider.value == 0)
                    recovering_infringement = false;
            }
        }

    }
}