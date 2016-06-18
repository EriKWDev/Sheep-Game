using UnityEngine;
using System.Collections;

public class ChangeParticleSortingLayer : MonoBehaviour {
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Particles";
	}
}
