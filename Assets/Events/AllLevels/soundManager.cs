using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[System.Serializable]
public class soundManager : MonoBehaviour
{

    public AudioClip mouseSound;
    public AudioClip backgroundMusic;
    public AudioClip possesion;
    public AudioClip getKey;
    public AudioClip slidingBox;
    public AudioClip unlockingDoor;
    public AudioClip enemyPoof;

    private AudioSource mouseSoundSource;
    private AudioSource backgroundMusicSource;
    private AudioSource possesionSource;
    private AudioSource getKeySource;
    private AudioSource slidingBoxSource;
    private AudioSource unlockingDoorSource;
    private AudioSource enemyPoofSource;


    bool isBoy = true;

    //singleton setup 
    public static soundManager Instance { get; private set; }

    private void Awake()

    {
        // If there is an instance already, destroy this instance
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Set this instance as the singleton instance
            Instance = this;

            // Ensure the GameObject persists across scene changes
            DontDestroyOnLoad(gameObject);

            // Create AudioSource components
            mouseSoundSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            possesionSource = gameObject.AddComponent<AudioSource>();
            getKeySource = gameObject.AddComponent<AudioSource>();
            slidingBoxSource = gameObject.AddComponent<AudioSource>();
            unlockingDoorSource = gameObject.AddComponent<AudioSource>();
            enemyPoofSource = gameObject.AddComponent<AudioSource>();
            // Assign audio clips to AudioSource components
            mouseSoundSource.clip = mouseSound;
            backgroundMusicSource.clip = backgroundMusic;
            possesionSource.clip = possesion;
            getKeySource.clip = getKey;
            slidingBoxSource.clip = slidingBox;
            unlockingDoorSource.clip = unlockingDoor;
            enemyPoofSource.clip = enemyPoof;

            // Set loop for background music
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();


        }
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

    public void keySound()
    {
        getKeySource.Play();
    }

    public void doorSound()
    {
        unlockingDoorSource.Play();
    }

    public void enemyDissapearSound()
    {
        enemyPoofSource.Play();
    }
}
