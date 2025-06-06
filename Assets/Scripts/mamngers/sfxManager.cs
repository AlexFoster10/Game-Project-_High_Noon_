using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public static sfxManager instance;

    [SerializeField] private AudioSource sFXObj;
    [SerializeField] AudioClip backingTrack;


    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void playSFX(AudioClip audioClip,Transform transform, float volume)
    {
        //spawn oject to play audio
        AudioSource audioSource = Instantiate(sFXObj, transform.position, Quaternion.identity);

        //assign audio clip
        audioSource.clip = audioClip;

        //set volume of clip
        audioSource.volume = volume;

        //play
        audioSource.Play();

        //duration
        float clipDuration = audioSource.clip.length;

        //delete
        Destroy(audioSource.gameObject, clipDuration);

    }

    public void playRandSFX(AudioClip[] audioClip, Transform transform, float volume)
    {

        int rand = Random.Range(0, audioClip.Length);
        //spawn oject to play audio
        AudioSource audioSource = Instantiate(sFXObj, transform.position, Quaternion.identity);

        //assign audio clip
        audioSource.clip = audioClip[rand];

        //set volume of clip
        audioSource.volume = volume;

        //play
        audioSource.Play();

        //duration
        float clipDuration = audioSource.clip.length;

        //delete
        Destroy(audioSource.gameObject, clipDuration);

    }

    private void Start()
    {
        AudioSource audioSource = Instantiate(sFXObj, this.transform);
        audioSource.clip = backingTrack;
        audioSource.loop = true;
        audioSource.Play();
    }

}
