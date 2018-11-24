using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    public List<Video> trending_videos;

    public void AddToTrending(Video video)
    {
        trending_videos.Add(video);
    }

	public void SendReport(Video video)
    {
        
    }


    void CreateVideo(bool is_trending, float global_time)
    {
        //Video video = new Video(is_trending, global_time);
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
}
