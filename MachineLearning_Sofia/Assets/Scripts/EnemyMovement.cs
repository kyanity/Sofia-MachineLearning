using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement: MonoBehaviour
{
    private Vector3 point;
    public float distance;
    public float speed;

    private void Awake()
    {
        point = new Vector3(Random.Range(-3f, 3f), transform.localPosition.y, transform.localPosition.z);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(Vector3.Distance(transform.localPosition, point) < .1f)
            {
                point = new Vector3(Random.Range(-3f, 3f), transform.localPosition.y, transform.localPosition.z);
            }

        transform.localPosition = Vector3.Lerp(transform.localPosition, point, speed * Time.deltaTime);
    }
}
