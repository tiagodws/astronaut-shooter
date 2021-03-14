using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {
    [SerializeField] private RectTransform statusBarRect;
    [SerializeField] private Text statusText;

    private void Update() {
        transform.rotation = Quaternion.identity;
    }

    public void SetStatus(float current, float max) {
        float value = current / max;

        statusBarRect.localScale = new Vector3(value, statusBarRect.localScale.y, statusBarRect.localScale.z);
        statusText.text = current + "/" + max;
    }
}
