using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public SphereCollider RightHandCollider;
    public SphereCollider LeftHandCollider;
    public GameObject playerRifle;

	// Use this for initialization
	void Start () {

        RightHandCollider.enabled = false;
        LeftHandCollider.enabled = false;
	
	}


	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetRightHandCollider(int active)
    {
        RightHandCollider.enabled = (active == 0) ? false : true;  
        //^^^ if active = 0, turn off collider, if active = 1, turn on the collider 
    }

    public void SetLeftHandCollider(int active)
    {
        LeftHandCollider.enabled = (active == 0) ? false : true;
        //^^^ if active = 0, turn off collider, if active = 1, turn on the collider 
    }
    
    public void ShowRifle()
    {
        playerRifle.SetActive(true);
    }
}
