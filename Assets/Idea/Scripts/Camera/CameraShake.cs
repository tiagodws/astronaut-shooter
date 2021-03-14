using UnityEngine;

public class CameraShake : MonoBehaviour {
    [SerializeField] Camera mainCam;
    [SerializeField] private float shakeAmount = 0.05f;
    [SerializeField] private float shakeDurationSeconds = 0.1f;

    private void Awake() {
        if (mainCam == null) {
            mainCam = Camera.main;
        }
    }

    public void Shake() {
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", shakeDurationSeconds);
    }

    private void DoShake() {
        if (shakeAmount <= 0) return;

        Vector3 camPos = mainCam.transform.position;
        float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
        float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

        camPos.x += offsetX;
        camPos.y += offsetY;

        mainCam.transform.position = camPos;
    }

    private void StopShake() {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
