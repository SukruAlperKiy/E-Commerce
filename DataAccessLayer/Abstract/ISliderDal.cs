using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ISliderDal
    {
        void Insert(Slider entity);
        void Update(Slider entity);
        void Delete(int id);
        List<Slider> GetAll();
        Slider GetById(int id);

    }
}
