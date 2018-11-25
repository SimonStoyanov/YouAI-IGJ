using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Video : MonoBehaviour {

    [HideInInspector]
    public enum Category
    {
        Entertainment,
        Critique,
        Comedy,

        NULL
    }

    public float global_time = 30f;
    public float slide_time = 2f;

    // 144p, 360p, 720p, 1080p
    public int rand_quality = 0;
    public bool[] quality = new bool[4];
    public Category category = Category.Entertainment;
    public string author = "name";
    public bool copyrighted = false;

    public bool is_trending = false;

    int num_sprites = 42;
    public int[] sprite_id = new int[3];
    public Sprite sprite_in_use;
    public Sprite[] video = new Sprite[3];

    bool entered_video = false;
    bool automatic_sequence = false;
    bool sequence_on = false;
    int video_id_shown = 0;

    AIManager ai_manager;

    public float timer = 0f;
    public int time_left = 0;

    private void Awake()
    {
        ai_manager = GameObject.FindGameObjectWithTag("AITracker").GetComponent<AIManager>();
    }

    public void GenerateData () {
        // Random Author Generator
        author = GenerateName();

        // Random Category Generator
        category = (Category) Random.Range(0, 3);    

        // Random Sprite ID Generator
        if (!is_trending)
        {
            for (int i = 0; i < 3; ++i)
            {
                int tmp_id = Random.Range(0, num_sprites);

                for (int j = 0; j < 3;)
                {
                    if (tmp_id != sprite_id[j])
                        j++;
                    else
                        tmp_id = Random.Range(0, num_sprites);
                }
                sprite_id[i] = tmp_id;
            }
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                int tmp_id = ai_manager.ExcludingRandom(0, num_sprites);

                for (int j = 0; j < 3;)
                {
                    if (tmp_id != sprite_id[j])
                        j++;
                    else
                        tmp_id = ai_manager.ExcludingRandom(0, num_sprites);
                }
                sprite_id[i] = tmp_id;
            }
        }

        // Initialize Quality
        for (int i = 0; i < 4; ++i)
        {
            if (is_trending)
                quality[i] = true;
            else
                quality[i] = false;
        }

        // Random Quality Generator
        if (!is_trending)
        {
            rand_quality = Random.Range(0, 3);

            switch (rand_quality)
            {
                case 0:
                    quality[0] = quality[1] = true;
                    break;
                case 1:
                    quality[2] = quality[3] = true;
                    break;
                case 2:
                    quality[0] = quality[1] = quality[2] = quality[3] = true;
                    break;
            }
        }
        else
        {
            rand_quality = 2;
            quality[0] = quality[1] = quality[2] = quality[3] = true;
        }

        // Sprite Setter by ID
        for (int i = 0; i < 3; ++i)
        {
            string sprite_path = "Image_" + sprite_id[i];
            video[i] = Resources.Load<Sprite>(sprite_path);
        }

        if (GetComponent<Image>() != null)
            GetComponent<Image>().sprite = video[0];

        sprite_in_use = video[0];

        //StartCoroutine(TimeLeft());

        if (is_trending)
        {
            ai_manager.AddToTrending(this);
            automatic_sequence = true;
        }
        else
        {
            copyrighted = IsCopyrighted();
        }

        timer = global_time;
        time_left = (int)timer;
    }

    bool debugging = false;
    void Update()
    {
        // Debug
        {
            if (Input.GetKeyDown(KeyCode.F1))
                debugging = true;
            if (debugging)
                Debugging();
        }

        // Video
        if (!sequence_on && (entered_video || automatic_sequence))
        {
            sequence_on = true;
            automatic_sequence = false;
            StartCoroutine(VideoSequence());
        }

        if (timer > 0)
            TimeLeft();
    }

    void Debugging()
    {   
        Debug.Log("Category: " + category);
        Debug.Log("Quality: { " + quality[0] + ", " + quality[1] + ", " + quality[2] + ", " + quality[3] + " }");
        Debug.Log("Sprite 0: " + video[0]);
        Debug.Log("Sprite 1: " + video[1]);
        Debug.Log("Sprite 2: " + video[2]);
        debugging = false;
    }

    IEnumerator VideoSequence()
    {       
        while (sequence_on)
        {
            yield return new WaitForSecondsRealtime(slide_time);
            if (video_id_shown == 2) video_id_shown = 0;
            else video_id_shown++;

            if (GetComponent<Image>() != null)
                GetComponent<Image>().sprite = video[video_id_shown];

            sprite_in_use = video[video_id_shown];
        }
    }

    void TimeLeft()
    {
        float dt = Time.deltaTime;

        timer -= dt;
        time_left = (int) timer;

        if (timer <= 0f && !is_trending)
        {
            AIManager ai_manager = GameObject.FindGameObjectWithTag("AITracker").GetComponent<AIManager>();
            
            ai_manager.SendReport(this, true);

            EraseVideo();
        }
    }

    string GenerateName()
    {
        string name;

        string[] base_names = { "Pixel", "Cat", "Wizard", "Mago", "Key", "Banana", "Platano", "Monkey", "Ventisca", "Fresco", "Pizza", "Timmy", "Knuckles", "CuatroDos", "Vapor", "Global", "Simio", "Primate", "Leyenda", "Papaya" };
        string[] complement_names = { "Studios", "Arts", "United", "Interactive", "Enterteinment", "Media", "Solutions", "Industries", "Productions", "Games" };

        int name_id = Random.Range(0, base_names.Length);

        int complement_id = Random.Range(0, complement_names.Length);

        name = base_names[name_id] + " " + complement_names[complement_id];

        return name;
    }

    bool IsCopyrighted()
    {
        for (int it = 0; it < ai_manager.trending_videos.Count; it++)
        {
            Video v = ai_manager.trending_videos[it];
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (v.sprite_id[i] == sprite_id[j])
                    {
                        if (v.author != author)
                            return true;
                    }
                }
            }
        }

        return false;
    }

    public void EraseVideo()
    {
        if (GetComponent<Image>() != null)
            GetComponent<Image>().sprite = null;

        entered_video = false;
        sequence_on = false;

        video[0] = null;
        video[1] = null;
        video[2] = null;

        StopCoroutine(VideoSequence());

        ai_manager.GetComponent<AIManager>().BlockCanvas();
        GetComponentInChildren<Canvas>().enabled = false;
    }

    public void SetEntered(bool name)
    {
        entered_video = name;
    }
}
