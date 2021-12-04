using UnityEngine;

public class DamageScript : MonoBehaviour {
    [SerializeField]
    float damage = 1f;

    public float GetDamage() {
        return damage;
    }

    public void SetDamage(float _damage) {
        damage = _damage;
    }
}