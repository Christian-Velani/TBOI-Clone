using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    GameObject player;
    public int life = 6;
    public GameObject bulletPrefab;
    public float fireRate = 1.0f;
    public float bulletSpeed = 10.0f;
    private float nextFireTime = 0.0f;
    public Vector3 bulletOffset = new Vector3(0, 0, 0);
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        life = 6;
        nextFireTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (life < 0)
        {
            Destroy(this);
            SceneManager.LoadScene(5);
        }

        if (Time.time > nextFireTime)
        {
            Attack();

        }

    }

    void Attack()
    {
        Vector3 direction = Random.insideUnitCircle.normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity);
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<BulletController>().isEnemyBullet = true;
        bullet.GetComponent<BulletController>().GetPlayer(player.transform);

        nextFireTime = Time.time + fireRate;
    }
}
