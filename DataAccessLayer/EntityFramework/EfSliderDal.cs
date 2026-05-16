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
    public class EfSliderDal : ISliderDal
    {
        private readonly SqlVisualStudioKoprusu_EntityFramework _context;

        public EfSliderDal(SqlVisualStudioKoprusu_EntityFramework context123)
        {
            _context = context123;
        }

        public void Delete(int id)
        {
            var deger = _context.EfSliders.Find(id);
            if (deger != null)
            {
                _context.EfSliders.Remove(deger);
                _context.SaveChanges();
            }
        }

        public List<Slider> GetAll()
        {
            return _context.EfSliders.ToList();
        }

        public Slider GetById(int id)
        {
            return _context.EfSliders.Find(id);
        }

        public void Insert(Slider varlik)
        {
            _context.EfSliders.Add(varlik);
            _context.SaveChanges();
        }

        public void Update(Slider varlik)
        {
            _context.EfSliders.Update(varlik);
            _context.SaveChanges();
        }
    }
}
