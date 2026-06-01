using UnityEngine;

public class Torres : MonoBehaviour
{
    public float range = 5f;
    public float fireRate = 1f;
    public int damage = 1;

    private float fireCountdown = 0f;
    private Transform target;

    void Update()
    {
        Debug.Log("Torre activa");
        UpdateTarget();

        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
Enemigo enemigoScript = target.GetComponent<Enemigo>();

if (enemigoScript != null)
{
    enemigoScript.RecibirDaño(damage);
}
    }
}