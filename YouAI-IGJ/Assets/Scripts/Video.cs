using System.Collections;
using System.Collections.Generic;
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

    // 144p, 360p, 720p, 1080p
    public bool[] quality = new bool[4];
    public Category category = Category.Entertainment;
    public string author = "name";

    int num_sprites = 11;
    public int[] sprite_id = new int[3];
    public Sprite[] video = new Sprite[3];

    private void Awake()
    {
        for (int i = 0; i < 4; ++i)
            quality[i] = false;

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

        // Sprite Setter by ID
        for (int i = 0; i < 3; ++i)
        {
            string sprite_path = "Image_" + sprite_id[i];
            video[i] = Resources.Load<Sprite>(sprite_path);
        }
    }

    bool debugging = false;
    void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
            debugging = true;
        if (debugging)
            Debugging();
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

}
