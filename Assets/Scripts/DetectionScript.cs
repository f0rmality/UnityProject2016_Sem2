using UnityEngine;
using System.Collections;

public class DetectionScript : MonoBehaviour {

    public EnemyAI AIScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AIScript.SwitchState(EnemyAI.ENEMYSTATE.Seek);
            Debug.Log("seeking");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AIScript.SwitchState(EnemyAI.ENEMYSTATE.Wander);
            Debug.Log("wandering");
        }
    }
}
