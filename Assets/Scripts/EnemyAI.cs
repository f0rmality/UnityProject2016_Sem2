using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Animator animator;

    public float TotalHealth = 100f;
    public float CurrentHealth = 100f;
    public bool isAlive = true;

    public Image healthBarFill;

    const float WANDER_SPEED = 7.0f;
    const float SEEK_SPEED = 15f;
    const float EVADE_SPEED = 20.0f;

    public GameObject player;
    public NavMeshAgent agent;

    public GameObject model;

    public DetectionScript detectionScript;
    public EnemyAttackScript attackScript;

    public GameObject WanderTarget;
    public GameObject WanderCenter;
    float wanderAngleTo;
    float turnSpeed = 30;

    Coroutine JitterCoroutine;
    bool createJitter = false;

    public enum ENEMYSTATE
    {
        Attack,
        Wander,
        Seek,
        Death
    }

    public ENEMYSTATE currentEnemyState = ENEMYSTATE.Wander;

    // Use this for initialization
    void Start()
    {

    }

    public void EnemyHit(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, TotalHealth);
        healthBarFill.fillAmount = CurrentHealth / TotalHealth;

        if (CurrentHealth <= 0) SwitchState(ENEMYSTATE.Death);
    }

    public void SwitchState(ENEMYSTATE nextState)
    {
        Debug.Log("Switching to: " + nextState);

        switch (nextState)
        {
            case ENEMYSTATE.Wander:
                agent.enabled = false;
                break;
            case ENEMYSTATE.Seek:
                agent.enabled = true;
                agent.Resume();
                break;
            case ENEMYSTATE.Attack:
                agent.enabled = true;
                agent.Resume();
                break;
            case ENEMYSTATE.Death:
                agent.enabled = false;
                break;
        }
        currentEnemyState = nextState;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentEnemyState)
        {
            case ENEMYSTATE.Wander:
                Wander();
                break;
            case ENEMYSTATE.Seek:
                Seek();
                break;
            case ENEMYSTATE.Attack:
                Attack();
                break;
            case ENEMYSTATE.Death:
                if (isAlive) Death();
                break;
        }
    }

    //void IfAlerted(){ switch to Attack(); }


    void OnTriggerEnter(Collider collider)
    {
        if (currentEnemyState == ENEMYSTATE.Wander)
        {
            if (JitterCoroutine != null)
                StopCoroutine(JitterCoroutine);

            createJitter = true;
            ImmediateJitter(90);
        }
    }

    void ImmediateJitter(float turnAngle)
    {
        //when enemy triggers a collision with an object
        //turn to the left or right, increases angle by 90 or -90 degrees

        wanderAngleTo += (Random.Range(0, 2) == 0) ? turnAngle : -turnAngle;
        createJitter = false;

    }


    void Wander()
    {
        if (!createJitter)
        {
            JitterCoroutine = StartCoroutine(Jitter(5));
            createJitter = true;
        }

        Vector3 dir = WanderTarget.transform.position - WanderCenter.transform.position;

        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;

        //Debug.Log("Wander Target Angle is: " + angle);

        float deltaAngle = Mathf.DeltaAngle(wanderAngleTo, angle);

        if (Mathf.Abs(deltaAngle) < turnSpeed * Time.deltaTime)
        {
            //Debug.Log("ANGLE REACHED!");
        }
        else
        {
            if (deltaAngle < 0)
            {
                WanderTarget.transform.RotateAround(WanderCenter.transform.position,
                    Vector3.up, -turnSpeed * Time.deltaTime);
            }
            else
            {
                WanderTarget.transform.RotateAround(WanderCenter.transform.position,
                    Vector3.up, turnSpeed * Time.deltaTime);
            }
        }


        Vector3 dirToTarget = WanderTarget.transform.position - model.transform.position;

        Vector3 dirWithoutY = new Vector3(dirToTarget.x, 0, dirToTarget.z);

        dirWithoutY.Normalize();

        Vector3 velocity = new Vector3(dirWithoutY.x * WANDER_SPEED, rigidBody.velocity.y, dirWithoutY.z * WANDER_SPEED);

        rigidBody.velocity = velocity;


        //FACE THE TARGET
        Quaternion rotationToFace = Quaternion.LookRotation(dirWithoutY, Vector3.up);
        model.transform.rotation = rotationToFace;


    }

    void Seek()
    {
        if (agent.enabled)
        {
            animator.SetBool("isSeeking", true);
            agent.SetDestination(player.transform.position);
        }

        FaceAngle();
    }

    void Attack()
    {
        if (agent.enabled)
        {
            //if seeking, set destination player
            //maybe change agent enabled to "isSeeking" or something?
            agent.SetDestination(player.transform.position);
        }

        FaceAngle();
    }

    void Death()
    {
        isAlive = false;
        rigidBody.constraints = RigidbodyConstraints.None;

        detectionScript.gameObject.SetActive(false);
        attackScript.gameObject.SetActive(false);

        animator.SetBool("isAlive", false);
        StartCoroutine(RemoveEnemy());
    }

    void FaceAngle()
    {
        Quaternion rot = model.transform.localRotation;
        model.transform.localRotation = Quaternion.Slerp(rot, Quaternion.identity, 0.2f);
    }

    IEnumerator Jitter(float time)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);

        yield return waitForSeconds;

        wanderAngleTo -= Random.Range(-10, 11) * 10f;

        Debug.Log("Adding Jitter");

        createJitter = false;
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
