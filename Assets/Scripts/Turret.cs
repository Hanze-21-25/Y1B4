using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{

	private Transform target;

	[Header("Attributes")]
	//Range of the turret can be defined
	public float range = 15f;
	public float fireRate = 1f;
	private float fireCountdown = 0f;
	public bool sht = false;


	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";


	//partToRotate and firePoint are in the prefab of every turret model
	public Transform partToRotate;
	public float turnSpeed = 10f;

	public GameObject bulletPrefab;
	public Transform firePoint;
	


	// Use this for initialization
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		//Makes turret focus on the closest enemy

		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
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

	//Locks the turret on the closest target
	void Update()
	{
		if (target == null)
			return;

		//Target lock on
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCountdown <= 0f)
        {
			Shoot();
			fireCountdown = 1f / fireRate;
			GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			Bullet bullet = bulletGO.GetComponent<Bullet>();
			FindObjectOfType<AudioManager>().Play(bullet.Csound);


		}
		fireCountdown -= Time.deltaTime;

	}

	void Shoot ()
    {
		sht = true;
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
			bullet.Seek(target);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}