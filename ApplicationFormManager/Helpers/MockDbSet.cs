using Microsoft.EntityFrameworkCore;
using Moq;

namespace ApplicationFormManager.Helpers
{
    public static class DbSetMock
    {
        public static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            dbSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            dbSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(entity => sourceList.Remove(entity));

            return dbSet;
        }
    }
}
