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
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(5, 15));
        
        SpawnFogAndOvni();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerRespawning();
    }

    void SpawnFogAndOvni()
    {
        Instantiate(fog, transform);
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
