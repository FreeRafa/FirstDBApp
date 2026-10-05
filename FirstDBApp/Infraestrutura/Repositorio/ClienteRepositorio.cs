using FirstDBApp.Infraestrutura.Data;
using FirstDBApp.Modelos.Entidades;
using Microsoft.EntityFrameworkCore;


namespace FirstDBApp.Infraestrutura.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly FirstDBAppContext _context;

        public ClienteRepositorio(FirstDBAppContext context)
        {
            _context = context;
        }

        public async Task<Cliente> CriarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        
    }
}


    

