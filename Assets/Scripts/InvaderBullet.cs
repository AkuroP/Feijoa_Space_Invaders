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

        if (other.name == "BulletWall")
        {
            BulletWall bWall = other.GetComponent<BulletWall>();
            bWall.HitWall();
            if (bWall.WallHP <= 0) DestroyObjectAndBullet(other);
            else Destroy(this.gameObject);
            return;
        }

        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().TakeDamage();
        DestroyObjectAndBullet(other);
    }

    private void DestroyObjectAndBullet(Collider2D other)
    {
        other.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
