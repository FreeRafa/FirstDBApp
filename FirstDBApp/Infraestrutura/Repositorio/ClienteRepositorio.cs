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

        public async Task<Cliente> CriarClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<List<Cliente>> ObterTodosOsClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> ObterClientePorIdAsync(int clienteId)
        {
            return await _context.Clientes.FindAsync(clienteId);
        }

    }
}




