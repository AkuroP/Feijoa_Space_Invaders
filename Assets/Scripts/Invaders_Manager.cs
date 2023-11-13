using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Invaders_Manager : MonoBehaviour
{
    [SerializeField] private float _maxTimerMoveInvader_1 = 1f;
    private float _timerMoveInvader_1;
    
    [SerializeField] private float _maxTimerMoveInvader_2 = 1.1f;
    private float _timerMoveInvader_2;
    
    [SerializeField] private float _maxTimerMoveInvader_3 = 1.2f;
    private float _timerMoveInvader_3;

    [SerializeField] private List<Transform> _invader_1;
    [SerializeField] private List<Transform> _invader_2;
    [SerializeField] private List<Transform> _invader_3;

    [Space]
    [SerializeField]
    private Vector3 _leftVector = new Vector3(-0.25f, 0f, 0f);
    
    [SerializeField]
    private Vector3 _rightVector = new Vector3(0.25f, 0f, 0f);
    
    [SerializeField]
    private Vector3 _downVector = new Vector3(0f, 0f, -.25f);
    
    private Vector3 _moveVector;
    private Vector3 _nextMove;
   
    
    //Get Set
    public float MaxTimerMoveInvader_1 { get => _maxTimerMoveInvader_1; set => _maxTimerMoveInvader_1 = value; }
    public float MaxTimerMoveInvader_2 { get => _maxTimerMoveInvader_2; set => _maxTimerMoveInvader_2 = value; }
    public float MaxTimerMoveInvader_3 { get => _maxTimerMoveInvader_3; set => _maxTimerMoveInvader_3 = value; }
    public List<Transform> Invader_1 {  get => _invader_1; set => _invader_1 = value; }
    public List<Transform> Invader_2 {  get => _invader_2; set => _invader_2 = value; }
    public List<Transform> Invader_3 {  get => _invader_3; set => _invader_3 = value; }
    public Vector3 LeftVector { get => _leftVector; set => _leftVector = value; }
    public Vector3 RightVector { get => _rightVector; set => _rightVector = value; }
    
    public Vector3 MoveVector { get => _moveVector; set => _moveVector = value; }

    // Start is called before the first frame update
    void Start()
    {
        _timerMoveInvader_1 = _maxTimerMoveInvader_1;
        _timerMoveInvader_2 = _maxTimerMoveInvader_2;
        _timerMoveInvader_3 = _maxTimerMoveInvader_3;
        _moveVector = _rightVector;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Move Invader 1
        if(_timerMoveInvader_1 <= 0 ) 
        { 
            foreach( Transform invaderTransform in _invader_1)
            {
                invaderTransform.position += _moveVector;
                _timerMoveInvader_1 = _maxTimerMoveInvader_1;
            }
        }
        else _timerMoveInvader_1 -= Time.deltaTime;

        //Move Invader 2
        if (_timerMoveInvader_2 <= 0)
        {
            foreach (Transform invaderTransform in _invader_2)
            {
                invaderTransform.position += _moveVector;
                _timerMoveInvader_2 = _maxTimerMoveInvader_2;
            }
        }
        else _timerMoveInvader_2 -= Time.deltaTime;

        //Move Invader 3
        if (_timerMoveInvader_3 <= 0)
        {
            foreach (Transform invaderTransform in _invader_3)
            {
                invaderTransform.position += _moveVector;
                _timerMoveInvader_3 = _maxTimerMoveInvader_3;
            }
        }
        else _timerMoveInvader_3 -= Time.deltaTime;


    }

    public void InvadersMoveDown()
    {
        foreach (Transform invaderTransform in _invader_1)invaderTransform.position += _downVector;
        
        foreach (Transform invaderTransform in _invader_2)invaderTransform.position += _downVector;
        
        foreach (Transform invaderTransform in _invader_3)invaderTransform.position += _downVector;
        
    }
}
