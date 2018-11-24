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
    public bool[] quality = new bool[4];
    public Category category = Category.Entertainment;
    public string author = "name";
    public bool is_trending = false;

    int num_sprites = 27;
    public int[] sprite_id = new int[3];
    public Sprite[] video = new Sprite[3];

    bool entered_video = false;
    int video_id_shown = 0;

    public Video(bool is_trending, float global_time)
    {
        this.is_trending = is_trending;
        this.global_time = global_time;
    }

    private void Awake()
    {
        // Initialize Quality
        for (int i = 0; i < 4; ++i)
        {
            if (is_trending)
                quality[i] = true;
            else
                quality[i] = false;
        }

        // Random Sprite ID Generator
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

    void Start () {
        // Random Category Generator
        category = (Category) Random.Range(0, 3);

        // Random Quality Generator
        if (!is_trending)
        {
            int rand_quality = Random.Range(0, 3);
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

        // Sprite Setter by ID
        for (int i = 0; i < 3; ++i)
        {
            string sprite_path = "Image_" + sprite_id[i];
            video[i] = Resources.Load<Sprite>(sprite_path);
        }

        GetComponent<Image>().sprite = video[0];

        StartCoroutine(TimeLeft());
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
        // if (clicked){
        if (Input.GetKeyDown(KeyCode.F2))
        {
            entered_video = true;
            StartCoroutine(VideoSequence());
        }
        // }
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
        while (entered_video)
        {
            yield return new WaitForSeconds(slide_time);
            if (video_id_shown == 2) video_id_shown = 0;
            else video_id_shown++;

            GetComponent<Image>().sprite = video[video_id_shown];
        }
    }

    IEnumerator TimeLeft()
    {
        yield return new WaitForSeconds(global_time);

        Slider quality_slider   = GameObject.FindGameObjectWithTag("QualityAI").GetComponent<Slider>();

        if (quality[0] && quality[1])
            quality_slider.value -= 0.1f;
        else if (quality[2] && quality[3])
            quality_slider.value += 0.1f;

        entered_video = false;
        GameObject.FindGameObjectWithTag("AITracker").GetComponent<AIManager>().SendReport(this);
    }
}
