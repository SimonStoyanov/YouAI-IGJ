using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MonkeyManager : MonoBehaviour
{

    GameObject[] monkeys;
    Video[] monkey_video;
    MeshRenderer[] monkey_renderer;
    public AudioMenuManage audio_random_monkeys;
    int seconds_a_day = 180;

    // primeros 2 dias -> spawn only entertainment
    // a partir del 3 dia -> critique
    // a partir del 5 comedia
    bool spawn_critique = false;
    bool spawn_comedy = false;

    private void Awake()
    {
        monkeys = GameObject.FindGameObjectsWithTag("Monkeys");
        monkey_video = new Video[monkeys.Length];
        monkey_renderer = new MeshRenderer[monkeys.Length];

        for (int i = 0; i < monkeys.Length; ++i)
        {
            monkey_video[i] = monkeys[i].GetComponent<Video>() as Video;
            GameObject capsule = monkeys[i].GetComponentInChildren<Button3D>().gameObject;
            monkey_renderer[i] = capsule.GetComponent<MeshRenderer>();
        }
    }

    private void Start()
    {
        StartCoroutine(WaitForCategory(seconds_a_day * 2, spawn_critique));
        StartCoroutine(WaitForCategory(seconds_a_day * 4, spawn_comedy));
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
        audio_random_monkeys.PlayRandomMonkey();
        monkey_video[rand_canvas].GenerateData();
        if (!spawn_critique && !spawn_comedy)
            monkey_video[rand_canvas].category = Video.Category.Entertainment;
        if (spawn_critique && !spawn_comedy)
            monkey_video[rand_canvas].category = (Video.Category) Random.Range(0, 2);
        
    }

    IEnumerator WaitForCategory(int i, bool spawn)
    {
        yield return new WaitForSecondsRealtime(i);
        spawn = true;
    }
}
