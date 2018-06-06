using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LootScript : MonoBehaviour {

    public PlayerMainStats playerStats;
    public GameObject medpack;
    public GameObject ammopack;
    public GameObject[] lootpacks = new GameObject[2];

	// Use this for initialization
	void Start () {
        lootpacks[0] = medpack;
        lootpacks[1] = ammopack;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if(gameObject == lootpacks[0])
            {
                Debug.Log("picked up med");
                playerStats.currentHealth += 25f;
                //destroy medpack
            }

            if(gameObject == lootpacks[1])
            {
                Debug.Log("picked up ammo");
                playerStats.ExtraAmmo += 100;
                //destroy ammo 
            }
        }
    }
}
