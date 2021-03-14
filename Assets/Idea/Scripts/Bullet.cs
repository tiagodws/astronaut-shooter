using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour {
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 40;
    [SerializeField] private int range = 20;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject impactVisualEffect;
    [SerializeField] private AudioClip impactSoundEffect;
    private Vector2 startPosition;
    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start() {
        startPosition = transform.position;
        rb.velocity = transform.right * speed;
    }

    void FixedUpdate() {
        float distanceFromStart = Vector2.Distance(startPosition, transform.position);
        bool isPastRange = distanceFromStart > range;
        
        if (isPastRange) Explode();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        bool isInHitLayer = (hitLayer & 1 << other.gameObject.layer) != 0;
        if (!isInHitLayer) return;

        IDamageable damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if (damageable != null) damageable.ReceiveDamage(damage);

        Explode();
    }

    private void Explode() {
        PlayAudioClip(impactSoundEffect);
        GameObject explosion = Instantiate(impactVisualEffect, transform.position, transform.rotation);
        Destroy(explosion, 5f);
        Destroy(gameObject);
    }

    protected virtual void PlayAudioClip(AudioClip clip) {
        if (clip == null) return;

        if (audioSource == null) SoundManager.PlayOneShot(clip);
        else audioSource.PlayOneShot(clip);
    }
}
