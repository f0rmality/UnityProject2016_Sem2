using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	void Awake () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {
	
	}

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
