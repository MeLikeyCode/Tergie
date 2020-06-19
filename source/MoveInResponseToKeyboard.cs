using System;

namespace Tergie.source
{
    public class MoveInResponseToKeyboard: Behavior
    {
        public int Speed { get; set; } // in characters per key event

        public MoveInResponseToKeyboard(Entity entity, int speed)
        {
            _entity = entity;
            Speed = speed;
        }

        public override void OnKeyEvent(ConsoleKeyInfo keyInfo)
        {
            int V_AMOUNT = Speed;
            int H_AMOUNT = Speed * 2;
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                    _entity.Pos.Y -= V_AMOUNT;
                    break;
                case ConsoleKey.S:
                    _entity.Pos.Y += V_AMOUNT;
                    break;
                case ConsoleKey.A:
                    _entity.Pos.X -= H_AMOUNT;
                    break;
                case ConsoleKey.D:
                    _entity.Pos.X += H_AMOUNT;
                    break;
            }
        }

        private Entity _entity;
    }
}