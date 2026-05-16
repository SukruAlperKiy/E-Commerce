using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ISliderService
    {
        void zzInsert(Slider varlik);
        void zzUpdate(Slider varlik);
        void zzDelete(int id);
        List<Slider> zzGetAll();
        Slider zzGetById(int id);
    }
}
