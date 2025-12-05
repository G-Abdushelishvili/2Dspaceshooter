using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Health")]
    public float health;
    public float energy;
    public Slider healthBar;
    public Slider energyBar;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    [Header("Move")]
    public float speed;
    float moveX, moveY;
    public GameObject flame;
    public bool unlBst;

    
    [Header("Shoot")]
    public GameObject bullet;
    public Transform bulletPoint;
    public float bulletAmount = 20;
    public float shootCoolDown;
    bool canShoot = true; 

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;
    



   void applyHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }










    void Damage(float damageAmount)
    {
        health -= damageAmount;
        applyHearts();
        if (health <= 0)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).parent = null;
            Destroy(gameObject);
        }
        //healthBar.value = health;
    }
    void Heal(float healAmount)
    {
        if(health < 5)
        {
            health += healAmount;
            applyHearts();
        }
    }



    void PlayerMove()
    {
        moveY = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * moveX, speed * moveY);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlyingEnemy")
        {
            Destroy(collision.gameObject);
            Damage(1);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "BulletEnemy(Clone)")
        {
            Destroy(collision.gameObject);
            Damage(1);
        }
        if (collision.name == "Fireball(Clone)")
        {
            Destroy(collision.gameObject);
            Damage(2);
        }
        if (collision.name == "Ammo(Clone)")
        {
            Destroy(collision.gameObject);
            bulletAmount += Random.Range(1, 11);
        }
        if(collision.name == "Heal(Clone)")
        {
            Destroy(collision.gameObject);
            Heal(1);
        }
        if(collision.name == "UnlimEner(Clone)")
        {
            Destroy(collision.gameObject);
            StartCoroutine(energyBuff());
        }
    }



    void PlayerShoot()
    {
        StartCoroutine(ShootCoolDown());
        bulletAmount -= 1;
        anim.SetTrigger("Shoot");
        GameObject bulletClone = Instantiate(bullet, bulletPoint.position, transform.rotation);
        Destroy(bulletClone, 2);
    }



    IEnumerator ShootCoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCoolDown);
        canShoot = true;
    }

    IEnumerator energyBuff()
    {
        unlBst = true;
        energyBar.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        unlBst = !unlBst;
        energyBar.transform.GetChild(2).gameObject.SetActive(false);
    }

    void PlayerInputs()
    {
        if (Input.GetKey(KeyCode.Space) && bulletAmount > 0 && canShoot == true) 
        {
            PlayerShoot();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (energy >= 1)
            {
                CancelInvoke("boostHeal");
                flame.SetActive(true);
                speed = 15;
                if (!unlBst)
                {
                    energy -= 20 * Time.deltaTime;
                    energyBar.value = energy;
                }
            }
            else
            {
                flame.SetActive(false);
                speed = 7;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            flame.SetActive(false);
            speed = 7;
            InvokeRepeating("boostHeal" , 0 , 0.1f);
        }
    }

    void boostHeal()
    {
        if (energy <= 100)
        {
            energy += 1;
            energyBar.value = energy;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        applyHearts();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerInputs();
    }
}
