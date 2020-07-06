using System;
using Tergie.examples;

namespace Tergie.source
{
    class Program
    {
        public static void Main(string[] args)
        {
            Scene scene = new Scene(80,30);

            char[,] graphics = new char[,]
            {
                {'=','='},
                {'=','='}
            };
            Entity entity = new Entity(graphics);
            scene.AddEntity(entity);
            
            Entity entity2 = new Entity(graphics);
            scene.AddEntity(entity2);
            entity2.Pos.X = 8;
            
            Game.DoLater(() =>
            {
                entity.Pos.X = 30;
            },4);

            Game.Start(scene,80,30);
        }
    }
}