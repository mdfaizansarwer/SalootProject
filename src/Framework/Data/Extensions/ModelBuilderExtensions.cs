using Core.Utilities;
using Data.Entities;
using Data.Entities.Logging;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Dynamicaly register all Entities that inherit from specific BaseType.
        /// </summary>
        /// <param name="modelBuilder">For access to entities.</param>
        /// <param name="assemblies">Assemblies contains Entities.</param>
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(p => p.GetExportedTypes())
                .Where(p => p.IsClass && !p.IsAbstract && p.IsPublic && typeof(BaseType).IsAssignableFrom(p));

            foreach (Type type in types)
            {
                modelBuilder.Entity(type);
            }
        }

        /// <summary>
        /// Dynamicaly load all IEntityTypeConfiguration with Reflection.
        /// </summary>
        /// <param name="modelBuilder">For access to entities.</param>
        /// <param name="assemblies">Assemblies contains Entities.</param>
        public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

            IEnumerable<Type> types = assemblies.SelectMany(p => p.GetExportedTypes())
                .Where(p => p.IsClass && !p.IsAbstract && p.IsPublic);

            foreach (Type type in types)
            {
                foreach (Type iface in type.GetInterfaces())
                {
                    if (iface == typeof(ICreatedOn))
                    {
                        modelBuilder.Entity(type).Property(nameof(ICreatedOn.CreatedOn)).HasDefaultValue(DateTime.Now);
                    }

                    //if (iface == typeof(IBaseEntity))
                    //{
                    //    modelBuilder.Entity(type).HasKey(nameof(IBaseEntity.Id));
                    //}

                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        MethodInfo applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
                    }
                }
            }
        }

        /// <summary>
        /// Add delete behavior to entities with reflection.
        /// </summary>
        /// <param name="modelBuilder">For access to entities.</param>
        /// <param name="deleteBehavior">Intended DeleteBehavior enum.</param>
        public static void AddDeleteBehaviorConvention(this ModelBuilder modelBuilder, DeleteBehavior deleteBehavior)
        {
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(p => p.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (IMutableForeignKey fk in cascadeFKs)
            {
                fk.DeleteBehavior = deleteBehavior;
            }
        }

        /// <summary>
        /// Add check constraints for current database 
        /// </summary>
        /// <param name="modelBuilder">For adding check constraints to entities.</param>
        /// <param name="databaseFacade">For access to database provider.</param>
        public static void AddCheckConstraints(this ModelBuilder modelBuilder, DatabaseFacade databaseFacade)
        {
            // Postgres
            if (databaseFacade.IsNpgsql())
            {
                modelBuilder.Entity<Team>().HasCheckConstraint("chk_tenant", "(( parent_id IS NULL AND tenant_id IS NOT NULL) OR (tenant_id IS NULL AND parent_id IS NOT NULL))");
                modelBuilder.Entity<EmailsLog>().HasCheckConstraint("chk_emails_log", "(( to_user_id IS NULL AND to_email IS NOT NULL) OR (to_email IS NULL AND to_user_id IS NOT NULL))");
            }
            // SqlServer
            else
            {
                modelBuilder.Entity<Team>().HasCheckConstraint("CHK_Tenant", "((ParentId IS NULL AND TenantId IS NOT NULL) OR (TenantId IS NULL AND ParentId IS NOT NULL))");
                modelBuilder.Entity<EmailsLog>().HasCheckConstraint("CHK_EmailsLog", "((ToUserId IS NULL AND ToEmail IS NOT NULL) OR (ToEmail IS NULL AND ToUserId IS NOT NULL))");
            }
        }

        /// <summary>
        /// Change naming conventions to snake case for
        /// all tables,columns,primary key constraints,
        /// foreign key constraints and indexes.
        /// </summary>
        /// <param name="modelBuilder">For access to tables and columns and changing them.</param>
        public static void NamesToSnakeCase(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Change table names
                var tableName = entity.GetTableName();
                var schema = entity.GetSchema();
                entity.SetTableName(tableName.ToSnakeCase());

                // Change column names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }

                // Change primary keys constraint names
                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                // Change foreign keys constraint names
                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                // Change index names
                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
                }
            }
        }
    }
}