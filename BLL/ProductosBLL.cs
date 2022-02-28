using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using Samuel_Duran_Ap1_p1_.DAL;
using Samuel_Duran_Ap1_p1_.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Samuel_Duran_Ap1_p1_.BLL
{
    public class ProductosBLL
    {
        private static bool Existe(int ID)
        {
            bool está = false;
            Contexto contexto = new Contexto();

            try
            {
                está = contexto.Productos.Any(e => e.ProductoId == ID);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return está;
        }
        public static bool Existe(string descrip)
        {
            bool está = false;
            Contexto contexto = new Contexto();

            try
            {
                está = contexto.Productos.Any(e => e.Descripcion == descrip);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return está;
        }
        public static bool Insertar(Productos producto)
        {
            Contexto contexto = new Contexto();
            bool esta = false;

            try
            {
                producto.ValorInventario = producto.Costo * producto.Existencia;
                contexto.Productos.Add(producto);
                esta = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return esta;
        }
        private static bool Modificar(Productos producto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                producto.ValorInventario = producto.Costo * producto.Existencia;
                contexto.Database.ExecuteSqlRaw($"Delete FROM ProductosDetalle where ProductoId={producto.ProductoId}");
                foreach(var anterior in producto.ProductosDetalle)
                {
                    contexto.Entry(anterior).State= EntityState.Added;
                }
                contexto.Entry(producto).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        public static bool Guardar(Productos producto)
        {
            producto.Ganancia = Convert.ToInt32(((producto.Precio-producto.Costo)/producto.Costo)*100);
            if (Existe(producto.ProductoId))
                return Modificar(producto);
            else
                return Insertar(producto);
        }
        public static Productos Buscar(int ID)
        {
            Contexto contexto = new Contexto();
            Productos producto;

            try
            {
                producto = contexto.Productos.Include(x => x.ProductosDetalle).Where( p => p.ProductoId == ID).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return producto;
        }
        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                var producto = contexto.Productos.Find(id);
                if (producto != null)
                {
                    contexto.Productos.Remove(producto);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
        {
            List<Productos> lista = new List<Productos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Productos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}