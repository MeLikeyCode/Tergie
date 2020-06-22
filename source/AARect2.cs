using System;

namespace Tergie.source
{
    public class AARect2
    {
        public Vector2 TopLeft { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Vector2 BotRight => new Vector2(TopLeft.X+Width,TopLeft.Y+Height);
        public float Left => TopLeft.X;
        public float Right => TopLeft.X + Width;
        public float Top => TopLeft.Y;
        public float Bottom => TopLeft.Y + Height;

        public AARect2(Vector2 topLeft, float width, float height)
        {
            TopLeft = topLeft;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(AARect2 rect)
        {
            AARect2 rect1 = this;
            AARect2 rect2 = rect;
            return rect1.TopLeft.X < rect2.TopLeft.X + rect2.Width &&
                   rect1.TopLeft.X + rect1.Width > rect2.TopLeft.X &&
                   rect1.TopLeft.Y < rect2.TopLeft.Y + rect2.Height &&
                   rect1.TopLeft.Y + rect1.Height > rect2.TopLeft.Y;
        }
    }
}