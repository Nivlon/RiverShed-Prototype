using UnityEngine;
using UnityEngine.Rendering;

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
            //screenShader. = Mathf.Lerp(-100f,100f,Mathf.InverseLerp(0,playerMaxHealth,playerHealth));
        }
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