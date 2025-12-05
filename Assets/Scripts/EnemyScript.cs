using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Shoot")]
    [Space(10)]
    public float health;
    public GameObject bullet;
    public Transform bulletPoint;
    public Transform bulletTarget;
    public float bulletForce;

    [Header("Components")]
    [Space(10)]
    public Animator anim;

    [Header("Drop")]
    public GameObject[] droppObject;
    public GameObject explosion;




    void DropObjects()
    {
      
        float dropChance = Random.Range(0, 101);
        if (dropChance <= 30 && dropChance >= 10)
        {
            GameObject ammoClone = Instantiate(droppObject[0], transform.position, transform.rotation);
            Destroy(ammoClone, 3);
        }
        else if (dropChance <= 10)
        {
            GameObject healClone = Instantiate(droppObject[1], transform.position, transform.rotation);
            Destroy(healClone, 2);
        }
        else if (dropChance <= 40 && dropChance >= 30)
        {
            GameObject unlbstClone = Instantiate(droppObject[2], transform.position, transform.rotation);
            Destroy(unlbstClone, 1.5f);
        }
    }

    void Explode()
    {
        GameObject explosionClone = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explosionClone, 0.7f);

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Explode();
        }
    }




    void SpawnBullet()
    {
        bulletTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 directionToPlayer = bulletTarget.position - transform.position;
        directionToPlayer.Normalize();
        if (gameObject.name == "Enemy(Clone)")
        {
            anim.SetTrigger("Shoot");
        }
        GameObject bulletClone = Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        bulletClone.GetComponent<Rigidbody2D>().AddForce(directionToPlayer * bulletForce, ForceMode2D.Impulse);
        Destroy(bulletClone, 3);
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBullet", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletPlayer")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            DropObjects();
            Explode();
        }
    }
}
