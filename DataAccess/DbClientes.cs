using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Filme_Locadora.DataAccess
{
    public static class DbClientes
    {
        //Método para incluir um cliente no banco de dados
        public static void Incluir(Tbclientes cliente)
        {
            using (var ctx = new VIDEO_LOCADORAContext())
            {
                ctx.Tbclientes.Add(cliente);
                ctx.SaveChanges();
            }
        }
        //Método para listar todos os clientes
        public static IEnumerable<Tbclientes> Listar()
        {
            using (var ctx = new VIDEO_LOCADORAContext())
            {
                return ctx.Tbclientes.ToList();
            }
        }
        //Método para buscar um cliente pelo documento
        public static Tbclientes Buscar(string documento)
        {
            using (var ctx = new VIDEO_LOCADORAContext())
            {
                //FirstOrDefault se for encontrado na lista, na coleção de clientes, um cliente com o documento informado no método, esse obj cliente correspondente é retornado
                //E se não for encontrado ele retorna um valor default, que é nulo.
                return ctx.Tbclientes.FirstOrDefault(p => p.Documento == documento);
            }
        }

        //Método para alterar um cliente com base no documento
        public static void Alterar(Tbclientes cliente)
        {
            using (var ctx = new VIDEO_LOCADORAContext())
            {
                ctx.Entry<Tbclientes>(cliente).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
        //Método para excluir um cliente, com base no documento
        public static void Excluir(Tbclientes cliente)
        {
            using (var ctx = new VIDEO_LOCADORAContext())
            {
                ctx.Entry<Tbclientes>(cliente).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }
    }
}
