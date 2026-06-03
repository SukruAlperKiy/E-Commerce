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
    public class FooterSolManager : IFooterSolService
    {
        private readonly IFooterSolDal _footerSolDal;
        public FooterSolManager(IFooterSolDal footerSolDal)
        {
            _footerSolDal = footerSolDal;
        }

        public void IFooterSolServiceInsert(FooterSol varlik)
        {
            _footerSolDal.Insert(varlik);
        }

        public void IFooterSolServiceUpdate(FooterSol varlik)
        {
            _footerSolDal.Update(varlik);
        }

        public void IFooterSolServiceDelete(int id)
        {
            _footerSolDal.Delete(id);
        }

        public List<FooterSol> IFooterSolServiceGetAll()
        {
            return _footerSolDal.GetAll();
        }

        public FooterSol IFooterSolServiceGetById(int id)
        {
            return _footerSolDal.GetById(id);
        }

    }
}
