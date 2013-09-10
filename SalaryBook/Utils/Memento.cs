using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pellared.Utils
{
    // Save:    memento = new Memento<SomeClass>(this);
    // Restore: memento.Restore(this);
    // can be also used to copy properties to another instance
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

        public void Restore(T originator)
        {
            foreach (var pair in storedProperties)
            {
                pair.Key.SetValue(originator, pair.Value, null);
            }
        }
    }
}