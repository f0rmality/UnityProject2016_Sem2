using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Building01_Script : MonoBehaviour {

    public Canvas buildingUI;
    public Text instructions;
    public Text resolvedQuest;
    public PlayerProgress progressScript;

	// Use this for initialization
	void Start () {
        buildingUI.enabled = false;
        instructions.enabled = false;
        resolvedQuest.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player") && progressScript.progressLevel == 1)
        {
            buildingUI.enabled = true;
            instructions.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                instructions.enabled = false;
                resolvedQuest.enabled = true;
                progressScript.progressLevel = 2;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            buildingUI.enabled = false;
        }
    }
}
