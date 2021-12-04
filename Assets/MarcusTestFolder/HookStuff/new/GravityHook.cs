using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class GravityHook : MonoBehaviour, IProjectile {
	[HideInInspector] public ProjectileLauncher projectileLauncher = null;

	[SerializeField] private float MaxShootLength = 10, FireVelocity = 20, RetractSpeed = 40, zDepth = 0;
	[SerializeField] private Material chainMaterial;

	private LineRenderer ChainRenderer = null;

	private new Rigidbody2D rigidbody2D = null;
	private Hookable hookedObject = null;

	private Vector2 shootDistination = Vector2.zero;
	private Vector3 zDepthVector => Vector3.forward * zDepth;
	private HookState hookState;

	private void Awake()
	{
		ChainRenderer = GetComponent<LineRenderer>();
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Vector2 newPos;
		switch(hookState) {
			case HookState.None:
				break;

			case HookState.Shooting:
				if(MaxShootLength <= (transform.position - projectileLauncher.transform.position).magnitude) {
					hookState = HookState.Retracting;
				}
				break;

			case HookState.Retracting:
				newPos = Vector2.MoveTowards(transform.position, projectileLauncher.transform.position, RetractSpeed * Time.deltaTime);
				if(newPos == (Vector2)projectileLauncher.transform.position) {
					Die();
				} else {
					transform.position = (Vector3)newPos + zDepthVector;
				}
				break;

			default:
				break;
		}
		ChainRenderer.SetPosition(0, projectileLauncher.transform.position);
		ChainRenderer.SetPosition(1, transform.position);
		if(hookedObject) {
			hookedObject.transform.position = transform.position;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.TryGetComponent(out Hookable hookable)) {
			hookedObject = hookable;
			Retract();
		}
	}

	public void Die()
	{
		Destroy(gameObject);
		projectileLauncher.canShoot = true;
	}

	public void Retract()
	{
		if(hookState == HookState.Shooting) {
			hookState = HookState.Retracting;
		}
	}

	public void Fire(Vector2 aim)
	{
		if(hookState == HookState.None) {
			hookState = HookState.Shooting;
			shootDistination = aim.normalized * MaxShootLength + (Vector2)transform.position;
			rigidbody2D.AddForce(aim.normalized * FireVelocity, ForceMode2D.Impulse);
		}
	}

	public void SetLauncher(ProjectileLauncher launcher)
	{
		projectileLauncher = launcher;
	}

	private enum HookState {
		None,
		Shooting,
		Retracting,
	}

	private void OnValidate()
	{
		if(!TryGetComponent(out ChainRenderer)) {
			ChainRenderer = gameObject.AddComponent<LineRenderer>();
		}
		ChainRenderer.hideFlags = HideFlags.HideInInspector;
		ChainRenderer.positionCount = 2;
		ChainRenderer.textureMode = LineTextureMode.Tile;
		ChainRenderer.material = chainMaterial;

		if(!TryGetComponent(out rigidbody2D)) {
			rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
		}
	}

	public void FireOff()
	{
		throw new System.NotImplementedException();
	}

	public void Secondary()
	{
		throw new System.NotImplementedException();
	}
}
