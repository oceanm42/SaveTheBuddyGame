using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sounds[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    private void Start()
    {
        Play("Theme");    
    }

    public void Play(string name)
    {
        foreach (Sounds s in sounds)
        {
            if(s.name == name)
            {
                s.source.Play();
                return;
            }
        }
        Debug.LogWarning("Could not find audio " + name + "!");
    }

}

[System.Serializable]
public class Sounds
{
    public AudioMixerGroup mixer;
    public string name;
    public AudioClip clip;
    public float volume;
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
