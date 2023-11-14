using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    [SerializeField] private Invaders_Manager _invaderManager;

    public enum INVADER_TYPE
    {
        FIRST_TYPE,
        SECOND_TYPE,
        THIRD_TYPE
    }

    public INVADER_TYPE _invaderType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        /*switch(_invaderType)
        {
            case INVADER_TYPE.FIRST_TYPE:
                _invaderManager.Invader.Remove(this.transform);
            break;
            case INVADER_TYPE.SECOND_TYPE:
                _invaderManager.Invader_2.Remove(this.transform);
            break;
            case INVADER_TYPE.THIRD_TYPE:
                _invaderManager.Invader_3.Remove(this.transform);
            break;
        }*/

        _invaderManager.Invader.Remove(this);

        _invaderManager.MaxTimerMoveInvader -= .017f;
       /* _invaderManager.MaxTimerMoveInvader_2 -= .017f;
        _invaderManager.MaxTimerMoveInvader_3 -= .017f;*/

        if(_invaderManager.Invader.Count /*+ _invaderManager.Invader_2.Count + _invaderManager.Invader_3.Count*/ <= 1)
        {
            _invaderManager.MaxTimerMoveInvader = 0f;
            /*_invaderManager.MaxTimerMoveInvader_2 = 0f;
            _invaderManager.MaxTimerMoveInvader_3 = 0f;*/
            _invaderManager.LeftVector *= 2;
            _invaderManager.RightVector *= 2;
            _invaderManager.MoveVector *= 2;
        }
    }
}
