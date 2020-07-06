using System;
using System.Collections.Generic;
using Tergie.source;

namespace Tergie.examples
{
    /// <summary>
    /// Hello! In this example, we learn how to get all entities that are colliding with a particular rectangular region.
    ///
    /// Now, I know you already learned how to check if one entity is colliding with a particular other one, but often
    /// you have a list of entities, and you wanna see which one of these entities are colliding with a particular rectangular region.
    /// </summary>
    public class Example5
    {
        public static void Start()
        {
            Scene scene = new Scene(80, 30);
            
            // create a bunch of entities at random spots in the scene
            Random random = new Random();
            List<Entity> entities = new List<Entity>();
            for (int i = 0; i < 20; i++)
            {
                Entity entity = new Entity(Utils.GetTestGraphics());
                entities.Add(entity);
                scene.AddEntity(entity);
                entity.Pos.X = random.Next(0, 80);
                entity.Pos.Y = random.Next(0, 30);
            }
            
            // find out which entities are colliding with a rectangle at the top left of the scene
            AARect2 someRegion = new AARect2(new Vector2(0,0),20,10);
            HashSet<Entity> collidingEntities = Game.HitTest(entities, someRegion);
            foreach (var entity in collidingEntities)
            {
                // for each entity that collided w the rect, change some of its graphics to say "hit"
                entity.Characters[0, 0] = 'h';
                entity.Characters[0, 1] = 'i';
                entity.Characters[0, 2] = 't';
            }

            Game.Start(scene,80,30);
        }
    }
}