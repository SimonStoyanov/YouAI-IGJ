using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour {

    GameObject[] monkeys;
    Video[] monkey_video;
    Canvas[] monkey_canvas;

    private void Awake()
    {
        monkeys = GameObject.FindGameObjectsWithTag("Monkeys");
        monkey_video = new Video[monkeys.Length];
        monkey_canvas = new Canvas[monkeys.Length];

        for (int i = 0; i < monkeys.Length; ++i)
        {
            monkey_video[i]  = monkeys[i].GetComponent<Video>() as Video;
            monkey_canvas[i] = monkeys[i].GetComponentInChildren<Canvas>() as Canvas;
        }
    }

    public void SpawnVideo()
    {
        List<Canvas> canvas_to_render = new List<Canvas>();

        for (int i = 0; i < monkeys.Length; ++i)
        {
            if (!monkey_canvas[i].enabled)
                canvas_to_render.Add(monkey_canvas[i]);
        }

        if (canvas_to_render.Count == 0)
            return;

        int rand_canvas = Random.Range(0, canvas_to_render.Count);
        monkey_canvas[rand_canvas].enabled = true;     
    }
}
