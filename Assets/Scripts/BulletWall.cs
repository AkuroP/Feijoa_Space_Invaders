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
    public void HitWall()
    {
        _wallHP--;
       ServiceLocator.Get().PlaySound(_wallCrackAudio, GameManager.instance._audioMixer);
    }
    private Animator _animator => this.GetComponent<Animator>();

    public Animator Animator { get => _animator;}
    public void DisableSelf() => this.gameObject.SetActive(false);
}
