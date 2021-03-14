using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource musicSource;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static void PlayOneShot(AudioClip clip) {
        instance.effectsSource.PlayOneShot(clip);
    }

    public static void Play(AudioClip clip) {
        instance.musicSource.clip = clip;
        instance.musicSource.Play();
    }
}