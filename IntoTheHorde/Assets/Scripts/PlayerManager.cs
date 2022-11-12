using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region Singleton
    public static PlayerManager instance;
    private void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;
    public GameObject mainCamera;
}
