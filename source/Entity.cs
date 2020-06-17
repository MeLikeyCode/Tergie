namespace Tergie.source
{
    public class Entity
    {
        public Vector2I Pos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public char[,] Characters { get; set; }

        public Entity(char[,] characters)
        {
            Pos = new Vector2I(0,0);
            Characters = characters;
            Width = Characters.GetLength(1);
            Height = Characters.GetLength(0);
        }
    }
}