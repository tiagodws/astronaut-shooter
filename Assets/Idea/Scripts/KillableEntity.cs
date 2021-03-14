using UnityEngine;
using UnityEngine.Events;

public abstract class KillableEntity : MonoBehaviour, IHealable, IDamageable {
    [SerializeField] private float hitPoints = 100f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSoundEffect;
    [SerializeField] private GameObject deathVisualEffect;
    [SerializeField] private AudioClip damageSoundEffect;
    [SerializeField] private GameObject damageVisualEffect;
    [SerializeField] private AudioClip healingSoundEffect;
    [SerializeField] private GameObject healingVisualEffect;
    [SerializeField] private UnityEvent<float, float> OnHitPointsChange = new UnityEvent<float, float>();

    private float _currentHitPoints;
    public float currentHitPoints => _currentHitPoints;

    private void Awake() {
        _currentHitPoints = hitPoints;
    }

    private void Start() {
        OnHitPointsChange.Invoke(hitPoints, hitPoints);
    }

    public virtual void ReceiveDamage(float damage) {
        if (damage < 0) {
            Debug.LogError("Cannot receive negative damage");
            return;
        }

        float startingHitPoints = _currentHitPoints;
        _currentHitPoints = Mathf.Max(0f, _currentHitPoints - damage);
        float damageTaken = startingHitPoints - _currentHitPoints;

        OnHitPointsChange.Invoke(_currentHitPoints, hitPoints);
    
        if (_currentHitPoints <= 0f) {
            Die();
            return;
        }

        PlayAudioClip(damageSoundEffect);

        if (damageVisualEffect != null) {
            GameObject particles = Instantiate(damageVisualEffect, transform.position, transform.rotation);
            Destroy(particles, 5f);
        }
    }

    public virtual void ReceiveHealing(float healing) {
        if (healing < 0) {
            Debug.LogError("Cannot receive negative healing");
            return;
        }

        float startingHitPoints = currentHitPoints;
        _currentHitPoints = Mathf.Min(_currentHitPoints + healing, hitPoints);
        float healingTaken = _currentHitPoints - startingHitPoints;

        OnHitPointsChange.Invoke(_currentHitPoints, hitPoints);

        PlayAudioClip(healingSoundEffect);

        if (healingVisualEffect != null) {
            GameObject particles = Instantiate(healingVisualEffect, transform.position, transform.rotation);
            Destroy(particles, 5f);
        }
    }

    public virtual void Die() {
        PlayAudioClip(deathSoundEffect);

        if (deathVisualEffect != null) {
            GameObject particles = Instantiate(deathVisualEffect, transform.position, transform.rotation);
            Destroy(particles, 5f);
        }

        Destroy(gameObject);
    }

    protected virtual void PlayAudioClip(AudioClip clip) {
        if (clip == null) return;

        if (audioSource == null) SoundManager.PlayOneShot(clip);
        else audioSource.PlayOneShot(clip);
    }
}
