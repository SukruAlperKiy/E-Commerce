using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITopCategoryAreaService
    {
        void aaInsert(TopCategoryArea varlik);
        void aaUpdate(TopCategoryArea varlik);
        void aaDelete(int id);
        List<TopCategoryArea> aaGetAll();
        TopCategoryArea aaGetById(int id);

    }
}
