using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {
	[SerializeField] private string primaryButton = "Fire1", secondaryButton = "Fire2";
	[SerializeField] private float zDepth = 0;
	[SerializeField] private Transform projectilePrefab = null;
	[SerializeField] public bool canShoot = true;

	private Transform projectile = null;
	private IProjectile iprojectile = null;
	private Vector2 mPos {
		get
		{
			return Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
		}
	}

	private Vector3 zDepthVector => Vector3.forward * zDepth;

	private void Update()
	{
		if(Input.GetButtonDown(primaryButton) && canShoot) {
			canShoot = false;
			projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			iprojectile = projectile.GetComponent<IProjectile>();
			iprojectile.SetLauncher(this);
			iprojectile.Fire(mPos);
		}
		if(Input.GetButtonUp(primaryButton)) {
			iprojectile?.FireOff();
		}
		if(Input.GetButtonDown(secondaryButton)) {
			iprojectile?.Secondary();
		}
	}
}
