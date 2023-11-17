using System;
using Game.Script.SoundManager;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BulletWall : MonoBehaviour
{
    [SerializeField]
    private int _wallHP = 3;
    public int WallHP {  get { return _wallHP; }}
    public AudioClip _wallCrackAudio;

    [SerializeField] public GameObject hitParticles;
    public void HitWall()
    {
        _wallHP--;
       if(!GameManager.instance._inputJuiciness._input2) ServiceLocator.Get().PlaySound(_wallCrackAudio, GameManager.instance._audioMixer);

       GameObject particleInstance = Instantiate(hitParticles, transform.position, Quaternion.Euler(0,0,0));
       StartCoroutine(WaitCoroutine(particleInstance));
    }
    private Animator _animator => this.GetComponent<Animator>();

    public Animator Animator { get => _animator;}
    //public void DisableSelf() => this.gameObject.SetActive(false);

    IEnumerator WaitCoroutine(GameObject particleInstance)
    {
        if (particleInstance)
        {
            yield return new WaitForSeconds(1);
            Destroy(particleInstance);
        }

        if (_wallHP < 1)
        {
            Destroy(this);
        }
    }
}
