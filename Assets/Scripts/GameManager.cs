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
using UnityEngine.InputSystem;
//using UnityEngine.UIElements;
using static GameManager;
using UnityEngine.UI;

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

    public Sprite _normalBG;
    public Sprite _nuitBG;
    public Image _bg;

    [System.Serializable]
    public class InputJuiciness
    {
        public bool _input1;
        public Image _inputImage1;
        public bool _input2;
        public Image _inputImage2;
        public bool _input3;
        public Image _inputImage3;
        public bool _input4;
        public Image _inputImage4;
        public bool _input5;
        public Image _inputImage5;
        public GameObject _inputGO5;
    }

    public InputJuiciness _inputJuiciness;

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
        if (GameManager.instance._inputJuiciness._input2) return;
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
        if (GameManager.instance._inputJuiciness._input2) return;
            Invaders_Manager.instance.FEARALL(false);
        Instantiate(fog, transform);
        SpawnVignette();
        _player.MaxShootCD = _player.ShootCDStacked;
        _bg.sprite = _nuitBG;
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
        _bg.sprite = _normalBG;
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
        if (!GameManager.instance._inputJuiciness._input4)
        {
        
        if (_player.PlayerHP == 1)
        {
            _heartbeat.enabled = true;
            TurnVignetteColorTo(Color.red, false);
        }
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

    public void OnInput1(InputAction.CallbackContext callback)
    {
        if(callback.performed)
        {
            if(_inputJuiciness._input1)
            {
                _inputJuiciness._input1 = false;
                _inputJuiciness._inputImage1.color = Color.white;
            }
            else
            {
                _inputJuiciness._input1 = true;
                _inputJuiciness._inputImage1.color = Color.green;
            }
        }
            
    }
    public void OnInput2(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (_inputJuiciness._input2)
            {
                _inputJuiciness._input2 = false;
                _inputJuiciness._inputImage2.color = Color.white;
            }
            else
            {
                _inputJuiciness._input2 = true;
                _inputJuiciness._inputImage2.color = Color.green;
                ServiceLocator.Get()?.StopMusic();
            }
        }

    }
    
     public void OnInput3(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (_inputJuiciness._input3)
            {
                _inputJuiciness._input3 = false;
                _inputJuiciness._inputImage3.color = Color.white;
            }
            else
            {
                _inputJuiciness._input3 = true;
                _inputJuiciness._inputImage3.color = Color.green;
            }
        }

    }
    public void OnInput4(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (_inputJuiciness._input4)
            {
                _inputJuiciness._input4 = false;
                _inputJuiciness._inputImage4.color = Color.white;
            }
            else
            {
                _inputJuiciness._input4 = true;
                _inputJuiciness._inputImage4.color = Color.green;
            }
        }

    }

    public void OnInput5(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (_inputJuiciness._input5)
            {
                _inputJuiciness._input5 = false;
                _inputJuiciness._inputImage5.color = Color.white;
                _inputJuiciness._inputGO5.SetActive(false);
            }
            else
            {
                _inputJuiciness._input5 = true;
                _inputJuiciness._inputImage5.color = Color.green;
                _inputJuiciness._inputGO5.SetActive(true);
            }
        }

    }

}
