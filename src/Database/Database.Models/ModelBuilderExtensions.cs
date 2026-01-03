using Microsoft.EntityFrameworkCore;
using System;

namespace Database.Models
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureEntityTriggers(this ModelBuilder modelBuilder, params (Type EntityType, string TriggerName)[] entityTriggers)
        {
            foreach (var (entityType, triggerName) in entityTriggers)
            {
                modelBuilder.Entity(entityType)
                    .ToTable(tb => tb.HasTrigger(triggerName));
            }
        }
    } 
}