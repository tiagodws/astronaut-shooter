using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private int spawnDelaySeconds = 1;
    [SerializeField] private int respawnDelaySeconds = 2;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip spawnEffect;
    [SerializeField] private AudioClip backgroundMusic;
    public static GameManager instance = null;

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        SoundManager.Play(backgroundMusic);
        StartCoroutine(SpawnPlayer());
    }

    public static void OnPlayerDeath(Player player) {
        ScoreManager.IncrementScore(-100);
        instance.StartCoroutine(instance.RespawnPlayer());
    }

    private IEnumerator SpawnPlayer() {
        yield return new WaitForSeconds(spawnDelaySeconds);
        Instantiate(instance.playerPrefab, instance.spawnPoint.position, instance.spawnPoint.rotation);
        PlayAudioClip(instance.spawnEffect);
        SpawnParticles();
    }

    private IEnumerator RespawnPlayer() {
        yield return new WaitForSeconds(respawnDelaySeconds);
        StartCoroutine(SpawnPlayer());
    }

    private void SpawnParticles() {
        GameObject particles = Instantiate(spawnPrefab, instance.spawnPoint.position, instance.spawnPoint.rotation);
        Destroy(particles, 5f);
    }

    protected virtual void PlayAudioClip(AudioClip clip) {
        if (clip == null) return;

        if (audioSource == null) SoundManager.PlayOneShot(clip);
        else audioSource.PlayOneShot(clip);
    }
}