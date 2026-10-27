using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMManager : MonoBehaviour
{

    [System.Serializable]
    class BGMTrack
    {
        [SerializeField] public AudioSource audioSource;
        public float targetVolume = 1.0f;
        public float fadeSpeed = 0.4f;
    }
    [SerializeField] List<BGMTrack> tracks;

    void Awake()
    {
        foreach(BGMTrack track in tracks)
        {
            track.audioSource.loop = true;
            track.audioSource.volume = 0;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach( BGMTrack track in tracks)
        {
            track.audioSource.volume = Mathf.MoveTowards(track.audioSource.volume, track.targetVolume,
                                        track.fadeSpeed * Time.unscaledDeltaTime);
            if(track.audioSource.volume <= 0 && track.targetVolume <= 0)
            {
                track.audioSource.loop = false;
                track.audioSource.Stop();
                track.audioSource.volume = 0;   
            }
        }
    }

    public void Play(AudioClip clip, float fadeTime)
    {
        foreach (BGMTrack track in tracks)
        {
            if (track.audioSource.isPlaying)
            {
                track.targetVolume = 0;
            }
        }

        BGMTrack repurposedTrack = null;
        bool isIdlePlayback = false;

        foreach (BGMTrack track in tracks)
        {
            if (!track.audioSource.isPlaying)
            {
                track.audioSource.clip = clip;
                track.audioSource.loop = true;
                track.audioSource.Play();
                track.targetVolume = 1;
                track.fadeSpeed = 1f / fadeTime;
                isIdlePlayback = true;
                break;
            }
        }
        if (!isIdlePlayback)
        {
            repurposedTrack = tracks[0];
            foreach (BGMTrack candidate in tracks)
            {
                if (candidate.audioSource.volume < repurposedTrack.audioSource.volume)
                {
                    repurposedTrack = candidate;

                }
            }
            repurposedTrack.audioSource.clip = clip;
            repurposedTrack.audioSource.loop = true;
            repurposedTrack.audioSource.Play();
            repurposedTrack.targetVolume = 1;
            repurposedTrack.fadeSpeed = 1f / fadeTime;
        }
    }
}
