using UnityEngine;

public class Parallaxing : MonoBehaviour {
    [SerializeField] private Transform[] elements;
    [SerializeField] private float smoothing = 1f;
    private float[] parallaxScales;
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    void Awake() {
        cameraTransform = Camera.main.transform;
    }

    void Start() {
        previousCameraPosition = cameraTransform.position;
        parallaxScales = new float[elements.Length];

        for (int i = 0; i < elements.Length; i++) {
            parallaxScales[i] = -elements[i].position.z;
        }
    }

    void Update() {
        for (int i = 0; i < elements.Length; i++) {
            Transform element = elements[i];
            Vector3 currentPosition = element.position;
            float parallaxScale = parallaxScales[i];

            float parallax = (previousCameraPosition.x - cameraTransform.position.x) * parallaxScale;
            float targetPosX = currentPosition.x + parallax;
            Vector3 targetPos = new Vector3(targetPosX, currentPosition.y, currentPosition.z);

            element.position = Vector3.Lerp(currentPosition, targetPos, smoothing * Time.deltaTime);
        }

        previousCameraPosition = cameraTransform.position;
    }
}
