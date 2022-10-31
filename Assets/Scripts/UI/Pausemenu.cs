using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pausemenu : MonoBehaviour {
    public GameObject m_pauseMenu;
    public GameObject m_ConfirmMenu;
    public GameObject m_ConfirmDesktopMenu;
    private bool inPause;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Resume();
        }
    }

    public void Resume() {
        if (inPause) {
            if (m_ConfirmDesktopMenu.activeSelf || m_ConfirmMenu.activeSelf) CancelQuit();
            else {
                m_pauseMenu.SetActive(false);
                m_ConfirmMenu.SetActive(false);
                m_ConfirmDesktopMenu.SetActive(false);
                inPause = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else {
            m_pauseMenu.SetActive(true);
            inPause = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void ConfirmQuit() {
        m_ConfirmMenu.SetActive(true);
        m_pauseMenu.SetActive(false);
    }

    public void ConfirmDesktopQuit() {
        m_ConfirmDesktopMenu.SetActive(true);
        m_pauseMenu.SetActive(false);
    }

    public void CancelQuit() {
        m_ConfirmDesktopMenu.SetActive(false);
        m_ConfirmMenu.SetActive(false);
        m_pauseMenu.SetActive(true);
    }
}
