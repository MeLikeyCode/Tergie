using System;

namespace Tergie.source
{
    /// <summary>
    /// A 2D vector in regular cartesian space (x going right, y going up). Most rendering frameworks use a flipped cartesian space
    /// (x going right but y going down), so just be aware of this (you might have to negate y coordinates or angles).
    ///
    /// This class specifies angles as radians. 0 is pointing towards positive x axis (right), as you go counter-clockwise you increase.
    /// </summary>
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float Magnitude
        {
            get => MathF.Sqrt(MathF.Pow(X, 2) + MathF.Pow(Y, 2));
            set
            {
                X = value * MathF.Cos(Rotation);
                Y = value * MathF.Sin(Rotation);
            }
        }

        public float Rotation
        {
            get => MathF.Atan2(Y, X);
            set
            {
                X = Magnitude * MathF.Cos(value);
                Y = Magnitude * MathF.Sin(value);
            }
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public static Vector2 FromRotationAndMagnitude(in float rotation, float length)
        {
            float dx = length * MathF.Cos(rotation);
            float dy = length * MathF.Sin(rotation);
            return  new Vector2(dx,dy);
        }
        
        public Vector2I ToVector2I() => new Vector2I((int) X,(int) Y);
        
        public Vector2 Copy() => new Vector2(X,Y);

        public void Normalize()
        {
            float length = 1;
            X = length * MathF.Cos(Rotation);
            Y = length * MathF.Sin(Rotation);
        }
        
        // operators
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X,left.Y + right.Y);
        }

        public static Vector2 operator *(Vector2 vector, double scaler)
        {
            return new Vector2(vector.X * (float)scaler, vector.Y * (float)scaler);
        }

        public static Vector2 operator *(double scaler, Vector2 vector)
        {
            return new Vector2(vector.X * (float)scaler, vector.Y * (float)scaler);
        }
    }
}