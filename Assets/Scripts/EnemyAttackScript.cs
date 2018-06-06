using UnityEngine;
using System.Collections;

public class EnemyAttackScript : MonoBehaviour {

    public EnemyAI AIScript;
    public Animator animator;

    public PlayerMainStats playerStats;
    public AudioSource enemyAudio;
    public AudioClip gunshot;

    bool attackStarted = false;

    const float ENEMY_RANGED_DMG = 10f;
    const float ENEMY_MELEE_DMG = 30f;
    const float ENEMY_ATTACK_SPEED = 1f;

	// Use this for initialization
	void Start () {
	

	}

	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        //if !true then set to true, else return
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            animator.SetBool("isAttacking", true);
            AIScript.SwitchState(EnemyAI.ENEMYSTATE.Attack);

        }
    }

    void OnTriggerStay(Collider collider)
    {
        //TODO:
        //set the enemy to stay a certain ways back or freeze position
        //then throw raycasts, updating where they hit based on where the player has moved to

        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (AIScript.currentEnemyState == EnemyAI.ENEMYSTATE.Attack)
            {
                if (!attackStarted)
                {
                    StartCoroutine(AttackProcess());
                    attackStarted = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            animator.SetBool("isAttacking", false);
            AIScript.SwitchState(EnemyAI.ENEMYSTATE.Seek);
        }
    }

    IEnumerator AttackProcess()
    {
        //muzzle flash
        enemyAudio.PlayOneShot(gunshot);
        playerStats.currentHealth -= ENEMY_RANGED_DMG;
        Debug.Log("damage");

        yield return new WaitForSeconds(ENEMY_ATTACK_SPEED);
        attackStarted = false;
    }
	
	// Update is called once per frame
}
