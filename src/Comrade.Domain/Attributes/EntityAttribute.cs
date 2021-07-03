#region

using System;

#endregion

namespace comrade.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
        public EntityAttribute(string include)
        {
            Include = include;
        }

        public string Include { get; set; }
    }
}