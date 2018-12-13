using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2.0f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = -15f;
    [SerializeField] GameObject destroyVFXPrefab;
    [SerializeField] AudioClip enemyClipPrefab;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.4f;
    [SerializeField] AudioClip enemyShootPrefab;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.4f;

    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);	
	}
	
	// Update is called once per frame
	void Update () {

        CountDownAndShot();

	}

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(enemyShootPrefab, Camera.main.transform.position, shootSoundVolume);
    }
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject boomVFX = Instantiate(destroyVFXPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(boomVFX, 1f);
        AudioSource.PlayClipAtPoint(enemyClipPrefab, Camera.main.transform.position, deathSoundVolume);
    }
    

}
