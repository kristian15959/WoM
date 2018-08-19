using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSystem : MonoBehaviour {

    public Vector2 waterGridSize;
    public GameObject waterBox;

    public float waterBoxSize = 100;

	void Start () {
		for(int x = 0; x <= waterGridSize.x; x++)
        {
            for(int y = 0; y<= waterGridSize.y; y++)
            {
                Instantiate(waterBox, new Vector3(waterBoxSize * x, this.transform.position.y, waterBoxSize * y), Quaternion.identity, this.transform);
            }
        }
	}
}
