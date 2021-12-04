using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
	void SetLauncher(ProjectileLauncher launcher);
	void Fire(Vector2 aim);
	void FireOff();
	void Secondary();
}
