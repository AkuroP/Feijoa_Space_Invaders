using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Invaders_Manager : MonoBehaviour
{
    [SerializeField] private bool _isGamePlaying = true;
    
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
    

    [Space]
    [Header("Invader Shoot")]
    [SerializeField] private float _maxTimerInvaderShoot;
    private float _timerInvaderShoot = 1f;
    [SerializeField]
    private GameObject _invaderBullet;

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



    /*public float MaxTimerMoveInvader_2 { get => _maxTimerMoveInvader_2; set => _maxTimerMoveInvader_2 = value; }
    public float MaxTimerMoveInvader_3 { get => _maxTimerMoveInvader_3; set => _maxTimerMoveInvader_3 = value; }*/
    //public List<Invaders> Invader {  get => _invader; }
    [System.Serializable]
    public class InvadersWave
    {
        public List<Invaders> _invader;
        public float _timerBeforeWaveStart;
        public bool _hasWaveStarted;
        public GameObject _wave;
    }
    [Space]
    [Header("Wave")]
    [SerializeField]
    private List<InvadersWave> _invadersWave;
    [SerializeField]
    private int _currentWave;

    
    
    //Get Set

    public bool IsGamePlaying { get => _isGamePlaying; set => _isGamePlaying = value; }
    public List<InvadersWave> InvadersWaves { get => _invadersWave; set => _invadersWave = value; }

    public int CurrentWave { get => _currentWave ; set => _currentWave = value; }
    /*public List<Transform> Invader_2 {  get => _invader_2; set => _invader_2 = value; }
    public List<Transform> Invader_3 {  get => _invader_3; set => _invader_3 = value; }*/
    public Vector2 LeftVector { get => _leftVector; set => _leftVector = value; }
    public Vector2 RightVector { get => _rightVector; set => _rightVector = value; }
    public Vector2 DownRightVector { get => _downRightVector; set => _downRightVector = value; }
    public Vector2 DownLeftVector { get => _downLeftVector; set => _downLeftVector = value; }
    
    //public Vector2 MoveVector { get => _moveVector; set => _moveVector = value; }


    // Start is called before the first frame update
    void Start()
    {
        
        _isGamePlaying = true;
        _ovniSpawnTimer = _maxOvniSpawnTimer;
        _timerInvaderShoot = _maxTimerInvaderShoot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isGamePlaying) return;
        //Start wave
        if (!_invadersWave[_currentWave]._hasWaveStarted)
        {
            if (_invadersWave[_currentWave]._timerBeforeWaveStart > 0)
            {
                _invadersWave[_currentWave]._timerBeforeWaveStart -= Time.deltaTime;
                return;
            }
            else
            {
                _invadersWave[_currentWave]._wave.SetActive(true);
                _invadersWave[_currentWave]._hasWaveStarted = true;
            }

        }

        //Shoot Invader
        if (!_invadersWave[_currentWave]._hasWaveStarted) return;
        if(_timerInvaderShoot > 0)_timerInvaderShoot -= Time.deltaTime;
        else
        {
            int invaderIndex = Random.Range(0, _invadersWave[_currentWave]._invader.Count);
            Instantiate(_invaderBullet, _invadersWave[_currentWave]._invader[invaderIndex].transform.position, Quaternion.identity);
            _timerInvaderShoot = _maxTimerInvaderShoot;
        }


    }

   
}
