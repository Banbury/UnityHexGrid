using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class ScreenCoordinate {
    public Vector2 position;

    public ScreenCoordinate(float x, float y) {
        position = new Vector2(x, y);
    }

    public ScreenCoordinate(double x, double y) {
        position = new Vector2((float)x, (float)y);
    }

    public override bool Equals(object o) {
        ScreenCoordinate p = o as ScreenCoordinate;
        return position.Equals(p.position); 
    }
    public override int GetHashCode() {
 	    return position.GetHashCode();
    }
    public override String ToString() { return String.Format("{0},{1}", position.x, position.y); }
    
    public double LengthSquared { get { return position.x * position.x + position.y * position.y; } }
    public double Length { get { return Math.Sqrt(LengthSquared); } }

    public ScreenCoordinate Normalize() { var d = Length; return new ScreenCoordinate(position.x / d, position.y / d); }
    public ScreenCoordinate Scale(float d) { return new ScreenCoordinate(position.x * d, position.y * d); }
    
    public ScreenCoordinate rotateLeft() { return new ScreenCoordinate(position.y, -position.x); }
    public ScreenCoordinate rotateRight() { return new ScreenCoordinate(-position.y, position.x); }
    
    public static ScreenCoordinate operator +(ScreenCoordinate p1, ScreenCoordinate p2) { return new ScreenCoordinate(p1.position.x + p2.position.x, p1.position.y + p2.position.y); }
    public static ScreenCoordinate operator -(ScreenCoordinate p1, ScreenCoordinate p2)  { return new ScreenCoordinate(p1.position.x - p2.position.x, p1.position.y - p2.position.y); }
    public float Dot(ScreenCoordinate p) { return position.x * p.position.x + position.y * p.position.y; }
    public float Cross(ScreenCoordinate p) { return position.x * p.position.y - position.y * p.position.x; }
    public double Distance(ScreenCoordinate p) { return (this-p).Length; }}
