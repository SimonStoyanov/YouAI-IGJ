using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrendingManager : MonoBehaviour {

    public Video[] trending_videos = new Video[6];

    AIManager ai_manager;
    int seconds_a_day = 180;

    private void Awake()
    {
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

                return;
            }
        }
        
    }

    IEnumerator WaitAndCreate(int i)
    {
        yield return new WaitForSecondsRealtime(i);
        CreateTrendingVideo();
    }
}
