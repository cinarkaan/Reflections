using System;

namespace ReflectionTask
{
    public class CustomSerializeAttribute : Attribute
    {
        public readonly string Name;

        public CustomSerializeAttribute(){}

        public CustomSerializeAttribute(string name)
        {
            Name = name;
        }
    }
}
