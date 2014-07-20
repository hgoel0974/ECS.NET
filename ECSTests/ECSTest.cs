using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECS.NET;

namespace ECSTests
{
    [TestClass]
    public class ECSTest
    {
        static EntityComponentSystem ecs;

        static ECSTest()
        {
            ecs = new EntityComponentSystem();
        }

        public class TestComponent : IComponent
        {
            public int t = 0;
            public void Dispose()
            {
                
            }
        }

        public class TestSystem : ISystem
        {

            public void Activate(Entity e)
            {
                Assert.AreEqual(0, e.Id);
            }

            public Type[] ComponentRequirements()
            {
                return new Type[]{typeof(TestComponent)};
            }

            public void Dispose()
            {
                
            }


            public void Activate(IEnumerable<Entity> e)
            {
                foreach (var item in e)
                {
                    Console.WriteLine(item.Id);
                }
            }
        }

        [TestMethod]
        public void CreateEntityTest()
        {
            Entity e = ecs.CreateEntity();
            e.AddComponent(new TestComponent());
            ecs.CreateEntity();

            ecs.AddSystem(new TestSystem());
            ecs.Activate();
        }
    }
}
