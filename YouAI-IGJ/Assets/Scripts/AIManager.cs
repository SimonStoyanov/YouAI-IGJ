using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIManager : MonoBehaviour {

    public List<Video> trending_videos;
    public Slider popularity_slider;
    public Slider quality_slider;
    public Slider copyright_slider;
    public Canvas VideoUI;

    public Text PopValue;
    public Text QValue;
    public Text IValue;
    public Text IMax;

    public int tick = 10;
    public bool playing = true;

    public int ticks_done = 0;
    public int ticks_a_day = 30;
    public int days = 0;

    bool recovering_infringement = false;
    int infringement_ticks = 0;
    int ticks_to_recover = 90;
    int days_left = 3;

    int bad_videos = 0;
    int good_videos = 0;
    int entertainment_num = 0;
    int critique_num = 0;
    int comedy_num = 0;

    TrendingManager trending_manager = null;

    public AudioSource copyrighted_sent;

    public Text Numero_Dias_Texto;

    private void Awake()
    {
        trending_manager = GetComponent<TrendingManager>();
    }

    private void Start()
    {
        StartCoroutine(Tick());
    }

    private void Update()
    {
        if (popularity_slider.value <= 0 || quality_slider.value <= 0 || copyright_slider.value >= 5)
        {
            PlayerPrefs.SetInt("Days", days);
            PlayerPrefs.SetInt("BadVideos", bad_videos);
            PlayerPrefs.SetInt("GoodVideos", good_videos);
            PlayerPrefs.SetInt("Entertainment", entertainment_num);
            PlayerPrefs.SetInt("Critique", critique_num);
            PlayerPrefs.SetInt("Comedy", comedy_num);

            GetComponent<SceneMenuManage>().LoadScene("Score Menu");
        }
    }

    public void AddToTrending(Video video)
    {
        trending_videos.Add(video);
    }

	public void SendReport(Video video, bool accept)
    {
        video.timer = 0f;
        if (accept)
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

            QValue.text = quality_slider.value.ToString();

            // Copyright
            if (video.copyrighted)
            {
                copyright_slider.value += 1;
                copyrighted_sent.Play();
                infringement_ticks = 0;
                recovering_infringement = true;
                days_left = 3;

                bad_videos++;
            }
            else
                good_videos++;

            switch (video.category)
            {
                case Video.Category.Entretenimiento:
                    entertainment_num++;
                    break;
                case Video.Category.Critica:
                    critique_num++;
                    break;
                case Video.Category.Comedia:
                    comedy_num++;
                    break;
            }
        }
        else
        {
            // Copyright
            if (!video.copyrighted)
            {
                popularity_slider.value -= 2;
            }
        }
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
            TickSpawnVideo();
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

        PopValue.text = popularity_slider.value.ToString();
    }

    void TickCopyright()
    {
        ticks_done++;

        if (ticks_done % ticks_a_day == 0)
        {
            days++;
            Numero_Dias_Texto.text = days.ToString();
        }

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
        IValue.text = copyright_slider.value.ToString();

    }

    void TickSpawnVideo()
    {
        float popularity_value = popularity_slider.value;
        int random_spawn = Random.Range(0, 10);

        // There should be space left to spawn

        if (popularity_value >= 0 && popularity_value <= 10)
        {
            if (random_spawn <= 6)//70%
            {
                //Spawn
                GameObject.FindGameObjectWithTag("AITracker").GetComponent<MonkeyManager>().SpawnVideo();
            }
        }
        else if (popularity_value >= 11 && popularity_value <= 70)
        {
            if (random_spawn <= 4)//50%
            {
                //Spawn
                GameObject.FindGameObjectWithTag("AITracker").GetComponent<MonkeyManager>().SpawnVideo();
            }
        }
        else if (popularity_value >= 71 && popularity_value <= 100)
        {
            if (random_spawn <= 5)//60%
            {
                //Spawn
                GameObject.FindGameObjectWithTag("AITracker").GetComponent<MonkeyManager>().SpawnVideo();
            }
        }
    }

    public void RenderCanvas(Video video)
    {
        VideoUIManager video_manager = GameObject.FindGameObjectWithTag("VideoUI").GetComponent<VideoUIManager>();
        if (video.sprite_in_use != null)
        {
            VideoUI.enabled = true;
            video.SetEntered(true);
            video_manager.rendered_video = video;
        }
    }

    public void BlockCanvas(Video video)
    {
        VideoUI.enabled = false;
        video.SetEntered(false);
    }
}
