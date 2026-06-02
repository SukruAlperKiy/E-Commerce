using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenelDal<ClassGelecekBuraya> where ClassGelecekBuraya : class
    {
        void Insert(ClassGelecekBuraya varlik);
        void Update(ClassGelecekBuraya varlik);
        List<ClassGelecekBuraya> GetAll();
        void Delete(int id);
        ClassGelecekBuraya GetById(int id);

    }
}
