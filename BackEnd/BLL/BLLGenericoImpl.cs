﻿using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public class BLLGenericoImpl<T> : IBLLGenerico<T> where T : class
    {
        private UnidadDeTrabajo<T> unidad;
        public string Add(T entity)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    unidad.genericDAL.Add(entity);
                    unidad.Complete();
                }
                return "Exito";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public string AddRange(IEnumerable<T> entities)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    unidad.genericDAL.AddRange(entities);
                    unidad.Complete();
                }
                return "Exito";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public List<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                List<T> resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.Find(predicate).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public T Get(int id)
        {
            try
            {
                T resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.Get(id);
                }
                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<T> GetAll()
        {
            try
            {
                List<T> resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.GetAll().ToList();
                }
                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public string Remove(T entity)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    unidad.genericDAL.Remove(entity);
                    unidad.Complete();
                }
                return "Exito";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {

                }

            }
            catch (Exception)
            {


            }
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            try
            {
                T resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.SingleOrDefault(predicate);
                }
                return resultado;

            }
            catch (Exception)
            {

                return null;
            }
        }
        public string Update(T entity)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    unidad.genericDAL.Update(entity);
                    unidad.Complete();
                }
                return "Exito";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
        public bool Check(string idDocumento)
        {
            try
            {
                List<T> resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.GetAll().ToList();
                }
                foreach(T entidad in resultado)
                {
                   
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
