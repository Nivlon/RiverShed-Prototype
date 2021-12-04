using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    [SerializeField]
    float playerMaxHealth = 10f;
    [SerializeField]
    Volume screenShader = null;
    float playerHealth;

    private void Start() {
        SetPlayerHealth(playerMaxHealth);
    }
    public void SetPlayerHealth(float _playerHealth) {
        playerHealth = Mathf.Clamp(_playerHealth,0f,playerMaxHealth);
        if(screenShader!=null) {
            screenShader.weight = Mathf.Lerp(1f,0f,Mathf.InverseLerp(0,playerMaxHealth,playerHealth));
        }
        if(playerHealth<=0f) {
            Die();
        }
    }

    void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public float GetPlayerHealth() {
        return playerHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")) {
            SetPlayerHealth(GetPlayerHealth()-other.gameObject.GetComponent<DamageScript>().GetDamage());
            Debug.Log("Ouch: "+ other.gameObject.GetComponent<DamageScript>().GetDamage());
        }
    }
}