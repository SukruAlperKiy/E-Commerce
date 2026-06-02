using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfTopCategoryAreaDal : ITopCategoryAreaDal
    {
        private readonly SqlVisualStudioKoprusu_EntityFramework _context;

        public EfTopCategoryAreaDal(SqlVisualStudioKoprusu_EntityFramework context555)
        {
            _context = context555;
        }
        public void Delete(int id)
        {
            var deger123 = _context.EfTopCategoryArea.Find(id);
            if (deger123 != null)
            {
                _context.EfTopCategoryArea.Remove(deger123);
                _context.SaveChanges();
            }
        }

        public List<TopCategoryArea> GetAll()
        {
            return _context.EfTopCategoryArea.ToList();
        }
        public TopCategoryArea GetById(int id)
        {
            return _context.EfTopCategoryArea.Find(id);
        }
        public void Insert(TopCategoryArea tuzellik)
        {
            _context.EfTopCategoryArea.Add(tuzellik);
            _context.SaveChanges();
        }
        public void Update(TopCategoryArea tuzellik)
        {
            _context.EfTopCategoryArea.Update(tuzellik);
            _context.SaveChanges();

        }
    }
}
