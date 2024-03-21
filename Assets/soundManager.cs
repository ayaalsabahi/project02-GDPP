using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class soundManager : MonoBehaviour
{

    public AudioClip mouseSound;
    public AudioClip backgroundMusic;
    public AudioClip possesion;
    public AudioClip getKey;
    public AudioClip slidingBox;
    public AudioClip unlockingDoor;

    private AudioSource mouseSoundSource;
    private AudioSource backgroundMusicSource;
    private AudioSource possesionSource;
    private AudioSource getKeySource;
    private AudioSource slidingBoxSource;
    private AudioSource unlockingDoorSource;


    bool isBoy = true;

    //singleton setup 
    public static soundManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    //get audio source
    private void Start()
    {
        mouseSoundSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        possesionSource = gameObject.AddComponent<AudioSource>();
        getKeySource = gameObject.AddComponent<AudioSource>();
        slidingBoxSource = gameObject.AddComponent<AudioSource>();
        unlockingDoorSource = gameObject.AddComponent<AudioSource>();

        mouseSoundSource.clip = mouseSound;  
        backgroundMusicSource.clip = backgroundMusic;
        possesionSource.clip = possesion;
        getKeySource.clip = getKey;
        slidingBoxSource.clip = slidingBox;
        unlockingDoorSource.clip = unlockingDoor;

        //which sounds loop 
        backgroundMusicSource.loop = true;
        mouseSoundSource.loop = true;


        startMusic();
    }


    public void startMusic()
    {
        backgroundMusicSource.Play();
    }

    public void stopMusic()
    {
        backgroundMusicSource.Pause();
    }

    public void justPossesed()
    {
        isBoy = !isBoy;
        possesionSource.Play();

        if (!isBoy)
        {
            mouseSoundSource.Play();
        }
        else
        {
            mouseSoundSource.Pause();
        }

    }
}
