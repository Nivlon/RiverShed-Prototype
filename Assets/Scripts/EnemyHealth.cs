using UnityEngine;

public class EnemyHealth : MonoBehaviour,ITakeHit {
    [SerializeField]
    float maxHealth = 2f;
    float health;

    private void Start() {
        SetHealth(maxHealth);
    }
    public void SetHealth(float _health) {
        health = Mathf.Clamp(_health,0f,maxHealth);
        if(health==0f) {
            Die();
        }
    }

    private void Die() {
        Destroy(this.gameObject);
    }

    public float GetHealth() {
        return health;
    }

    public void Hit(Vector2 direction, float damage) {
        SetHealth(GetHealth()-damage);
        Debug.Log("Enemy hit for " + damage);
    }
}