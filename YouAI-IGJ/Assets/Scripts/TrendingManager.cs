using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrendingManager : MonoBehaviour {

    public Video[] trending_videos = new Video[6];
    int[] tick = new int[6];
    int[] ticks_left = new int[6];

    AIManager ai_manager;
    int seconds_a_day = 180;

    private void Awake()
    {
        // Initializing Tick
        for (int i = 0; i < 6; ++i)
            tick[i] = 0;

        ai_manager = GameObject.FindGameObjectWithTag("AITracker").GetComponent<AIManager>(); 
    }

    // Use this for initialization
    void Start () {
        CreateTrendingVideo();
        StartCoroutine(WaitAndCreate(seconds_a_day * 2));
        StartCoroutine(WaitAndCreate(seconds_a_day * 4));
        StartCoroutine(WaitAndCreate(seconds_a_day * 6));
        StartCoroutine(WaitAndCreate(seconds_a_day * 7));
        StartCoroutine(WaitAndCreate(seconds_a_day * 8));
    }
	
	// Update is called once per frame
	void Update () {

    }

    void CreateTrendingVideo()
    {
        for (int i = 0; i < 6; ++i)
        {
            if (trending_videos[i].video[0] == null)
            {
                trending_videos[i].GenerateData();
                ticks_left[i] = (int) trending_videos[i].global_time / 10;
                StartCoroutine(Tick(i));
                return;
            }
        }
        
    }

    IEnumerator Tick(int i)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(10);
            tick[i]++;
            if (tick[i] == ticks_left[i])
            {
                tick[i] = 0;
                trending_videos[i].GenerateData();
            }
        }
    }

    IEnumerator WaitAndCreate(int i)
    {
        yield return new WaitForSecondsRealtime(i);
        CreateTrendingVideo();
    }
}
