using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MiniORM
{
    internal class ChangeTracker<T>
        where T : class, new()
    {
        private readonly List<T> allEntities;
        private readonly List<T> added;
        private readonly List<T> removed;

        public IReadOnlyCollection<T> AllEntities => this.allEntities.AsReadOnly();
        public IReadOnlyCollection<T> Added => this.added.AsReadOnly();
        public IReadOnlyCollection<T> Removed => this.removed.AsReadOnly();


        public ChangeTracker(IEnumerable<T> entities)
        {
            this.added = new List<T>();
            this.removed = new List<T>();

             this.allEntities = CloneEntities(entities);

            //this.allEntities = new List<T>(entities);


        }

        private static List<T> CloneEntities(IEnumerable<T> entities)
        {
            var clonedEntities = new List<T>();

            var propertiesToClone = typeof(T).GetProperties()
                .Where(p => DbContext.AllowedSqlTypes.Contains(p.PropertyType))
                .ToArray();

            foreach (var entity in entities)
            {
                var clonedEntity = Activator.CreateInstance<T>();

                foreach (var property in propertiesToClone)
                {
                    var value = property.GetValue(entity);
                    property.SetValue(clonedEntity, value);
                }

                clonedEntities.Add(clonedEntity);
            }

            return clonedEntities;
        }

        public void Add(T item) => this.added.Add(item);
        public void Remove(T item) => this.removed.Add(item);
        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            List<T> modifiedEntities = new List<T>();

            PropertyInfo[] primaryKeys = typeof(T)
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (var proxyEntity in this.AllEntities)
            {
                object[] primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();

                T entity = dbSet.Entities.SingleOrDefault(e => GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(primaryKeyValues));

                if (entity==null)
                {
                    return modifiedEntities;
                }

                bool isModified = IsModified(proxyEntity, entity);

                if (isModified)
                {
                    modifiedEntities.Add(entity);
                }
            }

            return modifiedEntities;
        }

        private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T entity)
        {
            return primaryKeys.Select(pk=>pk.GetValue(entity));
        }

        private static bool IsModified(T proxyEntity, T entity)
        {
            var monitoredProperties = typeof(T).GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType));

            var modifiedProperties = monitoredProperties
                .Where(pi => !Equals(pi.GetValue(entity), pi.GetValue(proxyEntity)))
                .ToArray();

            var isModified = modifiedProperties.Any();

            return isModified;
        }
    }
}