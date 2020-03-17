using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Parcial2.DAL;
using Parcial2.Entidades;

namespace Parcial2.BLL
{
     public class LlamadasBLL
    {
        public static bool Guardar(LLamada llamada)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Llamadas.Add(llamada) != null)
                    paso = db.SaveChanges() > 0;
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(LLamada llamada)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                db.Database.ExecuteSqlRaw($"DELETE FROM LlamadaDetalle Where LlamadaId = {llamada.LlamadaId}");
                foreach(var item in llamada.Llamadas)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.Entry(llamada).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            Contexto db = new Contexto();
            bool paso = false;
            try
            {
                var eliminar = db.Llamadas.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static LLamada Buscar(int id)
        {
            Contexto db = new Contexto();
            LLamada llamada = new LLamada();
            try
            {
                llamada = db.Llamadas.Include(o => o.Llamadas).Where(p => p.LlamadaId == id).SingleOrDefault();
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return llamada;
        }
        public static List<LLamada> GetList(Expression<Func<LLamada, bool>> llamada)
        {
            Contexto db = new Contexto();
            List<LLamada> Lista = new List<LLamada>();
            try
            {
                Lista = db.Llamadas.Where(llamada).ToList();
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}
