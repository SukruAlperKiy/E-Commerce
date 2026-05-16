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
    public class SliderManager : ISliderService
    {
        private readonly ISliderDal _sliderDal;
        public SliderManager(ISliderDal sliderDal123)
        {
            _sliderDal = sliderDal123;
        }

        public void zzDelete(int id)
        {
            _sliderDal.Delete(id);
        }
        public List<Slider> zzGetAll()
        {
            return _sliderDal.GetAll();
        }
        public Slider zzGetById(int id)
        {
            return _sliderDal.GetById(id);
        }

        public void zzInsert(Slider entity)
        {
            _sliderDal.Insert(entity);
        }
        public void zzUpdate(Slider varlik)
        {
            _sliderDal.Update(varlik);
        }
        
    }
}
