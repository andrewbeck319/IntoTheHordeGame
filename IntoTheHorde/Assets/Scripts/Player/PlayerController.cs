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
    private bool rotating = false;

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

    //Invulnerability 

    private float invulTime = 3.0f; 
    private float invulCooldown = 5.0f; 
    private bool canInvul = true; 
    private bool isInvul = false; 

    // Particle System for Player 

    public ParticleSystem invulnerabilityPS; 

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

        if (Input.GetKey(KeyCode.A))
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
                if(!rotating)
                    transform.RotateAround(transform.position, transform.up, 180f);

                //cam after
                this.transform.GetChild(4).position = camPos;
                this.transform.GetChild(4).rotation = camRot;
            }

            if(!rotating)
                facingDirection = FacingDirection.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
			Animationcontroller.SetBool("walk",true);
            this.transform.Translate(-Vector3.right * speed * Time.deltaTime);
            //spriteRenderer.flipX = true; gotta flip the whole gameobject


            if (facingDirection != FacingDirection.Right)
            {
                Transform camGet = this.transform.GetChild(4);
                Vector3 camPos = camGet.position;
                Quaternion camRot = camGet.rotation;

                if(!rotating)
                    transform.RotateAround(transform.position, transform.up, -180f);
                this.transform.GetChild(4).position = camPos;
                this.transform.GetChild(4).rotation = camRot;

            }
            if(!rotating)
                facingDirection = FacingDirection.Right;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            Animationcontroller.SetBool("walk",true);
            this.transform.Translate((facingDirection == FacingDirection.Right) ? -Vector3.forward * speed * Time.deltaTime : Vector3.forward * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S))
        {
            Animationcontroller.SetBool("walk",true);
            this.transform.Translate((facingDirection == FacingDirection.Right) ? Vector3.forward * speed * Time.deltaTime : -Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E) && !rotating)
        {
            // this.GetComponent<Rigidbody>().AddTorque(0, 0.5f, 0);
            //this.transform.Rotate(0, 90, 0);
            StartCoroutine(cameraRotate(90));
        } else if (Input.GetKey(KeyCode.Q) && !rotating)
        {
            // this.GetComponent<Rigidbody>().AddTorque(0, -0.5f, 0);
            //this.transform.Rotate(0, -90, 0);
            StartCoroutine(cameraRotate(-90));
        }

        if(Input.GetKey(KeyCode.Space))
        {
			Animationcontroller.SetBool("Attack",true);
            if (characterCombat.attackOffCooldown())
            {
                float randNum = Random.Range(0, 3);
                if (randNum == 0)
                {
                    audioManager.Play("PlayerAttackSwing1");
                }
                else if (randNum == 1)
                {
                    audioManager.Play("PlayerAttackSwing2");
                }
                else if (randNum == 2)
                {
                    audioManager.Play("PlayerAttackSwing3");
                }
            }
            characterCombat.Attack();
        }
        lastFacingDirection = facingDirection;
		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Space))
        {
		    Animationcontroller.SetBool("walk",false);
		    Animationcontroller.SetBool("Attack",false);
		}

        if (Input.GetKeyUp(KeyCode.F))
        {
            StartCoroutine(PlayerInteract());
        }

        if(Input.GetKeyDown(KeyCode.J) && canDash)
        {
            audioManager.Play("Dash");
            StartCoroutine(Dash());
        }

        if(Input.GetKeyDown(KeyCode.K) && leapCount != playerStats.leapMaxCount.GetValue())
        {
            audioManager.Play("Jump");
            Vector3 forceVec = new Vector3(0.0f, 0.1f * playerStats.leapHeight.GetValue(), 0.0f );
            rb.AddForce(forceVec, ForceMode.Impulse);
            leapRegenDelay = playerStats.leapRegenRate.GetValue();
            leapCount++;
        }

        if(Input.GetKeyDown(KeyCode.L) && canInvul)
        {
            audioManager.Play("Invulnerability");
            StartCoroutine(Invulnerability());
        }

    }
    public void TakeDamage(CharacterStats stats)
    {
        if(!isInvul){
            audioManager.Play("PlayerHit");
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
        }
        regenDelay = playerStats.shieldRegenDelay.GetValue();
    }

    private void ToggleInvulnerabiltyPS()
    {
        if(invulnerabilityPS.isPlaying)
        {
            invulnerabilityPS.Stop();
        }
        else{
            invulnerabilityPS.Play();
        }
    }

    public void invulnerabilityBuff(float percent)
    {
        invulTime *= percent;
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

    private IEnumerator Invulnerability()
    {
        canInvul = false; 
        isInvul = true; 
        ToggleInvulnerabiltyPS();
        yield return new WaitForSeconds(invulTime);
        isInvul = false; 
        ToggleInvulnerabiltyPS();
        yield return new WaitForSeconds(invulCooldown);
        canInvul = true; 
    }

    private IEnumerator cameraRotate(float angle)
    {
        rotating = true;
        var begin = transform.rotation;
        var end = begin * Quaternion.Euler(0, angle, 0);

        for (var t = 0f; t < 1; t += Time.deltaTime/0.5f)
        {
            this.transform.rotation = Quaternion.Slerp(begin, end, t);
            yield return null;
        }
        rotating = false;
    }
}
