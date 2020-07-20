using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{

	public Transform firePoint;
	public GameObject ArrowPrefab;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}

	void Shoot()
	{
		Instantiate(ArrowPrefab, firePoint.position, Quaternion.identity);
	}
}