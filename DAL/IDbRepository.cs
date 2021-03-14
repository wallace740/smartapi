using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IDbRepository<T>
    {
        T GetItemById(string id);
        List<T> GetAll();
        T Insert(T item);
        T Update(T item);
    }
}
