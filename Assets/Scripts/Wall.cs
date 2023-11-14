using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Invaders_Manager _invadersManager;
    [SerializeField] private bool _isRightWall;

    private bool hasSwitchSide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Invaders")) return;
        if (hasSwitchSide) return;
        if (_isRightWall)
        {
            _invadersManager.InvadersMoveDownLeft();
            _invadersManager.MoveVector = _invadersManager.LeftVector;
        }
        else
        {
            _invadersManager.InvadersMoveDownRight();
            _invadersManager.MoveVector = _invadersManager.RightVector;
        }
        hasSwitchSide = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (!other.CompareTag("Invaders")) return;
        hasSwitchSide = false;
    }
}
