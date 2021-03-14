using UnityEngine;

[RequireComponent (typeof(Collider2D))]

public class KillerObject : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable = other.GetComponent(typeof(IDamageable)) as IDamageable;
        
        if (damageable != null) damageable.ReceiveDamage(Mathf.Infinity);
    }
}
