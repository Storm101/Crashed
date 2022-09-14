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

    public float healingAmount = 5;
    public bool healing = true;
    private float tempHealth = 0;
    private float timer = 0;

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

        if (health < maxHealth) {
            if (health >= tempHealth) {
                timer += Time.deltaTime;
                if (timer > 3 && healing) {
                    health += Time.deltaTime * healingAmount;
                }
            }
            else {
                timer = 0;
            }
            tempHealth = health;
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
