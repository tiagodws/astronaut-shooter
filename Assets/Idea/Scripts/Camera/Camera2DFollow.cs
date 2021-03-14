using UnityEngine;

public class Camera2DFollow : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float damping = 1;
    [SerializeField] private float lookAheadFactor = 3;
    [SerializeField] private float lookAheadReturnSpeed = 0.5f;
    [SerializeField] private float lookAheadMoveThreshold = 0.1f;
    [SerializeField] private float yPosRestriction = -1f;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;
    private Vector3 lookAheadPos;
    private float nextTimeToSearch = 0f;
    private float searchDelay = 0.5f;

    private void Start() {
        if (target) Calibrate();
        transform.parent = null;
    }

    private void Update() {
        if (target == null) {
            SearchForPlayer();
            return;
        };

        float xMoveDelta = (target.position - lastTargetPosition).x;
        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget) {
            lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        } else {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

        float clamping = Mathf.Clamp(newPos.y, yPosRestriction, Mathf.Infinity);
        newPos = new Vector3(newPos.x, clamping, newPos.z);

        transform.position = newPos;
        lastTargetPosition = target.position;
    }

    private void Calibrate() {
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
    }

    private void SearchForPlayer() {
        if (nextTimeToSearch <= Time.time) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null) {
                target = player.transform;
                Calibrate();
            }

            nextTimeToSearch = Time.time + searchDelay;
        }
    }
}
