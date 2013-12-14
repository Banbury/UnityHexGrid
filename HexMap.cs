using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HexMap : MonoBehaviour, IInitialize {
    public HexGrid grid;

    [UpdateWhenChanged]
    public int size = 10;

    [UpdateWhenChanged]
    public GridShape shape = GridShape.Hexagonal;

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init() {
        var oldgrid = grid;
        switch (shape) {
            case GridShape.Hexagonal:
                grid = new HexGrid(HexGrid.HexagonalShape(size));
                break;
            case GridShape.Triangle:
                grid = new HexGrid(HexGrid.TriangularShape(size));
                break;
            case GridShape.Trapeze:
                grid = new HexGrid(HexGrid.TrapezoidalShape(0, size, 0, size, HexGrid.OddQToCube));
                break;
        }

        if (oldgrid != null) {
            grid.orientation = oldgrid.orientation;
            grid.scale = oldgrid.scale;
        }
    }

    void Reset() {
        Init();
    }

    void OnDrawGizmos() {
        var poly = grid.PolygonVertices();
        foreach (Cube cube in grid.Hexes) {
            ScreenCoordinate pos = grid.HexToCenter(cube);
            ScreenCoordinate first = null;
            ScreenCoordinate last = null;
            foreach (ScreenCoordinate coord in poly) {
                if (last == null) {
                    first = coord + pos;
                    last = coord + pos;
                    continue;
                }
                ScreenCoordinate next = coord + pos;
                Gizmos.DrawLine(new Vector3(last.position.x, last.position.y), new Vector3(next.position.x, next.position.y));
                last = next;
            }

            Gizmos.DrawLine(new Vector3(last.position.x, last.position.y), new Vector3(first.position.x, first.position.y));
        }
    }
}
