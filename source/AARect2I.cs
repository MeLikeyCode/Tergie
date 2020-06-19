using System;

namespace Tergie.source
{
    /// <summary>
    /// A 2D axis aligned rectangle.
    /// </summary>
    public class AARect2I
    {
        public Vector2I TopLeft { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2I BotRight => new Vector2I(TopLeft.X+Width,TopLeft.Y+Height);
        public int Left => TopLeft.X;
        public int Right => TopLeft.X + Width;
        public int Top => TopLeft.Y;
        public int Bottom => TopLeft.Y + Height;

        public AARect2I(Vector2I topLeft, int width, int height)
        {
            TopLeft = topLeft;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(AARect2I rect)
        {
            AARect2I rect1 = this;
            AARect2I rect2 = rect;
            return rect1.TopLeft.X < rect2.TopLeft.X + rect2.Width &&
                   rect1.TopLeft.X + rect1.Width > rect2.TopLeft.X &&
                   rect1.TopLeft.Y < rect2.TopLeft.Y + rect2.Height &&
                   rect1.TopLeft.Y + rect1.Height > rect2.TopLeft.Y;
        }
    }
}