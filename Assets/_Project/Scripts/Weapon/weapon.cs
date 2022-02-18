using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject shooter;
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    public void Shoot()
    {
        if(firePoint != null && shooter != null)
        {
            GameObject bullet = ObjectPool.instance.GetPooledObject();

            if(bullet != null)
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
            }
          

            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if(shooter.transform.localScale.x < 0)
            {
                bulletComponent.direction = Vector2.left;
            }
            else
            {
                bulletComponent.direction = Vector2.right;
            }
        }
    }

    public void ShootEnemy()
    {
        if (firePoint != null && shooter != null)
        {
            GameObject bullet = Instantiate(bulletPrefab);

            if (bullet != null)
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
            }


            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (shooter.transform.localScale.x < 0)
            {
                bulletComponent.direction = Vector2.left;
            }
            else
            {
                bulletComponent.direction = Vector2.right;
            }
        }
    }
}
