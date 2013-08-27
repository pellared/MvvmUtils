using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pellared.MvvmUtils
{
    public class Memento<T> : IMemento<T>
    {
        private readonly Dictionary<PropertyInfo, object> storedProperties;

        public Memento(T originator)
        {
            storedProperties = new Dictionary<PropertyInfo, object>();
            var propertyInfos = ReflectionUtils.GetPropertyInfos(typeof(T));
            foreach (var property in propertyInfos)
            {
                storedProperties[property] = property.GetValue(originator, null);
            }
        }

        public virtual void Restore(T originator)
        {
            foreach (var pair in storedProperties)
            {
                pair.Key.SetValue(originator, pair.Value, null);
            }
        }
    }
}