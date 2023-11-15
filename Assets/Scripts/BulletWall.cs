using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWall : MonoBehaviour
{
    [SerializeField]
    private int _wallHP = 3;
    public int WallHP {  get { return _wallHP; }}
    public void HitWall() => _wallHP--;
}
