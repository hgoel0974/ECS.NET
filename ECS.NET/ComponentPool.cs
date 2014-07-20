using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int AddComponent(IComponent c)
        {
            components.Add(c);
            return components.Count - 1;    //Return the id of the added component
        }

        public void RemoveComponent(int id)
        {
            components[id].Dispose();
            components.RemoveAt(id);
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
