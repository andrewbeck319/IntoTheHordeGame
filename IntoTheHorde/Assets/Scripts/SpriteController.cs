using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public RotationManager _rotationManager;
    // Start is called before the first frame update
    void Start()
    {
        this._rotationManager = FindObjectOfType<RotationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.rotation = new Quaternion(0f, this._rotationManager.rotationY, 0f, 0f);
        this.transform.rotation = this._rotationManager.rotation;
    }
}
