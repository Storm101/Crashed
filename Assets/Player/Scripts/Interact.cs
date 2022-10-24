using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Interact : MonoBehaviour
{
    //Manager variables
    private GameManager gameManager;

    public LayerMask ignoreLayer;
    public GameObject keyPrompt;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        //Raycast where the camera is looking
        RaycastHit hit;
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 2.5f, ~ignoreLayer))
        {
            if (hit.collider.gameObject.tag == "Ship" && gameManager.shipRepaired)
            {
                keyPrompt.SetActive(true);
                keyPrompt.transform.GetChild(0).GetComponent<TMP_Text>().text = "Press \"e\" to take off.";
                if (Input.GetKeyDown(KeyCode.E))
                    SceneManager.LoadScene(2);
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
            }
            else
            {
                keyPrompt.SetActive(false);
                keyPrompt.transform.GetChild(0).GetComponent<TMP_Text>().text = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {


            if (Physics.Raycast(ray, out hit, 5f, ~ignoreLayer))
            {
                if (hit.collider.gameObject.tag == "Ship" && gameManager.shipRepaired)
                {
                    SceneManager.LoadScene(2);
                }
            }



        }

        //Take off | End Game
        if (gameManager.shipRepaired)
        {
        }
    }
}