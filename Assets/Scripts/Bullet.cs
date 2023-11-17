using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private Fog miniFog;
    [SerializeField] private Material baseMat;
    [SerializeField] private Material emissiveMat;
    private float raycastDistance = 0.05f;

    private void Start()
    {
        if (GameManager.instance._inputJuiciness._input3) this.GetComponentInChildren<TrailRenderer>().gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += transform.up * Time.deltaTime * _speed;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastDistance);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                if (!hit.collider.gameObject.GetComponent<Wall>().DeleteBullet) return;
                if(!GameManager.instance._inputJuiciness._input1) Instantiate(miniFog, hit.point, Quaternion.Euler(0, 0, 0));
            }
        }
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

        if (other.CompareTag("Fog"))
        {
           if(!GameManager.instance._inputJuiciness._input1)GetComponent<SpriteRenderer>().material = emissiveMat;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fog"))
        {
            if (!GameManager.instance._inputJuiciness._input1) GetComponent<SpriteRenderer>().material = baseMat;
        }
    }

    private void DestroyObjectAndBullet(Collider2D other)
    {

        other.GetComponent<Invaders>().OnDeath();
        Destroy(this.gameObject);
    }
}
