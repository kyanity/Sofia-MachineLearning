using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<GameObject> projectiles = new List<GameObject>();
    public GameObject projectile;
    public int projectileNumber;
    public float secondsUntilNextBomb;

    private void Start()
    {
        GameObject tempProjectile;

        for(int i = 0; i < projectileNumber; i++)
        {
            tempProjectile = Instantiate(projectile);
            tempProjectile.SetActive(false);
            projectiles.Add(tempProjectile);
        }
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        while(true)
        {
            if(GetIdleProjectile() != null)
            {
                GetIdleProjectile().transform.position = transform.position;
                Rigidbody rb = GetIdleProjectile().GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                GetIdleProjectile().SetActive(true);
                yield return new WaitForSeconds(secondsUntilNextBomb);
            }

            yield return null;
        }
    }

    private GameObject GetIdleProjectile()
    {
        for (int i = 0; i < projectileNumber; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return projectiles[i];
            }
        }

        return null;
    }
}
