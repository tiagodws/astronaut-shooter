using UnityEngine;

public class StickToCameraBottom : MonoBehaviour {
    [SerializeField] Camera mainCam;
    [SerializeField] private float yOffset = 0f;

    private void Awake() {
        if (mainCam == null) {
            mainCam = Camera.main;
        }
    }

    private void Start() {
        Position();
    }

    private void Update() {
        Position();
    }

    private void Position() {
        float bottomY = mainCam.ScreenToWorldPoint(Vector3.zero).y;
        transform.position = new Vector3(transform.position.x, bottomY + yOffset, transform.position.z);
    }
}
