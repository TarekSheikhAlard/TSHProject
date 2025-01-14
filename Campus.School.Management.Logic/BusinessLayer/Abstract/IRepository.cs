using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Campus.School.Management.Logic.BusinessLayer;

namespace Campus.School.Management.Logic.BusinessLayer.Abstract
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T  GetById(int ID);
        bool Delete(int ID);
        T Update(T model);
        void Create(T model);
        T Create();

      

    }
}
