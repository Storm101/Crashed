using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertBox : MonoBehaviour
{
    //Alert box variables
    public GameObject alertBox;
    public Transform guiCanvas;

    //Alert popup function
    public void AlertPopup(string text, bool stayActive, float alertActiveTime)
    {
        alertBox.SetActive(true);
        alertBox.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = text;
        GetComponent<GameManager>().alertActive = true;

        //If the popup is not meant to stay active, close the popup after 5 seconds
        if (!stayActive)
            Invoke("ClosePopup", alertActiveTime);
    }

    //Close alert popup function
    public void ClosePopup()
    {
        alertBox.SetActive(false);
        GetComponent<GameManager>().alertActive = false;
    }
}
