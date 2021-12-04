using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LineRenderer))]
public class StraightHook : MonoBehaviour, IProjectile {
	[HideInInspector] public ProjectileLauncher projectileLauncher = null;

	[SerializeField] private float MaxShootLength = 10, ShootSpeed = 20, RetractSpeed = 40, zDepth = 0, HookDelay = 1, Damage = 1;
	[SerializeField] private Material chainMaterial;

	private LineRenderer ChainRenderer = null;
	private Transform hittableObject = null;
	private ITakeHit iHittableObject { get => hittableObject.GetComponent<ITakeHit>(); }
	private float hookTime = 0;
	private bool canHook = true;

	private Vector2 shootDistination = Vector2.zero, shootDirection = Vector2.zero;
	private Vector3 zDepthVector => Vector3.forward * zDepth;
	private HookState hookState;

	private void Awake()
	{
		ChainRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		Vector2 newPos;
		switch(hookState) {
			case HookState.None:
				break;

			case HookState.Shooting:
				newPos = Vector2.MoveTowards(transform.position, shootDistination, ShootSpeed * Time.deltaTime);
				if(newPos == shootDistination) {
					hookState = HookState.Retracting;
				} else {
					transform.position = (Vector3)newPos + zDepthVector;
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

			case HookState.Hooking:
				hookTime += Time.deltaTime;
				if(hookTime > HookDelay) {
					hookState = HookState.Retracting;
				}
				break;

			default:
				break;
		}
		ChainRenderer.SetPosition(0, projectileLauncher.transform.position);
		ChainRenderer.SetPosition(1, transform.position);
		if(canHook && hittableObject) {
			hittableObject.transform.position = transform.position;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(canHook && collision.gameObject.TryGetComponent(out ITakeHit iTakeHit)) {
			hittableObject = collision.transform;
			shootDistination = hittableObject.transform.position;
			transform.position = shootDistination;
			hookState = HookState.Hooking;
		}
	}

	public void Die()
	{
		Destroy(gameObject);
		projectileLauncher.canShoot = true;
	}

	public void Fire(Vector2 aim)
	{
		if(hookState == HookState.None) {
			hookState = HookState.Shooting;
			shootDirection = aim.normalized;
			shootDistination = shootDirection * MaxShootLength + (Vector2)transform.position;
		}
	}

	public void FireOff()
	{
		if(hookState == HookState.Shooting) {
			hookState = HookState.Retracting;
		}
		if(hookState == HookState.Hooking) {
			iHittableObject.Hit(shootDirection, Damage);
			canHook = false;
		}
		hittableObject = null;
	}

	public void Secondary()
	{
	}

	public void SetLauncher(ProjectileLauncher launcher)
	{
		projectileLauncher = launcher;
	}

	private enum HookState {
		None,
		Shooting,
		Hooking,
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
	}
}
