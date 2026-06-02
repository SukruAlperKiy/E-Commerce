using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IFooterService
    {
        void ifooterserviceDelete(int id);
        void ifooterserviceInsert(Footer varlik);
        void ifooterserviceUpdate(Footer varlik);
        List<Footer> ifooterserviceGetAll();
        Footer ifooterserviceGetById(int id);
    }
}
