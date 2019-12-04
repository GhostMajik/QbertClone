using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBaseScript : MonoBehaviour {
	
	protected cubeNodeScript curCube;

	public virtual void StartBehaviour(ref GameObject cube){
		curCube = cube.GetComponent<cubeNodeScript>();
		//Debug.Log (curCube);
		transform.parent = cube.transform;
	}
		
}
