using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPC_Cop : MonoBehaviour {

    public Canvas copUI;
    public Text firstComment;
    public Text secondComment;
    public PlayerProgress progressScript;

	// Use this for initialization
	void Start () {
        copUI.enabled = false;
        firstComment.enabled = false;
        secondComment.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("it's the cop yo");
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            copUI.enabled = true;

            if (progressScript.progressLevel <= 1)
            {
                firstComment.enabled = true;
            }
            else
            {
                secondComment.enabled = true;
            }
        }
    }


    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            copUI.enabled = false;

            if (progressScript.progressLevel <= 1)
            {
                firstComment.enabled = false;
            }
            else
            {
                secondComment.enabled = false;
            }
        }
    }
}
