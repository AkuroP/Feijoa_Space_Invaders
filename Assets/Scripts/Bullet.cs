using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))Destroy(this.gameObject);

        if (!other.CompareTag("Invaders")) return;
        other.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
