using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InvaderBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;

    private void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _trailRenderer = this.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += Vector3.down * Time.deltaTime * _speed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "BulletWall")
        {
            BulletWall bWall = other.GetComponent<BulletWall>();
            bWall.HitWall();
            if (bWall.WallHP <= 0)
            {
                bWall.Animator.SetBool("Destroy", true);
                Destroy(this.gameObject);
            }
            else
            {
                bWall.Animator.SetTrigger("Hit");
                Destroy(this.gameObject);
            }
            return;
        }
        if (other.CompareTag("Wall"))
        {
            Wall wall = other.GetComponent<Wall>();
            if(wall.DeleteBullet) Destroy(this.gameObject);
            if (wall.ChangeBulletColor)
            {
                _spriteRenderer.color = wall.NewColor;
                _trailRenderer.startColor = wall.NewColor;
            }

        }



        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().TakeDamage();
        other.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

}
