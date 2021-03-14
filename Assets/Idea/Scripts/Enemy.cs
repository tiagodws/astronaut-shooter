using UnityEngine;

public class Enemy : KillableEntity {
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private float damage = 20;
    [SerializeField] private LayerMask damageLayer;

    public override void Die() {
        base.Die();
        ScoreManager.IncrementScore(scoreValue);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        bool isInDamageLayer = (damageLayer & 1 << other.gameObject.layer) != 0;
        if (!isInDamageLayer) return;

        IDamageable damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if (damageable != null) {
            damageable.ReceiveDamage(damage);
        }

        Die();
    }
}
