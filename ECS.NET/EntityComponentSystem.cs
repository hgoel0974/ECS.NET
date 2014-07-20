using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.NET
{
    /// <summary>
    /// Entity Component System
    /// </summary>
    public class EntityComponentSystem : IDisposable
    {
        public ComponentPool ComponentCollection;

        private List<Entity> entities;
        private List<ISystem> systems;

        public EntityComponentSystem()
        {
            ComponentCollection = new ComponentPool();
            systems = new List<ISystem>();
            entities = new List<Entity>();
        }

        public Entity CreateEntity()
        {
            entities.Add(new Entity(this, entities.Count));
            return entities[entities.Count - 1];
        }

        /// <summary>
        /// Register a new system
        /// </summary>
        /// <param name="sys">The system to register</param>
        public void AddSystem(ISystem sys)
        {
            systems.Add(sys);
        }

        /// <summary>
        /// Activate the Entity-Component System
        /// </summary>
        public void Activate()
        {
            //Try to run all registered systems
            for(int i = 0; i < systems.Count; i++)
            {
                //Find all entities that the system in question can process
                Type[] reqs = systems[i].ComponentRequirements();
                List<Entity> tmp = entities.FindAll(e => e.MeetsRequirements(reqs));

                //Process all the entities that the system can handle
                for(int j = 0; j < tmp.Count; j++)
                {
                    systems[i].Activate(tmp[j]);
                }

            }

        }


        public void Dispose()
        {
            for(int i = 0; i < systems.Count; i++)
            {
                systems[i].Dispose();
            }
            systems.Clear();

            ComponentCollection.Dispose();
            entities.Clear();
        }
    }
}
