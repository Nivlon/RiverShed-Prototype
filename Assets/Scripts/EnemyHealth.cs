using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,ITakeHit {
    [SerializeField]
    float maxHealth = 2f;
    float health;
    [SerializeField] AudioSource audioSource = null;

    private bool dying = false;

    [SerializeField]
    SpriteRenderer SpriteNormal;
    [SerializeField]
    SpriteRenderer SpriteSoul;

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
        if(dying) {
            return;
        }
        dying = true;

        SpriteNormal.enabled = false;
        SpriteSoul.enabled = false;
        audioSource.Play();
        StartCoroutine(DeleteAfter(audioSource.clip.length));
    }

    IEnumerator DeleteAfter(float sec)
    {
        yield return new WaitForSeconds(sec);
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