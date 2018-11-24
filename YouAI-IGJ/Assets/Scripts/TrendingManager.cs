using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrendingManager : MonoBehaviour {

    public Video[] trending_videos = new Video[6];
    int[] tick = new int[6];
    int[] ticks_left = new int[6];

    private void Awake()
    {
        // Initializing Tick
        for (int i = 0; i < 6; ++i)
            tick[i] = 0;

    }
    // Use this for initialization
    void Start () {
        trending_videos[0].GenerateData();
        StartCoroutine(Tick(0));

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateTrendingVideo()
    {
        
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
}
