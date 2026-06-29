namespace CitadoDev.Data.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllListAsync();
        Task<List<TEntity>> GetAllListWithInclude(List<string> properties);
        IQueryable<TEntity> GetAllQuery();
        IQueryable<TEntity> GetAllQueryWithInclude(List<string> properties);
        Task<TEntity?> GetEntityByIdAsync(int Id);
        Task RemoveAsync(int Id);
        Task<TEntity> SaveEntityAsync(TEntity entity);
        Task<TEntity?> UpdateEntityAsync(int id, TEntity entity);
    }
}