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
        private Contexto _contexto;

        public ProductosBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        private bool Existe(int ID)
        {
            bool esta = false;

            try
            {
                esta = _contexto.Productos.Any(e => e.ProductoId == ID);
            }
            catch (Exception)
            {
                throw;
            }

            return esta;
        }
        public bool Existe(string descrip)
        {
            bool está = false;

            try
            {
                está = _contexto.Productos.Any(e => e.Descripcion == descrip);
            }
            catch (Exception)
            {
                throw;
            }
            return está;
        }
        private bool Insertar(Productos producto)
        {
            bool esta = false;

            try
            {
                producto.ValorInventario = producto.Costo * producto.Existencia;
                _contexto.Productos.Add(producto);

                esta = _contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }

            return esta;
        }
        private bool Modificar(Productos producto)
        {
            bool paso = false;

            try
            {
                _contexto.Database.ExecuteSqlRaw($"Delete FROM ProductosDetalle where ProductoId={producto.ProductoId}");

                foreach (var anterior in producto.ProductosDetalle)
                {
                    _contexto.Entry(anterior).State = EntityState.Added;
                }

                _contexto.Entry(producto).State = EntityState.Modified;

                paso = _contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _contexto.Dispose();
            }

            return paso;
        }

        public bool Guardar(Productos producto)
        {
            producto.Ganancia = Convert.ToInt32(((producto.Precio - producto.Costo) / producto.Costo) * 100);

            if (Existe(producto.ProductoId))
                return Modificar(producto);
            else
                return Insertar(producto);
        }
        public Productos Buscar(int ID)
        {
            Productos producto;

            try
            {
                producto = _contexto.Productos.Include(x => x.ProductosDetalle).Where(p => p.ProductoId == ID).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return producto;
        }
        public bool Eliminar(int id)
        {
            bool paso = false;

            try
            {
                var producto = _contexto.Productos.Find(id);
                if (producto != null)
                {
                    _contexto.Productos.Remove(producto);
                    paso = _contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                lista = _contexto.Productos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            
            return lista;
        }
    }
}