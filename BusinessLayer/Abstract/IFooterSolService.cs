using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IFooterSolService
    {
        void IFooterSolServiceInsert(FooterSol varlik);
        void IFooterSolServiceUpdate(FooterSol varlik);
        void IFooterSolServiceDelete(int id);
        List<FooterSol> IFooterSolServiceGetAll();
        FooterSol IFooterSolServiceGetById(int id);


    }
}
