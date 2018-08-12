using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour 
{

	[SerializeField] private GameObject bladeTrailPrefab;

	[SerializeField] private GameObject fruitPrefab;

	[SerializeField] private Transform centre;

	private GameObject currentBladeTrail;
	[SerializeField] private bool isCutting = false;

	[SerializeField] private Rigidbody2D rb;

	[SerializeField] private Camera cam;

	private CircleCollider2D circleCollider;

	private Vector2 previousPosition;

	[SerializeField] private float minCuttingVelocity = .001f;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		circleCollider = GetComponent<CircleCollider2D>();
		//circleCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if( Input.GetMouseButtonDown( 1 ) )
		{
			Instantiate( fruitPrefab, centre );
		}
		
		if( Input.GetMouseButtonDown( 0 ) )
		{
			StartCutting();
		}
		else if( Input.GetMouseButtonUp( 0 ) )
		{
			StopCutting();
		}

		if( isCutting )
		{
			UpdateBladeCutPosition();
		}
	}


	private void UpdateBladeCutPosition()
	{
		
		Vector2 newPosition = cam.ScreenToWorldPoint( Input.mousePosition );
		rb.position = newPosition;

		float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime; 
		if( velocity > minCuttingVelocity )
		{
			circleCollider.enabled = true;
		}
		else
		{
			circleCollider.enabled = false;
		}

		previousPosition = newPosition;
	}

	private void StartCutting()
	{
		isCutting = true;
		currentBladeTrail = Instantiate( bladeTrailPrefab, transform );
		previousPosition = cam.ScreenToWorldPoint( Input.mousePosition );
		circleCollider.enabled = false;
	}

	private void StopCutting()
	{
		isCutting = false;
		currentBladeTrail.transform.SetParent( null );
		Destroy( currentBladeTrail , 0.5f );
		circleCollider.enabled = false;
		

		
		
		
	}
}
