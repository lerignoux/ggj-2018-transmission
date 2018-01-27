﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using Pathfinding.Examples;

[RequireComponent(typeof(AudioSource))]
public class PartyGuy : MonoBehaviour
{


	public float hitVelocityToDie;
	public float speed;
	public float rotationSpeed;
	public float stoppingDistance;
	public Blood bloodPrefab;

	private AudioSource audioSource;
	public AudioClip dieSound;
	public float volume= 1.0f;

	float realSpeed;

	private Rigidbody rb;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 dirToPlayer = PlayerController.instance.transform.position - transform.position;
		dirToPlayer.y = 0;

		float d = dirToPlayer.sqrMagnitude;
		if (d < stoppingDistance * stoppingDistance) {
			realSpeed = 0;
		} else if (d > stoppingDistance * stoppingDistance * 1.1f) {
			realSpeed = speed;
		}

		rb.velocity = realSpeed * dirToPlayer.normalized;
		transform.rotation = (Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (dirToPlayer), rotationSpeed)); 
	}

	void OnCollisionEnter (Collision collisionInfo)
	{
		if (collisionInfo.collider.GetComponent<PartyGuy> ()) {
			return;
		}

		CopCar cop = collisionInfo.collider.GetComponent<CopCar> ();

		if (cop) {
			if (cop.agent.velocity.sqrMagnitude > hitVelocityToDie * hitVelocityToDie) {
				Die ();
			}
			return;
		}

		if (collisionInfo.relativeVelocity.sqrMagnitude > hitVelocityToDie * hitVelocityToDie) {
			Die ();
		} else {
			audioSource.Play();
		}
	}

	void Die ()
	{
		Instantiate (bloodPrefab, transform.position, transform.rotation);
		audioSource.PlayOneShot(dieSound, volume);
		Destroy (gameObject);
	}
}
