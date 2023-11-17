using Game.Script.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] AudioClip _fogAudio;
    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.Get().PlaySound(_fogAudio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
