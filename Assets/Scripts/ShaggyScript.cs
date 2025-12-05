using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShaggyScript : MonoBehaviour
{
    public int age;
    public string characterName = "Shaggy";
    public bool isAlive;
    public float speed = 5f;
    public float upJump = 10f;
    public float health;
    public Text healthText;
    public GameObject gameOver;
    public SpriteRenderer Sprite;
    public GameObject bulletSS;
    public float bulletForce;


    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletScSn = Instantiate(bulletSS, transform.position, transform.rotation);
            bulletScSn.GetComponent<Rigidbody2D>().AddForce(bulletForce * transform.right, ForceMode2D.Impulse);
            Destroy(bulletScSn, 2);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + health;

        if (isAlive == true)
        {
            print(characterName);
        }
        else
        {
            //print("Dead");
        }
    }
    void Shaggymove()
    {
        // Move right when D is pressed
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            //Sprite.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Move left when A is pressed
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            //Sprite.flipX = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // Move up when W is pressed
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, upJump * Time.deltaTime, 0);
        }

        // Move down when S is pressed
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }
    }
    void Damage(float dammageAmount)
    {
        health -= dammageAmount;
        healthText.text = "Health: " + health;
        if (health <= 0)
        {
            gameOver.SetActive(true);
            Destroy(gameObject);
            print("Shaggy Died");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shaggymove();
        Shoot();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy+")
        {
            Damage(2);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject); 
            print("It hurts, Scooby!");
            Damage(1);
        }
        if (collision.gameObject.tag == "Traps")
        {
            Damage(health);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Traps")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
