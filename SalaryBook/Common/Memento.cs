using System.Collections.Generic;
using System.Reflection;

namespace Pellared.Common
{
    public interface IMemento<T>
    {
        void Restore(T originator);
    }

    // Save:    memento = new Memento<SomeClass>(this);
    // Restore: memento.Restore(this);
    // can be also used to copy properties to another instance
    public class Memento<T> : IMemento<T>
    {
        private readonly Dictionary<PropertyInfo, object> storedProperties;

        public Memento(T originator)
        {
            storedProperties = new Dictionary<PropertyInfo, object>();
            IEnumerable<PropertyInfo> propertyInfos = ReflectionUtils.GetPropertyInfos(typeof(T));
            foreach (PropertyInfo property in propertyInfos)
            {
                storedProperties[property] = property.GetValue(originator, null);
            }
        }

        public void Restore(T originator)
        {
            foreach (KeyValuePair<PropertyInfo, object> pair in storedProperties)
            {
                pair.Key.SetValue(originator, pair.Value, null);
            }
        }
    }
}