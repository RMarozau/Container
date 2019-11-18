using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class ClassContainer
    {
        private readonly System.Collections.Generic.Dictionary<Type, Type> map = new System.Collections.Generic.Dictionary<Type, Type>();
        public string Name { get; private set; }
        public ClassContainer(string containerName)
        {
            Name = containerName;

        }

        /// <summary>
        /// Register the mapping for inversion of control
        /// </summary>
        /// <typeparam name="From">Interface </typeparam>
        /// <typeparam name="To">Insatnce</typeparam>
        public void Register<From, To>()
        {
            map.Add(typeof(From), typeof(To));

        }

        /// <summary>
        /// Resolves the Instance 
        /// </summary>
        /// <typeparam name="T">Interface</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type type)
        {
            Type resolvedType = map[type];

            var ctor = resolvedType.GetConstructors().First();
            var ctorParameters = ctor.GetParameters();
            if (ctorParameters.Length == 0)
            {
                return Activator.CreateInstance(resolvedType);
            }

            var parameters = new List<object>();


            foreach (var p in ctorParameters)
            {
                parameters.Add(p.ParameterType);
            }

            return ctor.Invoke(parameters.ToArray());
        }
    }
    public interface IScheduleManager
    {
        void GetShedule();
    }

    public class SheduleManager : IScheduleManager
    {
        public void GetShedule()
        {

        }
    }
   
    public class ScheduleViewer
    {
        public IScheduleManager _scheduleManager;
        public ScheduleViewer(IScheduleManager scheduleManager)
        {
            _scheduleManager = scheduleManager;
        }
        public void RenderSchedule()
        {
            _scheduleManager.GetShedule();
        }
    }

}

