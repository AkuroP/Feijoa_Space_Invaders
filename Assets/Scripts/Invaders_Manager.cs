using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Invaders_Manager : MonoBehaviour
{
    [SerializeField] private bool _isGamePlaying = true;
    [SerializeField] private List<Invaders> _invader;

    [Space]
    [Header("Movement")]
    [SerializeField] private float _maxTimerMoveInvader = 1f;
    private float _timerMoveInvader;
    
    /*[SerializeField] private float _maxTimerMoveInvader_2 = 1.1f;
    private float _timerMoveInvader_2;
    
    [SerializeField] private float _maxTimerMoveInvader_3 = 1.2f;
    private float _timerMoveInvader_3;*/

    /*[SerializeField] private List<Transform> _invader_2;
    [SerializeField] private List<Transform> _invader_3;*/

    [SerializeField]
    private Vector2 _leftVector = new Vector2(-0.25f, 0f);
    
    [SerializeField]
    private Vector2 _rightVector = new Vector2(0.25f, 0f);
    
    [SerializeField]
    private Vector2 _downLeftVector = new Vector2(-.25f, -.25f);
    [SerializeField]
    private Vector2 _downRightVector = new Vector2(.25f, -.25f);
    
    private Vector2 _moveVector;

    [Space]
    [Header("Ovni")]
    [SerializeField]
    private GameObject _ovni;
    [SerializeField]
    private Transform _ovniSpawnPointA;
    [SerializeField]
    private Transform _ovniSpawnPointB;
    [SerializeField, Range(5f, 15f)]
    private float _maxOvniSpawnTimer = 5f;
    private float _ovniSpawnTimer;
   
    
    //Get Set

    public bool IsGamePlaying { get => _isGamePlaying; set => _isGamePlaying = value; }
    public float MaxTimerMoveInvader { get => _maxTimerMoveInvader; set => _maxTimerMoveInvader = value; }
    /*public float MaxTimerMoveInvader_2 { get => _maxTimerMoveInvader_2; set => _maxTimerMoveInvader_2 = value; }
    public float MaxTimerMoveInvader_3 { get => _maxTimerMoveInvader_3; set => _maxTimerMoveInvader_3 = value; }*/
    public List<Invaders> Invader {  get => _invader; set => _invader = value; }
    /*public List<Transform> Invader_2 {  get => _invader_2; set => _invader_2 = value; }
    public List<Transform> Invader_3 {  get => _invader_3; set => _invader_3 = value; }*/
    public Vector2 LeftVector { get => _leftVector; set => _leftVector = value; }
    public Vector2 RightVector { get => _rightVector; set => _rightVector = value; }
    
    public Vector2 MoveVector { get => _moveVector; set => _moveVector = value; }

    // Start is called before the first frame update
    void Start()
    {
        _timerMoveInvader = _maxTimerMoveInvader;
        /*_timerMoveInvader_2 = _maxTimerMoveInvader_2;
        _timerMoveInvader_3 = _maxTimerMoveInvader_3;*/
        _moveVector = _rightVector;
        _isGamePlaying = true;
        _ovniSpawnTimer = _maxOvniSpawnTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isGamePlaying) return;
        //Move Invader 1
        if(_timerMoveInvader <= 0 ) 
        { 
            foreach(Invaders invaderTransform in _invader)
            {
                invaderTransform.transform.position += (Vector3)_moveVector;
                _timerMoveInvader = _maxTimerMoveInvader;
            }
        }
        else _timerMoveInvader -= Time.deltaTime;

        if (_ovniSpawnTimer > 0) _ovniSpawnTimer -= Time.deltaTime;
        else
        {
            int spawnPos = Random.Range(0, 2);
            switch(spawnPos)
            {
                case 0:
                    GameObject ovniA = Instantiate(_ovni, _ovniSpawnPointA.transform.position, Quaternion.identity);
                    ovniA.GetComponent<Ovni>().TargetPos = new Vector2(_ovniSpawnPointB.position.x * 1.5f, _ovniSpawnPointB.position.y);
                break; 
                case 1:
                    GameObject ovniB = Instantiate(_ovni, _ovniSpawnPointB.transform.position, Quaternion.identity);
                    ovniB.GetComponent<Ovni>().TargetPos = new Vector2(_ovniSpawnPointA.position.x * 1.5f, _ovniSpawnPointA.position.y);
                    break;
            }
            _ovniSpawnTimer = _maxOvniSpawnTimer;
        }

       /* //Move Invader 2
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
        else _timerMoveInvader_3 -= Time.deltaTime;*/


    }

    public void InvadersMoveDownLeft()
    {
        foreach (Invaders inv in _invader)inv.transform.position += (Vector3)_downLeftVector;
        
        /*foreach (Transform invaderTransform in _invader_2)invaderTransform.position += _downVector;
        
        foreach (Transform invaderTransform in _invader_3)invaderTransform.position += _downVector;*/
        
    }
    public void InvadersMoveDownRight()
    {
        foreach (Invaders inv in _invader) inv.transform.position += (Vector3)_downRightVector;
        
    }
}
