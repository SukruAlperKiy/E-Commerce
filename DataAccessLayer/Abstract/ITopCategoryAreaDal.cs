using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITopCategoryAreaDal
    {
        void Insert(TopCategoryArea entity);
        void Update(TopCategoryArea entity);
        void Delete(int id);
        List<TopCategoryArea> GetAll();
        TopCategoryArea GetById(int id);

    }
}
