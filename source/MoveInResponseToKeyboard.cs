using System;

namespace Tergie.source
{
 
    /// <summary>
    /// Make an entity move in response to keyboard keys being pressed.
    /// </summary>
    public class MoveInResponseToKeyboard: Behavior
    {
        public int Speed { get; set; } // in characters per key event

        public ConsoleKey UpKey = ConsoleKey.W;
        public ConsoleKey DownKey = ConsoleKey.S;
        public ConsoleKey LeftKey = ConsoleKey.A;
        public ConsoleKey RightKey = ConsoleKey.D;

        public MoveInResponseToKeyboard(Entity entity, int speed)
        {
            _entity = entity;
            Speed = speed;
        }

        public override void OnKeyEvent(ConsoleKeyInfo keyInfo)
        {
            int V_AMOUNT = Speed;
            int H_AMOUNT = Speed * 2;

            if (keyInfo.Key == UpKey)
                _entity.Pos.Y -= V_AMOUNT;
            else if (keyInfo.Key == DownKey)
                _entity.Pos.Y += V_AMOUNT;
            else if (keyInfo.Key == LeftKey)
                _entity.Pos.X -= H_AMOUNT;
            else if (keyInfo.Key == RightKey)
                _entity.Pos.X += H_AMOUNT;
        }

        private Entity _entity;
    }
}