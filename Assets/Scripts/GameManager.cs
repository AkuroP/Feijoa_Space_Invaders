using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private PlayerController _player;

    private void Awake()
    {
        if(instance != null)Destroy(instance.gameObject);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerRespawning();
    }

    private void PlayerRespawning()
    {
        if (_player.Respawning)
        {
            if (_player.RespawnCD > 0) _player.RespawnCD -= Time.deltaTime;
            else
            {
                _player.gameObject.SetActive(true);
                _player.Respawning = false;
            }

        }
    }

}
