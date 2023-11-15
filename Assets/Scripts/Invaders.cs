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

    [Header("Invader Movement")]
    [SerializeField] private float _maxTimerMoveInvader = 1f;
    private float _timerMoveInvader;

    private Vector2 _moveVector;


    public float MaxTimerMoveInvader { get => _maxTimerMoveInvader; set => _maxTimerMoveInvader = value; }

    // Start is called before the first frame update
    void Start()
    {
        _timerMoveInvader = _maxTimerMoveInvader;
        _moveVector = _invaderManager.RightVector;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_invaderManager.IsGamePlaying) return;
        if (_timerMoveInvader <= 0)
        {
            this.transform.position += (Vector3)_moveVector;
            _timerMoveInvader = _maxTimerMoveInvader;
        }
        else _timerMoveInvader -= Time.deltaTime;
    }

    private void OnDisable()
    {
        _invaderManager.Invader.Remove(this);
        foreach (Invaders invader in _invaderManager.Invader)invader.MaxTimerMoveInvader -= .017f;
       /* _invaderManager.MaxTimerMoveInvader_2 -= .017f;
        _invaderManager.MaxTimerMoveInvader_3 -= .017f;*/

        if(_invaderManager.Invader.Count <= 1)
        {
            _invaderManager.Invader[0].MaxTimerMoveInvader = 0f;
            /*_invaderManager.MaxTimerMoveInvader_2 = 0f;
            _invaderManager.MaxTimerMoveInvader_3 = 0f;*/
            _invaderManager.LeftVector *= 2;
            _invaderManager.RightVector *= 2;
            _invaderManager.Invader[0]._moveVector *= 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Wall")) return;
        Wall wall = other.GetComponent<Wall>();
        if (wall.IsRightWall)
        {
            this.InvadersMoveDownLeft();
            _moveVector = _invaderManager.LeftVector;
        }
        else
        {
            this.InvadersMoveDownRight();
            _moveVector = _invaderManager.RightVector;
        }

    }
        public void InvadersMoveDownLeft() => transform.position += (Vector3)_invaderManager.DownLeftVector;

        public void InvadersMoveDownRight() => transform.position += (Vector3)_invaderManager.DownRightVector;

}

