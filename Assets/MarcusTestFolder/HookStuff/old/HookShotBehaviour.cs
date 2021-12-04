using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HookShotBehaviour : MonoBehaviour {
	[SerializeField] private string inputButton = "Fire1";
	[SerializeField] private float MaxShootLength = 10, ShootSpeed = 20, RetractSpeed = 40, zDepth = 0;
	[SerializeField] private HookBehaviour hookPrefab = null;

	private LineRenderer ChainRenderer = null;
	private HookBehaviour hook = null;

	private Vector2 shootDirection = Vector2.zero;
	private Vector2 mPos {
		get
		{
			return Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
	private Vector3 zDepthVector => Vector3.forward * zDepth;
	private HookState hookState = HookState.None;

	private void Start()
	{
		hook = Instantiate(hookPrefab, transform);
		hook.hookShotBehaviour = this;
	}

	private void Update()
	{
		if(Input.GetButtonDown(inputButton)) {
			Shoot();
		} else if(Input.GetButtonUp(inputButton)) {
			Retract();
		}

		HookMove();

		ChainRenderer.SetPosition(0, transform.position);
		ChainRenderer.SetPosition(1, hook.transform.position);
	}

	void HookMove()
	{
		Vector3 newPos;
		switch(hookState) {
			case HookState.None:
				break;

			case HookState.Shooting:
				newPos = (Vector3)Vector2.MoveTowards(hook.transform.position, shootDirection, ShootSpeed * Time.deltaTime) + zDepthVector;
				if(newPos == hook.transform.position) {
					hookState = HookState.Retracting;
				} else {
					hook.transform.position = newPos;
				}
				break;

			case HookState.Retracting:
				newPos = (Vector3)Vector2.MoveTowards(hook.transform.position, transform.position, RetractSpeed * Time.deltaTime) + zDepthVector;
				if(newPos == hook.transform.position) {
					hookState = HookState.None;
					shootDirection = Vector3.zero;
				} else {
					hook.transform.position = newPos;
				}
				break;

			default:
				break;
		}
	}

	public void Shoot()
	{
		if(hookState == HookState.None) {
			hookState = HookState.Shooting;
			shootDirection = mPos.normalized * MaxShootLength;
		}
	}

	public void Retract()
	{
		if(hookState == HookState.Shooting) {
			hookState = HookState.Retracting;
		}
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
		ChainRenderer.positionCount = 2;
	}
}
