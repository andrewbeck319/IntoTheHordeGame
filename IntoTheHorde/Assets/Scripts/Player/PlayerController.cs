using UnityEditor;
using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
	[SerializeField] private Animator Animationcontroller; 
    private bool _isRunning = false;

    private PlayerStats playerStats;
    private ShieldHandler shieldHandler;
    private HealthHandler healthHandler;
    private CharacterCombat characterCombat;
    private AudioManager audioManager;


    //Ability shit==================

    //Dash
    private bool canDash = true;
    private bool isDashing;
    //could maybe make these stats in character stats
    public float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Rigidbody rb;

    //Leap
    private int leapCount = 0;
    private float leapRegenDelay = 0.0f;

    //shield shit
    private float regenDelay;
    private float regenAccum = 0.0f;

    public bool playerInteracted = false;
    enum FacingDirection
    {
        Left,
        Right
    }

    private FacingDirection facingDirection = FacingDirection.Left;
    private FacingDirection lastFacingDirection = FacingDirection.Left;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        shieldHandler = GetComponent<ShieldHandler>();
        healthHandler = GetComponent<HealthHandler>();
        characterCombat = GetComponent<CharacterCombat>();

        healthHandler.healthSystem.SetMaxHealth(playerStats.maxHealth);
        healthHandler.healthSystem.SetHealthPercent(100);

        shieldHandler.shieldSystem.SetMaxShield(playerStats.shield.GetValue());
        shieldHandler.shieldSystem.SetShieldPercent(100);

        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        regenDelay -= Time.deltaTime;
        leapRegenDelay -= Time.deltaTime;
       
        if (regenDelay <= 0)
        {
            if(shieldHandler.shieldSystem.GetShield() < shieldHandler.shieldSystem.GetMaxShield())
            {
                //do regen
                regenAccum += playerStats.shieldRegenRate.GetValue() * Time.deltaTime;
                if (regenAccum >= 1.0f)
                {
                    shieldHandler.shieldSystem.Regen((int)regenAccum);
                    regenAccum = 0.0f;
                }
            }
            else
            {
                regenAccum = 0.0f;
            }
        }

        if (leapRegenDelay <= 0)
        {
            leapCount--;
            if (leapCount < 0) leapCount = 0;
        }

        if (isDashing)
        {
            return;
        }

        this._isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = this._isRunning ? 6f : 3.5f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
			Animationcontroller.SetBool("walk",true);
            this.transform.Translate(-Vector3.right * speed * Time.deltaTime);

            //spriteRenderer.flipX = false; gotta flip the whole gameobject
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0, 180,0));


            if (facingDirection != FacingDirection.Left) //then we gotta rotate the gameobject, and since the camera is a child, reset its rotation manually
            {
                //cam before
                Transform camGet = this.transform.GetChild(4);
                Vector3 camPos = camGet.position;
                Quaternion camRot = camGet.rotation;

                transform.RotateAround(transform.position, transform.up, 180f);

                //cam after
                this.transform.GetChild(4).position = camPos;
                this.transform.GetChild(4).rotation = camRot;
            }


            facingDirection = FacingDirection.Left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
			Animationcontroller.SetBool("walk",true);
            this.transform.Translate(-Vector3.right * speed * Time.deltaTime);
            //spriteRenderer.flipX = true; gotta flip the whole gameobject


            if (facingDirection != FacingDirection.Right)
            {
                Transform camGet = this.transform.GetChild(4);
                Vector3 camPos = camGet.position;
                Quaternion camRot = camGet.rotation;

                transform.RotateAround(transform.position, transform.up, -180f);
                this.transform.GetChild(4).position = camPos;
                this.transform.GetChild(4).rotation = camRot;

            }

            facingDirection = FacingDirection.Right;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Animationcontroller.SetBool("walk",true);
            this.transform.Translate((facingDirection == FacingDirection.Right) ? -Vector3.forward * speed * Time.deltaTime : Vector3.forward * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            Animationcontroller.SetBool("walk",true);
            this.transform.Translate((facingDirection == FacingDirection.Right) ? Vector3.forward * speed * Time.deltaTime : -Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // this.GetComponent<Rigidbody>().AddTorque(0, 0.5f, 0);
            this.transform.Rotate(0, 100f * Time.deltaTime, 0);
        } else if (Input.GetKey(KeyCode.D))
        {
            // this.GetComponent<Rigidbody>().AddTorque(0, -0.5f, 0);
            this.transform.Rotate(0, -100f * Time.deltaTime, 0);
        }

        if(Input.GetKey(KeyCode.Space))
        {
			Animationcontroller.SetBool("Attack",true);
            float randNum = Random.Range(0,3);
            Debug.Log(randNum);
            if(randNum == 0)
            {
                audioManager.Play("PlayerAttackSwing1");
            }
            else if (randNum == 1)
            {
                audioManager.Play("PlayerAttackSwing2");
            }
            else if (randNum == 2){
                audioManager.Play("PlayerAttackSwing3");
            }
            characterCombat.Attack();
        }
        lastFacingDirection = facingDirection;
		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.Space))
        {
		    Animationcontroller.SetBool("walk",false);
		    Animationcontroller.SetBool("Attack",false);
		}

        if (Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(PlayerInteract());
        }

        if(Input.GetKeyDown(KeyCode.S) && canDash)
        {
            audioManager.Play("PlayerAttackSwing");
            StartCoroutine(Dash());
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && leapCount != playerStats.leapMaxCount.GetValue())
        {
            Vector3 forceVec = new Vector3(0.0f, 0.1f * playerStats.leapHeight.GetValue(), 0.0f );
            rb.AddForce(forceVec, ForceMode.Impulse);
            leapRegenDelay = playerStats.leapRegenRate.GetValue();
            leapCount++;
        }

    }
    public void TakeDamage(CharacterStats stats)
    {
        shieldHandler.shieldSystem.Damage(stats.damage.GetValue());
        if (shieldHandler.shieldSystem.GetShield() == 0)
        {
            healthHandler.healthSystem.Damage(playerStats.TakeDamage(stats.damage.GetValue()));
            if (playerStats.NeedsToDie())
            {
                //Destroy(gameObject);
                PlayerManager.instance.KillPlayer();
            }//skill issue
        }
        regenDelay = playerStats.shieldRegenDelay.GetValue();
    }
    private IEnumerator PlayerInteract()
    {
        playerInteracted = true;
        yield return new WaitForSeconds(1);
        playerInteracted = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.useGravity = false;
        rb.velocity = -transform.right * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
