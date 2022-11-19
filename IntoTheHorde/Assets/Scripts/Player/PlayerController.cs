using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Animator Animationcontroller; 
    private bool _isRunning = false;

    private PlayerStats playerStats; 
    private HealthHandler healthHandler;
    private CharacterCombat characterCombat;

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
        healthHandler = GetComponent<HealthHandler>();
        characterCombat = GetComponent<CharacterCombat>();

        healthHandler.healthSystem.SetMaxHealth(playerStats.maxHealth);
        healthHandler.healthSystem.SetHealthPercent(100);
    }

    // Update is called once per frame
    void Update()
    {
        this._isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = this._isRunning ? 6f : 3.5f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
			Animationcontroller.SetBool("walk",true);
            // this.GetComponent<Rigidbody>().AddForce(-transform.right);
            this.transform.Translate(-Vector3.right * speed * Time.deltaTime);

            //spriteRenderer.flipX = false; gotta flip the whole gameobject
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0, 180,0));
            if(facingDirection != FacingDirection.Left) //then we gotta rotate the gameobject, and since the camera is a child, reset its rotation manually
            {
                //cam before
                Transform camGet = this.transform.GetChild(3);
                Vector3 camPos = camGet.position;
                Quaternion camRot = camGet.rotation;

                transform.RotateAround(transform.position, transform.up, 180f);

                //cam after
                this.transform.GetChild(3).position = camPos;
                this.transform.GetChild(3).rotation = camRot;
            }
            facingDirection = FacingDirection.Left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
			Animationcontroller.SetBool("walk",true);
            // this.GetComponent<Rigidbody>().AddForce(transform.right);
            this.transform.Translate(-Vector3.right * speed * Time.deltaTime);
            //spriteRenderer.flipX = true; gotta flip the whole gameobject

            if(facingDirection != FacingDirection.Right)
            {
                Transform camGet = this.transform.GetChild(3);
                Vector3 camPos = camGet.position;
                Quaternion camRot = camGet.rotation;

                transform.RotateAround(transform.position, transform.up, -180f);
                this.transform.GetChild(3).position = camPos;
                this.transform.GetChild(3).rotation = camRot;

            }
            facingDirection = FacingDirection.Right;
        }
        
        // test
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // this.GetComponent<Rigidbody>().AddForce(-transform.right);
            this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            // this.GetComponent<Rigidbody>().AddForce(transform.right);
            this.transform.Translate(-Vector3.up * speed * Time.deltaTime);
        }
        // end of test

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
            characterCombat.Attack();
        }
        lastFacingDirection = facingDirection;
		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.Space))
        {
		Animationcontroller.SetBool("walk",false);
		}

        if (Input.GetKeyUp(KeyCode.E))
        {
            playerInteracted = true;
        }
    }
    public void TakeDamage(CharacterStats stats)
    {
        healthHandler.healthSystem.Damage(playerStats.TakeDamage(stats.damage.GetValue()));
        if (playerStats.NeedsToDie())
        {
            Destroy(gameObject);
            PlayerManager.instance.KillPlayer();
        }//skill issue
    }
}
