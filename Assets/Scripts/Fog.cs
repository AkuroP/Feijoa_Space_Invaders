using Game.Script.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] AudioClip _fogAudio;
    [SerializeField] GameObject _fxFog;
    [SerializeField] GameObject _fxGrain;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance._inputJuiciness._input2)ServiceLocator.Get()?.PlaySound(_fogAudio);
        if (!GameManager.instance._inputJuiciness._input1)_fxFog?.gameObject.SetActive(false);
        if (!GameManager.instance._inputJuiciness._input1)_fxGrain?.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
