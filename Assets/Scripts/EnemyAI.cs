using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField]
    private float move_speed = 2f;          // Speed of enemy movement
    private float timer = 3;
    private float bulletTime;

    [SerializeField]
    private GameObject playerTarget;        // The player target (camera)


    public GameObject EnemyBullet;       // Bullet prefab to shoot
    public float shootPower = 100f;        // Force applied to bullets
    public float shootingInterval = 1f;   // Time between shots

    private float shootTimer;               // Timer for shooting


    // Update is called once per frame
    void Update()
    {
        if(playerTarget!=null)
        {
            transform.LookAt(playerTarget.transform.position);
            transform.position += transform.forward * move_speed * Time.deltaTime;
            Shoot();
            
        }
    }
    // Shoot a bullet at the player
    void Shoot()
    {
        bulletTime -= Time.deltaTime;

        if(bulletTime > 0) return;
        bulletTime = timer;
        // GameObject BulletObj = Instantiate(EnemyBullet,transform.position+transform.forward * 0.5f,transform.rotation) as GameObject;

        // transform.LookAt(playerTarget.transform.position);
        GameObject BulletObj = Instantiate(EnemyBullet,transform.position,transform.rotation);
        BulletObj.GetComponent<Rigidbody>().AddForce(transform.forward*shootPower);

        Destroy(BulletObj,5f);
    }
}
