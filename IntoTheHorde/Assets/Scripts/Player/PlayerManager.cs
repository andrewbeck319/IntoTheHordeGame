using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region Singleton
    public static PlayerManager instance;
    private void Start()
    {
        Debug.Log(mainCamera.name);
        Debug.Assert(mainCamera != null);
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;
    public GameObject mainCamera;
    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
