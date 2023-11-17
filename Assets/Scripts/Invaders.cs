using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Animations;
using Game.Script.SoundManager;
//using UnityEditorInternal;

public class Invaders : MonoBehaviour
{
    [SerializeField] private Invaders_Manager _invaderManager;

    public enum INVADER_WAVE
    {
        FIRST_WAVE,
        SECOND_WAVE,
        THIRD_WAVE
    }

    public INVADER_WAVE _invaderType;

    [Header("Invader Movement")]
    [SerializeField] private float _maxTimerMoveInvader = 1f;
    private float _timerMoveInvader;
    [SerializeField] private float _timebeforeInvaderAppear;

    private Vector2 _moveVector;
    private int _moveMuliplier = 1;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    [SerializeField] private GameObject _fogSpawn;
    [SerializeField] private float _maxTimeBeforeSpawn;
    [SerializeField] private SpriteRenderer _glowSR;
    [SerializeField] private ObjectShake _fear;
    public float _disappearSpeed = 1f;

    public Sprite _normalSprite;
    public Sprite _angrySprite;

    [SerializeField] private AudioClip _invaderDeathAudio;
    [SerializeField] private AudioClip _invaderAppearAudio;

    public ObjectShake Fear { get => _fear;}

    public float MaxTimerMoveInvader { get => _maxTimerMoveInvader; set => _maxTimerMoveInvader = value; }
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }


    // Start is called before the first frame update
    void Start()
    {
        _timerMoveInvader = _maxTimerMoveInvader + _timebeforeInvaderAppear;
        _moveVector = _invaderManager.RightVector;

        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
        _fear = this.GetComponent<ObjectShake>();
        StartCoroutine(SpawnFogBeforeInvader());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_invaderManager.IsGamePlaying) return;
        if (_timerMoveInvader <= 0)
        {
            this.transform.position += (Vector3)_moveVector * _moveMuliplier;
            _timerMoveInvader = _maxTimerMoveInvader;
        }
        else _timerMoveInvader -= Time.deltaTime;
    }

    private void OnDisable()
    {
        _invaderManager.InvadersWaves[_invaderManager.CurrentWave]._invader.Remove(this);
        foreach (Invaders invader in _invaderManager.InvadersWaves[_invaderManager.CurrentWave]._invader)invader.MaxTimerMoveInvader -= .017f;
        /* _invaderManager.MaxTimerMoveInvader_2 -= .017f;
         _invaderManager.MaxTimerMoveInvader_3 -= .017f;*/
        switch(_invaderManager.InvadersWaves[_invaderManager.CurrentWave]._invader.Count)
        {
            case 1:
                _invaderManager.InvadersWaves[_invaderManager.CurrentWave]._invader[0].MaxTimerMoveInvader = 0f;
                _invaderManager.InvadersWaves[_invaderManager.CurrentWave]._invader[0]._moveMuliplier = 2;
            break;
            case 0:
                if (_invaderManager.CurrentWave >= _invaderManager.InvadersWaves.Count) break;
                _invaderManager.CurrentWave++;
            break;
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

    private IEnumerator SpawnFogBeforeInvader()
    {
        //wait for invader to properly spawn
        float randomTime = Random.Range(0f, _maxTimeBeforeSpawn);
        _timerMoveInvader += randomTime;
        yield return new WaitForSeconds(randomTime);
        GameObject fog = Instantiate(_fogSpawn, this.transform.position, Quaternion.identity);
        //wait for fog to appear a bit
        yield return new WaitForSeconds(_timebeforeInvaderAppear);
        ServiceLocator.Get().PlaySound(_invaderAppearAudio, GameManager.instance._audioMixer);
        _spriteRenderer.enabled = true;
        _boxCollider2D.enabled = true;
        
        Destroy(fog);
    }

    public void OnDeath()
    {
        _glowSR.enabled = true;
        _glowSR.sortingOrder = 10;
        _spriteRenderer.sortingOrder = 11;
        ServiceLocator.Get().PlaySound(_invaderDeathAudio, GameManager.instance._audioMixer);
        StartCoroutine(Disappear());
    }

    public void DisableSelf() => this.gameObject.SetActive(false);

    private IEnumerator Disappear()
    {
        while(_spriteRenderer.color.a > 0)
        {
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Mathf.Clamp(_spriteRenderer.color.a - Time.deltaTime * _disappearSpeed, 0f, 255f));
            _glowSR.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Mathf.Clamp(_spriteRenderer.color.a - Time.deltaTime * _disappearSpeed, 0f, 255f));
            yield return null;
        }
        if (_spriteRenderer.color.a <= 0f) this.gameObject.SetActive(false);
    }

}

