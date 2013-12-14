using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HexTile : MonoBehaviour {
    public HexMap map;
    public Vector2 position;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (map != null) {
            transform.position = map.grid.HexToCenter(HexGrid.HexToCube(new HexField(position.x, position.y))).position;
        }
	}
}
