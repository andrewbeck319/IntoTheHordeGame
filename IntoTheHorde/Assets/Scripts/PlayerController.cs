using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _isRunning = false;
    private RotationManager _rotationManager;
    // Start is called before the first frame update
    void Start()
    {
        this._rotationManager = FindObjectOfType<RotationManager>();
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
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            // this.GetComponent<Rigidbody>().AddForce(transform.right);
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
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
            this.transform.Rotate(0, 0.2f, 0);
        } else if (Input.GetKey(KeyCode.D))
        {
            // this.GetComponent<Rigidbody>().AddTorque(0, -0.5f, 0);
            this.transform.Rotate(0, -0.2f, 0);
        }

        this._rotationManager.rotationY = this.transform.rotation.y;
        this._rotationManager.rotation = this.transform.rotation;

    }
}
