using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectionSystem : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject engine;
    public GameObject cockpit;
    public GameObject hyperdrive;
    public Text engineText;
    public Text cockpitText;
    public Text hyperdriveText;

    public GameObject engineCheck;
    public GameObject cockpitCheck;
    public GameObject hyperdriveCheck;

    public GameObject Ship;
    public int PartsCollected;
    public int PartsNeeded = 3;
    public int partsHolding;

    public List<GameObject> collectedParts;

    private void Awake()
    {
        collectedParts = new List<GameObject>();

        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(engine)
        {
            if (col.gameObject.name == engine.name)
            {
                collectedParts.Add(engineCheck);
                engineText.text = "Install the engine module";
                engine = null;
                Destroy(col.gameObject);
                PartsCollected++;
                partsHolding++;

                gameManager.GetComponent<AlertBox>().AlertPopup(col.GetComponent<ModuleInfo>().text, col.GetComponent<ModuleInfo>().stayActive, col.GetComponent<ModuleInfo>().alertActiveTime);
            }
        }
        if (cockpit)
        {
            if (col.gameObject.name == cockpit.name)
            {
                collectedParts.Add(cockpitCheck);
                cockpitText.text = "Install the cockpit module";
                cockpit = null;
                Destroy(col.gameObject);
                PartsCollected++;
                partsHolding++;

                gameManager.GetComponent<AlertBox>().AlertPopup(col.GetComponent<ModuleInfo>().text, col.GetComponent<ModuleInfo>().stayActive, col.GetComponent<ModuleInfo>().alertActiveTime);
            }
        }
        if (hyperdrive)
        {
            if (col.gameObject.name == hyperdrive.name)
            {
                collectedParts.Add(hyperdriveCheck);
                hyperdriveText.text = "Install the hyperdrive module";
                hyperdrive = null;
                Destroy(col.gameObject);
                PartsCollected++;
                partsHolding++;

                gameManager.GetComponent<AlertBox>().AlertPopup(col.GetComponent<ModuleInfo>().text, col.GetComponent<ModuleInfo>().stayActive, col.GetComponent<ModuleInfo>().alertActiveTime);
            }
        }
    }
}
