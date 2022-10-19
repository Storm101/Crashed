using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertBox : MonoBehaviour
{
    public GameObject alertBox;
    public Transform guiCanvas;

    public void AlertPopup(string text, bool stayActive, float alertActiveTime)
    {
        alertBox.SetActive(true);
        alertBox.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = text;
        GetComponent<GameManager>().alertActive = true;

        if (!stayActive)
            Invoke("ClosePopup", 5);
    }

    public void ClosePopup()
    {
        alertBox.SetActive(false);
        GetComponent<GameManager>().alertActive = false;
    }
}
