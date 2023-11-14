using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Invaders_Manager _invadersManager;
    [SerializeField] private bool _isRightWall;

    private bool hasSwitchSide;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Invaders")) return;
        if (hasSwitchSide) return;
        _invadersManager.InvadersMoveDown();
        if (_isRightWall)
        {
            _invadersManager.MoveVector = _invadersManager.LeftVector;
        }
        else
        {
            _invadersManager.MoveVector = _invadersManager.RightVector;
        }
        hasSwitchSide = true;
    }

    private void OnTriggerExit(Collider other)
    {

        if (!other.CompareTag("Invaders")) return;
        hasSwitchSide = false;
    }
}
