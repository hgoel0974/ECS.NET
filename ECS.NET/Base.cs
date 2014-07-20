using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS.NET
{
    public interface ISystem : IDisposable
    {
        void Activate(Entity e);
        Type[] ComponentRequirements();
    }

    public class Entity
    {
        internal EntityComponentSystem Parent;
        internal List<int> ComponentIDs;
        internal List<Type> reqTypes;
        public int Id { get; private set; }

        internal Entity(EntityComponentSystem parent, int id)
        {
            Parent = parent;
            ComponentIDs = new List<int>();
            reqTypes = new List<Type>();
            this.Id = id;
        }

        /// <summary>
        /// Registers a component and returns the LOCAL ID of the component
        /// </summary>
        /// <param name="c">The component</param>
        /// <returns>the LOCAL ID of the component</returns>
        public int AddComponent(IComponent c)
        {
            return AddComponent(Parent.ComponentCollection.AddComponent(c));
        }

        /// <summary>
        /// Registers a component and returns the LOCAL ID of the component
        /// </summary>
        /// <param name="id">The GLOBAL ID of the component</param>
        /// <returns>the LOCAL ID of the component</returns>
        public int AddComponent(int id)
        {
            ComponentIDs.Add(id);
            reqTypes.Add(Parent.ComponentCollection[id].GetType());
            return ComponentIDs.Count - 1;
        }

        /// <summary>
        /// Returns the GLOBAL ID of a component given its LOCAL ID
        /// </summary>
        /// <param name="id">the LOCAL ID of the component</param>
        /// <returns>the GLOBAL ID of the component</returns>
        public int GetGlobalId(int id)
        {
            return ComponentIDs[id];
        }

        internal bool MeetsRequirements(Type[] types)
        {
            if (types == null) return true;
            
            for(int i = 0; i < types.Length; i++)
            {
                if (!reqTypes.Contains(types[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a component given its LOCAL ID
        /// </summary>
        /// <param name="index">the LOCAL ID of the component</param>
        /// <returns>the component</returns>
        public IComponent GetComponent(int index)
        {
            return Parent.ComponentCollection[ComponentIDs[index]];
        }

    }

    public interface IComponent : IDisposable{

    }

}
