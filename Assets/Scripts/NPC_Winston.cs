using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_Winston : MonoBehaviour {

    bool spokenTo = false;
    bool objectiveGiven = false;
    bool allComplete = false;
    bool activated = false;
    bool isFleeing = false;

    public Canvas winstonUI;
    public Text winstonResponse01;
    public Text winstonResponse02;
    public Text winstonResponse03;
    public Text winstonResponse04;
    public PlayerProgress progressScript;

	// Use this for initialization
	void Start () {
        winstonUI.enabled = false;
        winstonResponse01.enabled = false;
        winstonResponse02.enabled = false;
        winstonResponse03.enabled = false;
        winstonResponse04.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerStay(Collider collider)
    {
        //if player layer and e is pressed
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player") && Input.GetKeyDown(KeyCode.E))
        {
            activated = true;

            if (!spokenTo)
            {
                //display text 1

                winstonUI.enabled = true;
                winstonResponse01.enabled = true;
                spokenTo = true;
                objectiveGiven = true;

                progressScript.progressLevel = 1;
            }

            else if (objectiveGiven && progressScript.progressLevel < 2)
            {
                winstonUI.enabled = true;
                winstonResponse02.enabled = true;
            }

            else if (progressScript.progressLevel == 2 && !allComplete)
            {
                winstonUI.enabled = true;
                winstonResponse03.enabled = true;
                progressScript.progressLevel = 3;
                allComplete = true;
            }

            else if (allComplete && progressScript.progressLevel >= 3)
            {
                winstonUI.enabled = true;
                winstonResponse04.enabled = true;
                //change animation to fall over
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        //says different things as you're leaving
        activated = false;

        if (spokenTo)
        {
            winstonUI.enabled = false;
            winstonResponse01.enabled = false;
            spokenTo = true;
            objectiveGiven = true;
        }

        else if (objectiveGiven && progressScript.progressLevel < 2)
        {
            winstonUI.enabled = false;
            winstonResponse02.enabled = false;
        }

        else if (progressScript.progressLevel == 2 && !allComplete)
        {
            winstonUI.enabled = false;
            winstonResponse03.enabled = false;
        }

        else if (allComplete && progressScript.progressLevel >= 3)
        {
            winstonUI.enabled = false;
            winstonResponse04.enabled = false;
        }
    }

    void Fleeing()
    {
        //access player script to see if the raycast hit the Winston NPC Layer

        //if shotAt(), go hostile
        isFleeing = true;
        //set animation to true
        //change position to running away from player (inverse of enermy moving towards player)
        //display text "wtf!!"
    }


    //AI states. if any?
}
