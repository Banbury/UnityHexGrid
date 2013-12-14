using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class HexGrid {
    private static double SQRT_3_2 = Math.Sqrt(3) / 2;

    [SerializeField]
    private CubeList hexes;

    public Orientation orientation = Orientation.Horizontal;
    public float scale = 1.0f;

    public HexGrid(CubeList hexes) {
        this.hexes = hexes;
    }

    public IList<Cube> Hexes {
        get {
            return hexes;
        }
    }

    public ScreenCoordinate HexToCenter(Cube cube) {
        // NOTE: this is really a matrix multiply turning a 3-vector
        // into a 2-vector, and there's one matrix for each
        // orientation
        if (orientation == Orientation.Horizontal) {
            return new ScreenCoordinate(scale * (SQRT_3_2 * (cube.position.x + 0.5 * cube.position.z)),
                                        scale * (0.75 * cube.position.z));
        }
        else {
            return new ScreenCoordinate(scale * (0.75 * cube.position.x),
                                        scale * (SQRT_3_2 * (cube.position.z + 0.5 * cube.position.x)));
        }
    }

    public Bounds Bounds {
        get {
            IEnumerable<ScreenCoordinate> centers = hexes.Select(hex => HexToCenter(hex));

            Bounds b1 = BoundsOfPoints(PolygonVertices());
            Bounds b2 = BoundsOfPoints(new List<ScreenCoordinate>(centers));

            Bounds b = new Bounds();
            b.SetMinMax(new Vector3(b1.min.x + b2.min.x, b1.min.y + b2.min.y), new Vector3(b1.max.x + b2.max.x, b1.max.y + b2.max.y));
            return b;
        }
    }

    public IList<ScreenCoordinate> PolygonVertices() {
        List<ScreenCoordinate> points = new List<ScreenCoordinate>();

        foreach (int i in Enumerable.Range(0, 6)) {
            var angle = 2 * Math.PI * (2 * i - (orientation == Orientation.Horizontal ? 1 : 0)) / 12;
            points.Add(new ScreenCoordinate(0.5 * scale * Math.Cos(angle),
                                             0.5 * scale * Math.Sin(angle)));
        }
        return points;
    }

    public static Bounds BoundsOfPoints(IList<ScreenCoordinate> points) {
        float minX = 0.0f, minY = 0.0f, maxX = 0.0f, maxY = 0.0f;

        foreach (ScreenCoordinate p in points) {
            if (p.position.x < minX) { minX = p.position.x; }
            if (p.position.x > maxX) { maxX = p.position.x; }
            if (p.position.y < minY) { minY = p.position.y; }
            if (p.position.y > maxY) { maxY = p.position.y; }
        }
        Bounds b = new Bounds();
        b.SetMinMax(new Vector3(minX, minY), new Vector3(maxX, maxY));
        return b;
    }

    public static Cube HexToCube(HexField hex) {
        return new Cube(hex.q, -hex.r - hex.q, hex.r);
    }

    public static HexField CubeToHex(Cube cube) {
        return new HexField(cube.position.x, cube.position.z);
    }

    public static Cube OddQToCube(HexField hex) {
        int x = hex.q, z = hex.r - ((hex.q - (hex.q & 1)) >> 1);
        return new Cube(x, -x - z, z);
    }

    public static HexField CubeToOddQ(Cube cube) {
        int x = (int)cube.position.x, z = (int)cube.position.z;
        return new HexField(x, z + ((x - (x & 1)) >> 1));
    }

    public static Cube EvenQToCube(HexField hex) {
        int x = hex.q, z = hex.r - ((hex.q + (hex.q & 1)) >> 1);
        return new Cube(x, -x - z, z);
    }

    public static HexField CubeToEvenQ(Cube cube) {
        int x = (int)cube.position.x, z = (int)cube.position.z;
        return new HexField(x, z + ((x + (x & 1)) >> 1));
    }

    public static Cube OddRToCube(HexField hex) {
        int z = hex.r, x = hex.q - ((hex.r - (hex.r & 1)) >> 1);
        return new Cube(x, -x - z, z);
    }

    public static HexField CubeToOddR(Cube cube) {
        int x = (int)cube.position.x, z = (int)cube.position.z;
        return new HexField(x + ((z - (z & 1)) >> 1), z);
    }

    public static Cube EvenRToCube(HexField hex) {
        int z = hex.r, x = hex.q - ((hex.r + (hex.r & 1)) >> 1);
        return new Cube(x, -x - z, z);
    }

    public static HexField cubeToEvenR(Cube cube) {
        int x = (int)cube.position.x, z = (int)cube.position.z;
        return new HexField(x + ((z + (z & 1)) >> 1), z);
    }

    public delegate Cube ToCube(HexField hex);

    // These functions generate various shapes of hex maps

    public static CubeList TrapezoidalShape(int minQ, int maxQ, int minR, int maxR, ToCube toCube) {
        var hexes = new CubeList();
        foreach (int q in Enumerable.Range(minQ, minQ + maxQ + 1)) {
            foreach (int r in Enumerable.Range(minR, minR + maxR + 1)) {
                hexes.Add(toCube(new HexField(q, r)));
            }
        }
        return hexes;
    }


    public static CubeList TriangularShape(int size) {
        var hexes = new CubeList();
        foreach (int k in Enumerable.Range(0, size + 1)) {
            foreach (int i in Enumerable.Range(0, k + 1)) {
                hexes.Add(new Cube(i, -k, k - i));
            }
        }
        return hexes;
    }


    public static CubeList HexagonalShape(int size) {
        var hexes = new CubeList();
        foreach (int x in Enumerable.Range(-size, 2 * size + 1)) {
            foreach (int y in Enumerable.Range(-size, 2 * size + 1)) {
                var z = -x - y;
                if (Math.Abs(x) <= size && Math.Abs(y) <= size && Math.Abs(z) <= size) {
                    hexes.Add(new Cube(x, y, z));
                }
            }
        }
        return hexes;
    }
}

public enum Orientation {
    Horizontal, Vertical
}

public enum GridShape {
    Trapeze, Triangle, Hexagonal
}