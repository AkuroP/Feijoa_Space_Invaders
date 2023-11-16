using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private PlayerController _player;

    [SerializeField] private Fog fog;
    [SerializeField] private Ovni ovni;
    [SerializeField] private Transform[] ovniSpawnPoints;

    private void Awake()
    {
        if(instance != null)Destroy(instance.gameObject);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitalCoroutine());
    }

    IEnumerator InitalCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));
        
        SpawnFogAndOvni();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerRespawning();
    }

    void SpawnFogAndOvni()
    {
        SpawnFog();
        int spawnPos = Random.Range(0, 2);
        Ovni ovniInstance = Instantiate(ovni, ovniSpawnPoints[spawnPos].transform.position, Quaternion.identity);
        ovniInstance.spawnPoints = ovniSpawnPoints;
        
        if (spawnPos == 0)
        {
            ovniInstance.target = ovniSpawnPoints[1];
        }
        else
        {
            ovniInstance.target = ovniSpawnPoints[0];
        }
    }

    void SpawnFog()
    {
        Instantiate(fog, transform);
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


    public void OvniDeath()
    {
        StartCoroutine(OvniDeathCoroutine());
    }
    
    IEnumerator OvniDeathCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(10, 15));
        SpawnFog();
        yield return null;
    }

}
