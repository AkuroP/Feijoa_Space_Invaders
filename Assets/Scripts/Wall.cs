using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Invaders_Manager _invadersManager;
    [SerializeField] private bool _isRightWall;
    [SerializeField] private bool _deleteBullet = true;

    [SerializeField] private bool _changeBulletColor;
    [SerializeField] private Color _newColor;

    public bool IsRightWall { get => _isRightWall; }
    public bool ChangeBulletColor { get => _changeBulletColor; }
    public Color NewColor { get => _newColor; }
    public bool DeleteBullet { get => _deleteBullet; }

}
