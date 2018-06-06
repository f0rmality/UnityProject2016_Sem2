using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Camera playerCamera;

    public Light flashlight;
    public Image flashlightImage;

    public GameObject playerRifle;
    public GameObject bullet;

   // public GameObject[] firedBullets;
   // public GameObject[] firedShots;

    public GameObject targetReticle;

    public Animator m_PlayerAnimator;
    public Animator m_EnemyAnimator;

    public AudioSource playerAudio;
    public AudioClip gunshot;
    public AudioClip reload;
    public PlayerMainStats playerStats;

    public float overrideAmount;

    public float PunchForce = 200f;
    public float shootForce = 2000f;
    public float gunDamage = 30f;

    public bool flashlightOn = false;
    public bool timeSlowed = false;
    public bool hasAmmo = false;
    public bool isReloading = false;

	// Use this for initialization
	void Start () {

        flashlightImage.enabled = false;
	
	}



    void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;


        //checks if thisCollider (the collider of the object the script is attached to) hits otherCollider (the collider it hits)
        if (contactPoints[0].thisCollider.gameObject.layer == LayerMask.NameToLayer("Melee")
            && contactPoints[0].otherCollider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyAI enemy = contactPoints[0].otherCollider.gameObject.GetComponent<EnemyAI>();
            enemy.rigidBody.AddForce(playerCamera.transform.forward * PunchForce, ForceMode.Impulse);
            //Debug.Log("HIT ENEMY");
        }
    }

    // Update is called once per frame
    void Update () {

        //turn this into a switch/case statement

        if (Input.GetMouseButtonDown(0))
        {
            if (hasAmmo && !isReloading)
                Shoot();
            //else play audio clip of empty click
        }

        else if (Input.GetMouseButton(1))
        {
            //aim
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            m_PlayerAnimator.SetTrigger("LeftJabTrigger");
            playerRifle.SetActive(false);
            //overrideAmount += 0.3f * Time.deltaTime;
        }

        else if (Input.GetKeyDown(KeyCode.F))
        {
            Flashlight();
        }

        else if (Input.GetKeyDown(KeyCode.T)) ///&& has battery charge
        {
            SlowTime();
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Grapple();
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }	
	}

    ///////////////////////// PLAYER EQUIPMENT METHODS ////////////////////////////

    void Reload()
    {
        if (hasAmmo)
        {
            isReloading = true;
            if (isReloading)
            {
                StartCoroutine(playerReloading());
            }
        }
    }

    void Flashlight()
    {
        if (!flashlightOn)
        {
            flashlightImage.enabled = true;
            flashlight.enabled = true; //turn off not red
            flashlightOn = true;
        }

        else
        {
            flashlightImage.enabled = false;
            flashlight.enabled = false;
            flashlightOn = false;
        }
    }

    void Grapple()
    {
        //use this type of method for a grapple? raycast out and if it hits a certain layer, have the rope attach to the layer

        Ray ray = new Ray(targetReticle.transform.position, targetReticle.transform.forward);
        RaycastHit rayCastHit;

        bool IsHit = Physics.Raycast(ray, out rayCastHit, Mathf.Infinity);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100);

        if (IsHit)
        {
            if (rayCastHit.collider.gameObject.layer == LayerMask.NameToLayer("LAYER NAME FOR GRAPPLEABLE"))
            {
                //create new object of class "grapple" 
                //in grapple class, have functionality for sticking to shit and climbing up it
                Debug.Log("Grappled Surface");
            }
        }
    }

    void SlowTime()
    {
        if (!timeSlowed)
        {
            timeSlowed = true;
            //slow time, similar to how it's used in Unreal, have a battery charge for it
            //if(battery charge < xxxx){ slow time, subtract battery charge over time }

            //in here, also slow down the music sounds and the jump stuff
            Time.timeScale = 0.15f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        else
        {
            timeSlowed = false;
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    void Hack()
    {
        // maybe for cameras? disable them if find a certain item, so if(item != 0){disable camera AI, turn off light, disable sound}//or camera health=0
    }

    void Shoot()
    {
        //muzzle flash
        --playerStats.currentClip;
        playerAudio.PlayOneShot(gunshot);

        //auto reload
        if (playerStats.currentClip <= 0 && playerStats.ExtraAmmo >= 0)
        {
            Reload();
        }

        //shot = Instantiate(bullet, targetReticle.transform.position, Quaternion.identity);
        //instantiate at spawn point 

        //Instantiate(bullet, targetReticle.transform.position, Quaternion.identity);
        //bullet.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * 1, ForceMode.Impulse);

        Ray ray = new Ray(targetReticle.transform.position, targetReticle.transform.forward);
        RaycastHit[] hitTarget = Physics.RaycastAll(ray, Mathf.Infinity);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100);

       // StartCoroutine(removeBullet());

        if (hitTarget.Length > 0)
        {
            for(int i = 0; i<hitTarget.Length; i++)
            {
                if (hitTarget[i].collider.gameObject.layer == LayerMask.NameToLayer("Enemy")){
                    EnemyAI enemy = hitTarget[i].collider.gameObject.GetComponent<EnemyAI>();
                    enemy.EnemyHit(gunDamage);
                    Debug.Log("Hit Enemy");
                }
            }
        }


    }

    IEnumerator removeBullet()
    {
        //fix this shit yo

        Debug.Log("cleaning up");
        yield return new WaitForSeconds(3);
        Destroy(bullet);
    }

    IEnumerator playerReloading() {
    
        //play a reload sound

        
        playerAudio.PlayOneShot(reload);
        yield return new WaitForSeconds(2f);

        if (playerStats.ExtraAmmo >= playerStats.fullClip)
        {
            playerStats.ExtraAmmo -= (playerStats.fullClip - playerStats.currentClip);
            playerStats.currentClip = playerStats.fullClip;
        }

        else
        {
            playerStats.currentClip = playerStats.ExtraAmmo;
            playerStats.ExtraAmmo = 0;
        }

        isReloading = false;

    }
}
