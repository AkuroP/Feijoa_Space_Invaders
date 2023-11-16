using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    [SerializeField] private float _shakeSpeed = 1f;
    [SerializeField] private float _shakeAmount = 1f;
    [SerializeField] private Vector2 _shakeBasePos => this.transform.position;

    private void FixedUpdate() => transform.position = new Vector3(_shakeBasePos.x + (Mathf.Sin(Time.time * _shakeSpeed) * _shakeAmount), _shakeBasePos.y + (Mathf.Sin(Time.time * _shakeSpeed) * _shakeAmount));
    

    private void OnDisable() => transform.position = _shakeBasePos;
    
}
