namespace Tergie.source
{
    public class TextEntity: Entity
    {
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _updateChars();
            }
        }

        public TextEntity()
        {
            Text = "";
        }

        public TextEntity(string text)
        {
            Text = text;
        }

        // Update characters of entity based on its text
        private void _updateChars()
        {
            var lines = Text.Split('\n');
            int height = lines.Length;
            int width = 0;
            foreach (var line in lines)
            {
                if (line.Length > width)
                    width = line.Length;
            }
            
            char[,] characters = new char[height,width];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    characters[i, j] = lines[i][j];
                }
            }

            Characters = characters;
        }
        
        // private stuff
        private string _text;
    }
}