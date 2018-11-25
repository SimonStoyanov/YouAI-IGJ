using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour {

    GameObject[] monkeys;
    Video[] monkey_video;
    MeshRenderer[] monkey_renderer;

    private void Awake()
    {
        monkeys = GameObject.FindGameObjectsWithTag("Monkeys");
        monkey_video = new Video[monkeys.Length];
        monkey_renderer = new MeshRenderer[monkeys.Length];

        for (int i = 0; i < monkeys.Length; ++i)
        {
            monkey_video[i]  = monkeys[i].GetComponent<Video>() as Video;
            GameObject capsule = monkeys[i].GetComponentInChildren<Button3D>().gameObject;
            monkey_renderer[i] = capsule.GetComponent<MeshRenderer>();
        }
    }

    public void SpawnVideo()
    {
        List<MeshRenderer> canvas_to_render = new List<MeshRenderer>();

        for (int i = 0; i < monkeys.Length; ++i)
        {
            if (!monkey_renderer[i].enabled)
                canvas_to_render.Add(monkey_renderer[i]);
        }

        if (canvas_to_render.Count == 0)
            return;

        int rand_canvas = Random.Range(0, canvas_to_render.Count);
        monkey_renderer[rand_canvas].enabled = true;
        monkey_video[rand_canvas].GenerateData();
    }
}
