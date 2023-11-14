using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += Vector3.down * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall")) Destroy(this.gameObject);

        if (!other.CompareTag("Player")) return;
        other.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
