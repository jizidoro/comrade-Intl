#region

using System;

#endregion

namespace Comrade.Domain.Attributes
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