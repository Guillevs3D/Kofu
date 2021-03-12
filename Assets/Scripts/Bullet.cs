using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Variable Global Publica
    public float speedBullet = 3;
    public float damgeBullet = 20;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }
    public void BulletMove()
    {
        float xMove;
        xMove = speedBullet * Time.deltaTime;
        transform.Translate(Vector2.right * xMove);
    }

    private void OnCollisionEnter2D(Collision2D colBullet)
    {
        Destroy(gameObject);
        Instantiate(explosion,transform.position, transform.rotation);
        if(colBullet.gameObject.tag == "Enemy")
        {
            colBullet.gameObject.GetComponent<Enemy>().healt-=damgeBullet;
        }
        if(colBullet.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
}
