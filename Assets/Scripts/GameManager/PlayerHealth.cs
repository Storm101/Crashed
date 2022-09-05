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
                Debug.LogError("GameManager not found");
            }
            return instance;
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Debug.LogWarning("Multiple GameManagers found");
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
        healthSlider.value = health / maxHealth;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
