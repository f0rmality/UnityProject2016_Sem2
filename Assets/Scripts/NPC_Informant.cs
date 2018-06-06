using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC_Informant : MonoBehaviour {

    public Canvas InformantUI;
    public Text informantDialogue;
    public Scene objectiveComplete;
    //scenes and all that

	// Use this for initialization
	void Start () {

        InformantUI.enabled = false;
        informantDialogue.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InformantUI.enabled = true;
            informantDialogue.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                //load objective complete scene
                //fade out here
                SceneManager.LoadScene(3);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InformantUI.enabled = false;
            informantDialogue.enabled = false;
        }
    }
}
