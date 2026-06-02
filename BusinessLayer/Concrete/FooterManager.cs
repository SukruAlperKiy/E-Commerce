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
    public class FooterManager : IFooterService
    {
        private readonly IFooterDal _footerDaL;

        public FooterManager(IFooterDal footerDaL)
        {
            _footerDaL = footerDaL;
        }

        public void ifooterserviceDelete(int id)
        {
            _footerDaL.Delete(id);
        }

        public List<Footer> ifooterserviceGetAll()
        {
            return _footerDaL.GetAll();
        }

        public Footer ifooterserviceGetById(int id)
        {
            return _footerDaL.GetById(id);
        }

        public void ifooterserviceInsert(Footer varlik)
        {
            _footerDaL.Insert(varlik);
        }

        public void ifooterserviceUpdate(Footer varlik)
        {
            _footerDaL.Update(varlik);

        }
    }
}
