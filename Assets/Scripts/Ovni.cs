using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ovni : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    // private Vector2 _targetPos;
    // public Vector2 TargetPos { get => _targetPos; set => _targetPos = value; }

    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public Transform target;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        
        if (AreReallyNear(transform, target))
        {
            if (target == spawnPoints[0])
            {
                target = spawnPoints[1];
            }
            else
            {
                target = spawnPoints[0];
            }
        }
    }

    private bool AreReallyNear(Transform a, Transform b)
    {
        if (Math.Abs(a.position.x - b.position.x) < 0.1 && Math.Abs(a.position.y - b.position.y) < 0.1)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Fog[] fogs = FindObjectsOfType<Fog>();
            if (fogs.Length > 0)
            {
                foreach (Fog fog in fogs)
                {
                    Destroy(fog.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.OvniDeath();
        Invaders_Manager.instance.FEARALL(true);
    }
}
