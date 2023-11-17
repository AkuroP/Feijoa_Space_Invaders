using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Heartbeat : MonoBehaviour
{
    private Vignette _vignette;
    private float minV;
    private float maxV;
    [SerializeField]
    private float speed = 1f;

    private bool switchMinMax;
    // Start is called before the first frame update
    void Start()
    {
        _vignette = GameManager.instance.Vignette;
        minV = .2f;
        maxV = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
