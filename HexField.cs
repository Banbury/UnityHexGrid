using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class HexField {
    public Vector2 position;

    public HexField(int q, int r) {
        position = new Vector2(q, r);
    }

    public HexField(float q, float r) {
        position = new Vector2(q, r);
    }

    public int q {
        get {
            return (int)position.x;
        }
    }

    public int r {
        get {
            return (int)position.y;
        }
    }

    public override string ToString() {
        return String.Format("{0}:{1}", q, r);
    }
}
