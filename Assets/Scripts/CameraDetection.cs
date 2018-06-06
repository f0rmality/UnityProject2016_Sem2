using UnityEngine;
using System.Collections;

public class CameraDetection : MonoBehaviour {

    public Camera_Search cameraScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            cameraScript.isDetected = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            cameraScript.isDetected = false;
        }
    }
}
