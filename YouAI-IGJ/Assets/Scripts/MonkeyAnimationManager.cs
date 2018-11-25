using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAnimationManager : MonoBehaviour {

    public Animator monkey;
    public Animator chair;
    public Video video;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        bool has_video = video.sprite_in_use != null ? true : false;
        monkey.SetBool("has_video", has_video);
        chair.SetBool("has_video", has_video);
    }
}
