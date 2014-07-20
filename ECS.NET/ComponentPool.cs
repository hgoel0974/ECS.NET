using System;
using System.Collections.Generic;

namespace ECS.NET
{
    /// <summary>
    /// Stores all the components in the system
    /// </summary>
    public class ComponentPool : IDisposable
    {
        private List<IComponent> components;

        public ComponentPool()
        {
            components = new List<IComponent>();
        }

        /// <summary>
        /// Add a new component to the system
        /// </summary>
        /// <param name="c">The component to add</param>
        /// <returns>The GLOBAL ID of the component</returns>
        public int AddComponent(IComponent c)
        {
            components.Add(c);
            return components.Count - 1;    //Return the id of the added component
        }

        /// <summary>
        /// Remove the specified component from the system
        /// </summary>
        /// <param name="id">The GLOBAL ID of the component to remove</param>
        public void RemoveComponent(int id)
        {
            components[id].Dispose();
            components.RemoveAt(id);
        }

        /// <summary>
        /// Returns the component specified
        /// </summary>
        /// <typeparam name="T">The type of the component to return</typeparam>
        /// <param name="id">The GLOBAL ID of the component to get</param>
        /// <returns>The specified component</returns>
        public T GetComponent<T>(int id) where T:IComponent
        {
            return (T)components[id];
        }

        public IComponent this[int index]
        {
            get
            {
                return components[index];
            }
            set
            {
                components[index] = value;
            }
        }

        public override string ToString()
        {
            return components.ToString();
        }

        public void Dispose()
        {
            for(int i = 0; i < components.Count; i++)
            {
                components[i].Dispose();
            }
            components.Clear();
        }
    }
}
