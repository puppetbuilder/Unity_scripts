﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinTerrain : MonoBehaviour {

	private Mesh myMesh = null; 

	public float seed = 1000;
	public float waveSpeed = 0.004f;

	public float detailScale = 3.2f;
	public float detailScale2 = 25f;

	public float amplitude = 8f;
	public float amplitude2 = 15f;

	private bool perlinMoving = false;

	private MeshCollider myCollider = null;

	private Vector3[] vertices;

	void Start () {
		//getComponents ();
		generateTerrain ();
	}

	void getComponents(){
		// Get hold of the mesh collider.
		myCollider = this.GetComponent<MeshCollider> ();
		// Get hold of the 3D mesh via the MeshFilter.
		myMesh = this.GetComponent<MeshFilter> ().mesh;
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Space))
			perlinMoving = !perlinMoving;
		//if (Time.frameCount % 4 == 0)

		if (perlinMoving)
			generateTerrain ();
	}



	public void generateTerrain(){

		if (myMesh == null)
			getComponents ();

		seed+= waveSpeed;

		vertices = myMesh.vertices;

		float pX = 0;
		float pZ = 0;

		float pX2 = 0;
		float pZ2 = 0;

		for (int i = 0; i < vertices.Length; i++) {
			//vertices[i].y = Random.Range (0,3);

			pX = pX2 = (1000000 + this.transform.position.x + vertices [i].x * this.transform.lossyScale.x);
			pZ = pZ2 = (1000000 + this.transform.position.z + vertices [i].z * this.transform.lossyScale.z);

			pX = pX /detailScale + seed;
			pX2 = pX2 /detailScale2 + seed;
			pZ = pZ /detailScale;
			pZ2 = pZ2 /detailScale2;

//			pX2 = (1000000 + this.transform.position.x + vertices [i].x * this.transform.lossyScale.x)/detailScale2 + seed;
//			pZ2 = (1000000 + this.transform.position.z + vertices [i].z * this.transform.lossyScale.z)/detailScale2;
//

			vertices[i].y = amplitude/2f + Mathf.PerlinNoise(pX, pZ) * amplitude;

			vertices [i].y += Mathf.PerlinNoise (pX2, pZ2) * amplitude2;

			//Debug.Log ("Y = " + vertices [i].y);
		}

		// Update the vertices, and then the collider.
		myMesh.vertices = vertices;
		myMesh.RecalculateBounds ();
		myMesh.RecalculateNormals ();
		myCollider.sharedMesh = null;
		myCollider.sharedMesh = myMesh;
	

	}




}

