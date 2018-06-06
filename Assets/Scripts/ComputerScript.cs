using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComputerScript : MonoBehaviour {

    public PlayerProgress progressScript;

    public GameObject computer;
    public GameObject gateDoorLeft;
    public Collider gateCollider;

    public Light computerLight;
    public Canvas computerUI;
    public Text instructions;
    public Text complete;

    bool computerOn;

	// Use this for initialization
	void Start () {
        computerOn = true;
        computerUI.enabled = false;
        instructions.enabled = false;
        complete.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(computerOn && progressScript.progressLevel == 3)
            {
                computerUI.enabled = true;
                instructions.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    instructions.enabled = false;
                    complete.enabled = true;
                    computerLight.color = Color.red;
                    progressScript.progressLevel = 4;

                    //open the gate here
                    //disable the collider
                    gateCollider.enabled = false;
                    gateDoorLeft.transform.localPosition = new Vector3(-25f, 5.7f, -36f);
                    gateDoorLeft.transform.localRotation = Quaternion.Euler(new Vector3(0, 67f, 0));
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(progressScript.progressLevel >= 4)
        {
            complete.enabled = false;
            computerUI.enabled = false;
            //change music
        }
    }
}
