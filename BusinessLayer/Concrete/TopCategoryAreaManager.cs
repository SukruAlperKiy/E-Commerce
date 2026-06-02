using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TopCategoryAreaManager : ITopCategoryAreaService
    {
        private readonly ITopCategoryAreaDal _topCategoryAreaDal;

        public TopCategoryAreaManager(ITopCategoryAreaDal topCategoryAreaDal444)
        {
            _topCategoryAreaDal = topCategoryAreaDal444;
        }


        public void aaDelete(int id)
        {
            _topCategoryAreaDal.Delete(id);
        }

        public List<TopCategoryArea> aaGetAll()
        {
            return _topCategoryAreaDal.GetAll();
        }

        public TopCategoryArea aaGetById(int id)
        {
            return _topCategoryAreaDal.GetById(id);
        }

        public void aaInsert(TopCategoryArea varlik)
        {
            _topCategoryAreaDal.Insert(varlik);
        }

        public void aaUpdate(TopCategoryArea varlik)
        {
            _topCategoryAreaDal.Update(varlik);
        }
    }
}
