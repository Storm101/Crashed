using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertUI : MonoBehaviour
{
    private GameManager gameManager;

    [Multiline]
    public string text;
    public bool stayActive;

    public float alertActiveTime = 5;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.GetComponent<AlertBox>().AlertPopup(text, stayActive, alertActiveTime);
            gameObject.SetActive(false);
        }
    }
}
