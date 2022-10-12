using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertBox : MonoBehaviour
{
    public GameObject alertBoxPrefab;
    public Transform guiCanvas;

    public void AlertPopup(string text)
    {
        GameObject alertBox = Instantiate(alertBoxPrefab, guiCanvas);
        alertBox.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = text;
        GetComponent<GameManager>().alertActive = true;
    }
}
