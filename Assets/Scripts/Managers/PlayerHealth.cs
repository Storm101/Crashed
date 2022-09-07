using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private static PlayerHealth instance = null;
    public static PlayerHealth Instance {
        get {
            if (instance == null) {
                Debug.LogError("PlayerHealth not found");
            }
            return instance;
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Debug.LogWarning("Multiple PlayerHealth found");
            Destroy(this);
            return;
        }
    }

    public float health;
    public float maxHealth;
    public Slider healthSlider;
    public GameObject deathScreen;

    private void Update() {
        if (health <= 0) {
            deathScreen.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (health > maxHealth) {
            health = maxHealth;
        }
        healthSlider.value = health / maxHealth;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
