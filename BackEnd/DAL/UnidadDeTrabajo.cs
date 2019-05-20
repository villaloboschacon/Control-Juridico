using System;
using BackEnd.Model;
namespace BackEnd.DAL
{
    class UnidadDeTrabajo<T> : IDisposable where T : class
    {
        private readonly SCJ_BDEntities context;
        public IDALGenerico<TablaGeneral> tablaDAL;
        public IDALGenerico<T> genericDAL;
        public UnidadDeTrabajo(SCJ_BDEntities _context)
        {
            context = _context;
            genericDAL = new DALGenericoImpl<T>(context);
            tablaDAL = new DALGenericoImpl<TablaGeneral>(context);
        }
        public void Complete()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
