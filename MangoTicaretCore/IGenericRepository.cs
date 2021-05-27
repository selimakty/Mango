using System.Collections.Generic;

namespace MangoTicaretCore
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        long Save();
        void Delete(long id);
        List<dynamic> GetData();
        List<TEntity> GetModelList();
        TEntity GetModel();
        void Execute();
        object ExecuteScalar();
        string ExecuteScalarString();
        int ExecuteScalarInt();
        long ExecuteScalarLong();
        double ExecuteScalarDouble();
    }
}
