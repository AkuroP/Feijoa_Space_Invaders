using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEditor.Rendering;
using Game.Script.SoundManager;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private PlayerController _player;

    [SerializeField] private Fog fog;
    [SerializeField] private Ovni ovni;
    [SerializeField] private Transform[] ovniSpawnPoints;

    [SerializeField] private VolumeProfile _volumeProfile;
    [SerializeField] private float _vignetteSpeed;
    [SerializeField]
    [Range(0f, 1f)]
    private float _vignetteIntensity;
    private Vignette _vignette;
    private bool _vignetteEnabled = true;
    private Color _vignetteOldColor;
    [SerializeField]
    private Color _startingColor;

    public Vignette Vignette { get => _vignette; }

    public Heartbeat _heartbeat;
    [SerializeField] private AudioClip _music;
    public AudioMixerGroup _audioMixer;
    private void Awake()
    {
        if(instance != null)Destroy(instance.gameObject);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _volumeProfile.TryGet(out _vignette);
        _vignette.color.value = _startingColor;
        _vignette?.intensity.Override(0f);
        _heartbeat = this.GetComponent<Heartbeat>();
        StartCoroutine(InitalCoroutine());
        ServiceLocator.Get().PlayMusic(_music, _audioMixer);
    }

    IEnumerator InitalCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));
        
        SpawnFogAndOvni();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimVignette();
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
        Invaders_Manager.instance.FEARALL(false);
        Instantiate(fog, transform);
        SpawnVignette();
        _player.MaxShootCD = _player.ShootCDStacked;
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
        DeactivateVignette();
        _player.MaxShootCD = _player.ShootCDBoosted;
        StartCoroutine(OvniDeathCoroutine());
    }
    
    IEnumerator OvniDeathCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(10, 15));
        SpawnFog();
        yield return null;
    }

    private void AnimVignette()
    {
        if (_vignetteEnabled) return;
        if(_vignette?.intensity.value < _vignetteIntensity)
        {
            _vignette?.intensity.Override(Mathf.Clamp(_vignette.intensity.value + Time.deltaTime * _vignetteSpeed, 0f, _vignetteIntensity));
        }
        else _vignetteEnabled = true;
    }

    private void SpawnVignette()
    {
        _vignette.intensity.value = 0f;
        _vignetteEnabled = false;
        _vignetteOldColor = _vignette.color.value;
        if (_player.PlayerHP == 1)
        {
            _heartbeat.enabled = true;
            TurnVignetteColorTo(Color.red, false);
        }
    }

    public void TurnVignetteColorTo(Color color, bool animTurnVignette)
    {
        if (animTurnVignette) StartCoroutine(SmoothChangeColor(color, 1f));
        else _vignette.color.value = color;

    }

    private IEnumerator SmoothChangeColor(Color color, float speed)
    {
        while(_vignette.color.value != color)
        {
            _vignette.color.value = Color.Lerp(_vignette.color.value, color, Time.deltaTime * speed);
            yield return null;
        }
    }

    private void DeactivateVignette() => _vignette.intensity.value = 0f;

}
