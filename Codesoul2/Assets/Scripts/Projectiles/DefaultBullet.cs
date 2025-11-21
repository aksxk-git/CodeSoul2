using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DefaultBullet : MonoBehaviour
{
    Player player;
    WeaponManager weapon;
    Rigidbody2D rb;
    SpriteRenderer sr;

    float bulletDamage;
    public float speed;
    public float destroyTime;

    public GameObject damageCounter;
    public Vector3 velocity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon = player.GetComponentInChildren<WeaponManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        bulletDamage = weapon.GetCurrentWeapon().damage;
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }

    private void FixedUpdate()
    {
        transform.position -= velocity * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float giveRandomDamage = Random.Range(bulletDamage, bulletDamage + 3);

            collision.gameObject.GetComponent<Enemy>().Hurt(giveRandomDamage);

            GameObject hitCounter = Instantiate(damageCounter, collision.gameObject.transform.position, Quaternion.identity);
            hitCounter.GetComponent<HitCounter>().damage = giveRandomDamage;

            Destroy(gameObject);
        }
    }
}
