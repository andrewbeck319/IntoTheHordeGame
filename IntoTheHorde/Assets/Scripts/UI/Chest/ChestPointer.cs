using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChestPointer : MonoBehaviour
{
    private Vector3 chestPosition;
    private Vector3 playerPosition;
    private Vector3 cameraPosition;

    private RectTransform pointerRectTransform;

    public GameObject chest;
    public GameObject player;
    public GameObject camera;

    private void Awake()
    {
        pointerRectTransform = transform.GetComponent<RectTransform>();
        camera = GameObject.Find("Player/CameraContainer/Main Camera");

        chestPosition = chest.transform.position;
        playerPosition = player.transform.position;
        cameraPosition = camera.transform.position;
       
    }
    private void Update()
    {
        chestPosition = chest.transform.position;
        playerPosition = player.transform.position;
        cameraPosition = camera.transform.position;

        cameraPosition.y = playerPosition.y;
        Vector3 originVector = (cameraPosition - playerPosition).normalized;

        chestPosition.y = playerPosition.y;
        Vector3 directionVector = (chestPosition - playerPosition).normalized;

        float angleFromCam = Vector3.SignedAngle(originVector, directionVector, Vector3.up);

        Debug.Log(angleFromCam);
        if (angleFromCam > 0.0f) angleFromCam = -angleFromCam;
        else angleFromCam = -angleFromCam;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angleFromCam);






        //Vector3 toPosition = targetPosition;
        //Vector3 fromPosition = GameObject.Find("Player/CameraContainer/Main Camera").transform.position;
        //fromPosition.z = 0f;
        //Vector3 dir = (toPosition - fromPosition).normalized;
        //dir = dir.normalized;
        //float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //if (n < 0) n += 360;
        //pointerRectTransform.localEulerAngles = new Vector3(0, 0, n);
    }
}
