using UnityEngine;

public class Player : KillableEntity {
    [SerializeField] private int fallBoundary = -20;
    [SerializeField] private CameraShake cameraShake;

    private void Update() {
        if (transform.position.y <= fallBoundary) Die();
    }

    public override void ReceiveDamage(float damage) {
        if (damage > 0) cameraShake.Shake();
        base.ReceiveDamage(damage);
    }

    public override void Die() {
        base.Die();
        GameManager.OnPlayerDeath(this);
    }
}
