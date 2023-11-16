using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWall : MonoBehaviour
{
    [SerializeField]
    private int _wallHP = 3;
    public int WallHP {  get { return _wallHP; }}
    public void HitWall() => _wallHP--;
    private Animator _animator => this.GetComponent<Animator>();

    public Animator Animator { get => _animator;}
    public void DisableSelf() => this.gameObject.SetActive(false);
}
