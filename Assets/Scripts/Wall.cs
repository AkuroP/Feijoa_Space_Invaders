using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Invaders_Manager _invadersManager;
    [SerializeField] private bool _isRightWall;
    public bool IsRightWall { get => _isRightWall; }


}
