using UnityEngine;

public class PlayerArmRotation : MonoBehaviour {
    private PlayerMovement playerMovement;
    [SerializeField] private int rotationOffset;

    void Awake() {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float zRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation + rotationOffset);    
    }
}
