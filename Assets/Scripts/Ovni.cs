using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovni : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Vector2 _targetPos;
    public Vector2 TargetPos { get => _targetPos; set => _targetPos = value; }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, _targetPos, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Wall")) return;
        Destroy(this.gameObject);
    }
}
