using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SeaInk.Infrastructure.DataAccess.Extensions
{
    public static class DbSetExtensions
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, T data)
            where T : class
        {
            Type t = typeof(T);
            PropertyInfo? keyField = null;

            foreach (PropertyInfo propertyInfo in t.GetProperties())
            {
                KeyAttribute? keyAttr = propertyInfo.GetCustomAttribute<KeyAttribute>();
                if (keyAttr is null)
                    continue;

                keyField = propertyInfo;
                break;
            }

            if (keyField == null)
            {
                throw new Exception($"{t.FullName} does not have a KeyAttribute field. Unable to exec AddOrUpdate call.");
            }

            object? keyVal = keyField.GetValue(data);
            T? dbVal = dbSet.Find(keyVal);

            if (dbVal is not null)
            {
                dbSet.Update(data);
                return;
            }

            dbSet.Add(data);
        }
    }
}