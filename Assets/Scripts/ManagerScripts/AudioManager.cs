using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public bool mute = false;

	// Use this for initialization
	void Awake () {
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        DontDestroyOnLoad(this.gameObject);
	}

    private void Start()
    {
    }


    // Update is called once per frame
    void Update () {
 
        if(mute){
            AudioListener.volume = 0f;
        }
        else{
            AudioListener.volume = 1f;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find<Sound>(sounds, sound => sound.name == name);

        if(s == null){
            Debug.Log("Couldn't find named sound");
        }
        else{
            s.source.Play();
        }
    }
}
