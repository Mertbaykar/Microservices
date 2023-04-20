using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Bases
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Filters all entity types while querying, which are derived from EntityBase
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void IsActiveFilter(ModelBuilder modelBuilder)
        {
            Expression<Func<EntityBase, bool>> filterExpr = bm => bm.IsActive;
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(entity => entity.ClrType.IsAssignableTo(typeof(EntityBase))))
            {
                var parameter = Expression.Parameter(entityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambdaExpression);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IsActiveFilter(modelBuilder);
        }
    }
}
