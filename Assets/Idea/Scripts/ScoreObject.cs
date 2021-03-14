using UnityEngine;

[RequireComponent (typeof(Collider2D))]

public class ScoreObject : MonoBehaviour {
    [SerializeField] private int scoreValue;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scoreSound;

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject otherGameObject = other.gameObject;

        bool isPlayer = otherGameObject.tag == "Player";

        if (isPlayer) Collect();
    }

    private void Collect() {
        ScoreManager.IncrementScore(scoreValue);
        PlayAudioClip(scoreSound);
        Destroy(gameObject);
    }

    protected virtual void PlayAudioClip(AudioClip clip) {
        if (clip == null) return;

        if (audioSource == null) SoundManager.PlayOneShot(clip);
        else audioSource.PlayOneShot(clip);
    }
}
