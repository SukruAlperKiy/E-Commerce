using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfFooterSolDal : GenelRepository<FooterSol>, IFooterSolDal
    {
        public EfFooterSolDal(SqlVisualStudioKoprusu_EntityFramework context132) : base(context132)
        {
        }

    }
}


