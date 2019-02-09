using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserObject;
    [SerializeField] float laserSpeed = -15f;

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(laserObject, transform.position, Quaternion.identity);
        laserObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
    }

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();

    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime; 
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        processHit(damageDealer);
    }

    private void processHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
