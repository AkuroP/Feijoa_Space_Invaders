using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += transform.up * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            if (!other.GetComponent<Wall>().DeleteBullet) return;
            Destroy(this.gameObject);
        }


        if (!other.CompareTag("Invaders")) return;
        DestroyObjectAndBullet(other);
    }

    private void DestroyObjectAndBullet(Collider2D other)
    {
        other.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
