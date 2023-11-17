using Game.Script.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class Heartbeat : MonoBehaviour
{
    private Vignette _vignette;
    private float minV;
    private float maxV;
    [SerializeField]
    private float speed = 1f;

    private bool switchMinMax;
    [SerializeField]
    private AudioClip _heartbeatAudio;
    // Start is called before the first frame update
    void Start()
    {
        _vignette = GameManager.instance.Vignette;
        if (!GameManager.instance._inputJuiciness._input2) ServiceLocator.Get().PlaySound(_heartbeatAudio, GameManager.instance._audioMixer);
        minV = .2f;
        maxV = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameManager.instance._inputJuiciness._input4) return;
            if (!switchMinMax)
        {
            if (_vignette.smoothness.value > minV) _vignette.smoothness.value = Mathf.Clamp(_vignette.smoothness.value - Time.deltaTime * speed, minV, maxV);
            else switchMinMax = true;
        }
        else
        {
            if (_vignette.smoothness.value < maxV)
                _vignette.smoothness.value = Mathf.Clamp(_vignette.smoothness.value + Time.deltaTime * speed, minV, maxV);
            else switchMinMax = false;
        }


    }
}
