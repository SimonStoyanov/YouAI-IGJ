using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMenuManage : MonoBehaviour {

    // Use this for initialization
    public AudioSource monkey_1;
    public AudioSource monkey_2;
    public AudioSource monkey_3;
    public AudioSource monkey_4;
    public AudioSource monkey_5;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayRandomMonkey()
    {
        int rand = Random.Range(1, 5);
        switch(rand)
        {
            case 1:
                monkey_1.Play();
                break;
            case 2:
                monkey_2.Play();
                break;
            case 3:
                monkey_3.Play();
                break;
            case 4:
                monkey_4.Play();
                break;
            case 5:
                monkey_5.Play();
                break;
        }
    }
}
