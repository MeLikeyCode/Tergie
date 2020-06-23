using System;

namespace Tergie.source
{
    class Program
    {
        private static Scene _startScene;
        private static Scene _mainScene;
        private static Scene _endScene;
        private static FpsCounter _fpsCounter;
        private static TextEntity _scoreLabel;
        private static int _score = 0;
        private static Entity _player;
        private static char[,] _explosionChars = Utils.FileToCharArray("../../../resources/ascii_art/explostion.txt", true);

        static void Main(string[] args)
        {
            // = start scene =
            _startScene = new Scene(200,60);
            Entity title = new Entity(Utils.FileToCharArray("../../../resources/ascii_art/title.txt"));
            _startScene.AddEntity(title);
            title.Pos = new Vector2(70,20);

            char[,] testEntGraphics = new char[,]
            {
                {'-','-','-'},
                {'-','x','-'},
                {'-','-','-'},
            };
            Entity testEnt = new Entity(testEntGraphics);
            _startScene.AddEntity(testEnt);
            
            MenuEntity menu = new MenuEntity();
            _startScene.AddEntity(menu);
            _startScene.KeyFocusedEntity = menu;
            menu.Pos.X = title.Pos.X + 20;
            menu.Pos.Y = title.Pos.Y + 10;
            menu.AddItem("play");
            menu.AddItem("exit");
            menu.ItemSelected += (sender, item) =>
            {
                if (item == "exit")
                    Environment.Exit(0);
                else if (item == "play")
                    Game.Scene = _mainScene;
            };
            
            // = main scene =
            _mainScene = new Scene(200,60);

            // UI
            _fpsCounter = new FpsCounter();
            _mainScene.AddEntity(_fpsCounter);
            
            _scoreLabel = new TextEntity("score: 0");
            _mainScene.AddEntity(_scoreLabel);
            _scoreLabel.Pos = new Vector2(0,2);
            _scoreLabel.Updated += (scoreLabel, dt) => (scoreLabel as TextEntity).Text = "score: " + _score;
            
            // player
            _player = new Entity(Utils.FileToCharArray("../../../resources/ascii_art/plane.txt",true));
            _mainScene.AddEntity(_player);
            _player.Pos.X = _mainScene.Width / 2 - _player.BoundingBox.Width / 2;
            _player.Pos.Y = _mainScene.Height - _player.BoundingBox.Height;
            float fireDelay = 0.5f;
            DateTime timeLastFired = DateTime.Now;
            var missileChars = Utils.FileToCharArray("../../../resources/ascii_art/missile.txt", true);
            _player.KeyEventReceived += (sender, info) =>
            {
                var h_amount = 2;
                var v_amount = 1;
                var key = info.Key;
                AARect2 playerBBox = _player.BoundingBox;
                if (key == ConsoleKey.W)
                {
                    if (playerBBox.Top > 0)
                        _player.Pos.Y -= v_amount;
                }
                else if (key == ConsoleKey.S)
                {
                    if (playerBBox.Bottom < _mainScene.Height)
                        _player.Pos.Y += v_amount;
                }
                else if (key == ConsoleKey.A)
                {
                    if (playerBBox.Left > -13)
                        _player.Pos.X -= h_amount;
                }
                else if (key == ConsoleKey.D)
                {
                    if (playerBBox.Right < _mainScene.Width + 14)
                        _player.Pos.X += h_amount;
                }

                if (key == ConsoleKey.Spacebar)
                {
                    var now = DateTime.Now;
                    var elapsed = now - timeLastFired;
                    if (elapsed.TotalSeconds >= fireDelay)
                    {
                        Entity missile = new Entity(missileChars);
                        missile.Pos = _player.Pos.Copy();
                        missile.Pos.Y -= missile.BoundingBox.Height / 2;
                        missile.Pos.X += 16;
                        _mainScene.AddEntity(missile);
                        
                        missile.Updated += MissileOnUpdated;
                        timeLastFired = now;
                    }
                }
            };
            _mainScene.KeyFocusedEntity = _player;
            
            // enemy spawning
            Random random = new Random();
            var enemyChars = Utils.FileToCharArray("../../../resources/ascii_art/plane2.txt", true);
            Timer spawner = new Timer(-1,4);
            Game.AddTimer(spawner);
            spawner.Start();
            int wave = 0;
            spawner.Fired += sender =>
            {
                for (int i = 0; i < wave / 5 + 1; i++)
                {
                    int rx = random.Next(0, _mainScene.Width - enemyChars.GetLength(1));
                    int ry = random.Next(-50, -10);
                    Entity enemy = new Entity(enemyChars);
                    enemy.Pos = new Vector2(rx,ry);
                    _mainScene.AddEntity(enemy);

                    enemy.Updated += (entity, dt) =>
                    {
                        float speed = 10;
                        entity.Pos.Y += speed * dt;

                        if (entity.Pos.Y > Game.Window.Height)
                            _mainScene.RemoveEntity(entity);

                        if (enemy.BoundingBox.CollidesWith(_player.BoundingBox))
                        {
                            Entity explosion = new Entity(_explosionChars);
                            _mainScene.AddEntity(explosion);
                            explosion.Pos = _player.Pos.Copy();
                            explosion.Pos.X += 1;
                            Game.DoLater(() =>
                            {
                                _mainScene.RemoveEntity(explosion);
                            },0.2f);
                            Game.DoLater(() =>
                            {
                                Game.Scene = _endScene;
                            },2);

                            _mainScene.RemoveEntity(_player);
                            _mainScene.RemoveEntity(enemy);
                        }
                    };
                }

                wave += 1;
            };

            // = end scene =
            _endScene = new Scene(200,60);
            Entity gameOver = new Entity(Utils.FileToCharArray("../../../resources/ascii_art/game_over.txt"));
            _endScene.AddEntity(gameOver);
            gameOver.Pos.X = _endScene.Width / 2 - gameOver.BoundingBox.Width / 2;
            gameOver.Pos.Y = 20;

            // start the game!
            Game.Start(_startScene,200,60);
        }

        private static void MissileOnUpdated(Entity missile, float dt)
        {
            float speed = 70;
            missile.Pos.Y -= dt * speed;
                            
            if (missile.BoundingBox.Bottom < 0)
                _mainScene.RemoveEntity(missile);

            foreach (var entity in _mainScene.entitiesIn(missile.BoundingBox))
            {
                bool collided = false;
                if (entity != missile && entity != _fpsCounter && entity != _scoreLabel && entity != _player)
                {
                    Entity explosion = new Entity(_explosionChars);
                    _mainScene.AddEntity(explosion);
                    Game.DoLater(() => _mainScene.RemoveEntity(explosion), 0.2f);
                    explosion.Pos = entity.Pos.Copy();
                    explosion.Pos.X += 1;

                    _mainScene.RemoveEntity(entity);
                    collided = true;
                    _score += 1;
                }
                
                if (collided)
                    _mainScene.RemoveEntity(missile);
            }
        }
    }
}