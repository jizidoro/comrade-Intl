﻿#region

using System.ComponentModel.DataAnnotations;
using Comrade.Domain.Bases.Interfaces;

#endregion

namespace Comrade.Domain.Bases
{
    public abstract class Entity : IEntity
    {
        [Key] public int Id { get; set; }

        public virtual int Key => Id;

        public virtual string Value => ToString()!;
    }
}