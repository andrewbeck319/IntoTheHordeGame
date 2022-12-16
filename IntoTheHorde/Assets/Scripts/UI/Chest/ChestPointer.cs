using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPointer : MonoBehaviour
{
    private Vector3 chestPosition;
    private Vector3 playerPosition;
    private Vector3 cameraPosition;

    private RectTransform pointerRectTransform;

    public GameObject chest;
    public GameObject player;
    public GameObject camera;

    [SerializeField] public float destroyChestTimer = 30.0f;

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
        if (chest != null)
        {
            StartCoroutine(ChestDestroyTimer(destroyChestTimer));
            chestPosition = chest.transform.position;
            playerPosition = player.transform.position;
            cameraPosition = camera.transform.position;

            cameraPosition.y = playerPosition.y;
            Vector3 originVector = (cameraPosition - playerPosition).normalized;

            chestPosition.y = playerPosition.y;
            Vector3 directionVector = (chestPosition - playerPosition).normalized;

            //could flip the positionss of both vectors so we can avoid the -angleFromCam
            float angleFromCam = Vector3.SignedAngle(originVector, directionVector, Vector3.up);

            pointerRectTransform.localEulerAngles = new Vector3(0, 0, -angleFromCam);
        }

        //Vector3 toPosition = targetPosition;
        //Vector3 fromPosition = GameObject.Find("Player/CameraContainer/Main Camera").transform.position;
        //fromPosition.z = 0f;
        //Vector3 dir = (toPosition - fromPosition).normalized;
        //dir = dir.normalized;
        //float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //if (n < 0) n += 360;
        //pointerRectTransform.localEulerAngles = new Vector3(0, 0, n);
    }
    IEnumerator ChestDestroyTimer(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(chest);
        Destroy(this.transform.gameObject);
    }
}
