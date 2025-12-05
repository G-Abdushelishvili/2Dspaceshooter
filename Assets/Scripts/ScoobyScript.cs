using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoobyScript : MonoBehaviour
{
    public float health;

    void Damage(float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BulletScoobySnack(Clone)")
        {
            Damage(1);
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 

    }
}
