using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fireDelay = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private float muzzleFlashMinScale = 0.6f;
    [SerializeField] private float muzzleFlashMaxScale = 0.9f;
    [SerializeField] private float muzzleFlashDuration = 0.01f;
    [SerializeField] private AudioClip fireEffect;
    [SerializeField] private UnityEvent onFire;
    private float myTime = 0.0F;
    private float nextFire;

    private void Awake() {
        nextFire = fireDelay;
    }

    private void Update() {
        myTime = myTime + Time.deltaTime;

        bool shouldFire = Input.GetButton("Fire1") || Input.GetButtonDown("Fire1");
        bool canFire = myTime > nextFire;

        if (shouldFire && canFire) {
            nextFire = myTime + fireDelay;
            Fire();
            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }

    private void Fire() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePointPosition = firePoint.position;

        FireEffects();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        onFire.Invoke();
    }

    private void FireEffects() {
        float sizeVariation = Random.Range(muzzleFlashMinScale, muzzleFlashMaxScale);

        PlayAudioClip(fireEffect);
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        Vector3 localScale = muzzleFlash.transform.localScale;
 
        muzzleFlash.transform.parent = firePoint;
        muzzleFlash.transform.localScale = new Vector2(localScale.x * sizeVariation, localScale.y * sizeVariation);

        Destroy(muzzleFlash, muzzleFlashDuration);
    }

    protected virtual void PlayAudioClip(AudioClip clip) {
        if (clip == null) return;

        if (audioSource == null) SoundManager.PlayOneShot(clip);
        else audioSource.PlayOneShot(clip);
    }
}
