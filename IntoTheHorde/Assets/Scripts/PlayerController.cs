using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _isRunning = false;
    private SpriteRenderer spriteRenderer;
    private WeaponController weaponController;

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
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.weaponController = FindObjectOfType<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        this._isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = this._isRunning ? 6f : 3.5f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
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
            Debug.Log(facingDirection);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
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
            Debug.Log(facingDirection);
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
            weaponController.Attack();
        }
        lastFacingDirection = facingDirection;
    }
    public void TakeDamage()
    {
        //Destroy(gameObject);
        //// May cause problems with multiple spawners
        //enemySpawning = FindObjectOfType<EnemySpawning>();
        //enemySpawning.enemyCount--;
        //if (enemySpawning.spawnTime <= 0 && enemySpawning.enemyCount <= 0)
        //{
        //    enemySpawning.spawnerDone = true;
        //}
    }
}
