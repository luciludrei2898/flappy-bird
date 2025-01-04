using UnityEngine;

public class SoundSystem : MonoBehaviour
{

    public static SoundSystem instancie;
    public AudioClip audioPoint;
    public AudioClip audioFly;
    public AudioClip audioDie;
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
         if (SoundSystem.instancie == null)
        {
        SoundSystem.instancie = this;
        } else if (SoundSystem.instancie != this)
        {
            Destroy(gameObject);
        }       
    }

    public void PlayPoint()
    {
        PlayAudio(audioPoint);
    }

    public void PlayDie()
    {
        PlayAudio(audioDie);

    }

    public void PlayFly()
    {
        PlayAudio(audioFly);

    }

    private void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        if (SoundSystem.instancie == this)
        {
            SoundSystem.instancie = null;
        }
    }
}
