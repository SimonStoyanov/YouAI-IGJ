using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    public List<Video> trending_videos;

    public void AddToTrending(Video video)
    {
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

        for (int i = 0; i < list_size; ++i)
        {
            for (int j = 0; j < 3;)
            {
                if (id_pool[i] == video.sprite_id[j])
                {

                }
            }
        }
        
    }

	public void SendReport(Video video)
    {
        
    }


    void CreateVideo(bool is_trending, float global_time)
    {
        Video video = new Video(is_trending, global_time);
    }
}
