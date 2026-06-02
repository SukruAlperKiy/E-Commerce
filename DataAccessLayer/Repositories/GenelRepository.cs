using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenelRepository<ClassGelicekBuraya> : IGenelDal<ClassGelicekBuraya> where ClassGelicekBuraya : class
    {
        //sql ile baglanti kurduk.
        private readonly SqlVisualStudioKoprusu_EntityFramework _context;
        public GenelRepository(SqlVisualStudioKoprusu_EntityFramework context123)
        {
            _context = context123;
        }

        public void Delete(int id)
        {
            var deger = _context.Set<ClassGelicekBuraya>().Find(id);
            _context.Set<ClassGelicekBuraya>().Remove(deger);
            _context.SaveChanges();
        }

        public void Insert(ClassGelicekBuraya varlik)
        {
            _context.Set<ClassGelicekBuraya>().Add(varlik);
            _context.SaveChanges();
        }
            
        public void Update(ClassGelicekBuraya varlik)
        {
            _context.Set<ClassGelicekBuraya>().Update(varlik);
            _context.SaveChanges();
        }

        public List<ClassGelicekBuraya> GetAll()
        {
            return _context.Set<ClassGelicekBuraya>().ToList();
        }

        public ClassGelicekBuraya GetById(int id)
        {
            return _context.Set<ClassGelicekBuraya>().Find(id);
        }
    }
}
