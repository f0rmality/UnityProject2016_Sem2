using UnityEngine;
using System.Collections;

public class Camera_Search : MonoBehaviour {

    public bool isDetected = false;
    public PlayerProgress progressScript;
    public EnemyAI enemies;
    public GameObject cameraObject;
    public Light cameraLight;

    private float camSpeed = 7.0f;

    bool turnLeft = true;
    bool camerasOn;

    //USE THIS SAME METHOD FOR A SIREN

	// Use this for initialization
	void Start () {

        camerasOn = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (progressScript.progressLevel == 4)
            camerasOn = false;

        if (isDetected)
        {
            StopAllCoroutines();
            cameraLight.color = Color.green;
            enemies.currentEnemyState = EnemyAI.ENEMYSTATE.Attack;
        }

        if (!isDetected && camerasOn)
        {
            cameraLight.color = Color.red;
            StartCoroutine(cameraSearch());
        }

        if (!camerasOn)
        {
            StopAllCoroutines();
            cameraLight.enabled = false;
            //turn off lights
            //change cam light to red
        }

	}

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Detected!!!");
            isDetected = true;
        //if detected play siren noise and change light color
        //then call enemyAI script or something to come and attack player
        }
    }

    IEnumerator cameraSearch()
    {
        if(turnLeft)
        {
            cameraObject.transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime);
            yield return new WaitForSeconds(camSpeed);
            turnLeft = false;
        }


        else
        {
            cameraObject.transform.Rotate(new Vector3(0, -15, 0) * Time.deltaTime);
            yield return new WaitForSeconds(camSpeed);
            turnLeft = true;
        }

    }
}
