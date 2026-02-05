using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource musicSource;
    public AudioSource stingerSource;

    public AudioClip musicaNormal;
    public AudioClip musicaAdaptativa;

    public AudioClip stinger1;
    public AudioClip stinger2;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        TocarNormal();  
    }

     
    public void TocarNormal()
    {
        musicSource.clip = musicaNormal;
        musicSource.loop = true;
        musicSource.Play();
    }

     
    public void TocarAdaptativa()
    {
        musicSource.clip = musicaAdaptativa;
        musicSource.loop = true;
        musicSource.Play();
    }

     
    public void Stinger1()
    {
        stingerSource.PlayOneShot(stinger1);
    }

    public void Stinger2()
    {
        stingerSource.PlayOneShot(stinger2);
    }
}
