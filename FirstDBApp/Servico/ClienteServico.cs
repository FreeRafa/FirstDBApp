using FirstDBApp.Infraestrutura.Repositorio;
using FirstDBApp.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstDBApp.Servico
{
    public class ClienteServico
    {
        private readonly ClienteRepositorio _clienteRepositorio;

        public ClienteServico(ClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<Cliente> CriarCliente(Cliente cliente)
        {
            
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                throw new ArgumentException("O nome do cliente é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                throw new ArgumentException("O email do cliente é obrigatório.");
            }
            
            return await _clienteRepositorio.CriarClienteAsync(cliente);
        }

        public async Task<List<Cliente>> ObterTodosOsClientes()
        {
            return await _clienteRepositorio.ObterTodosOsClientes();
        }

        public async Task<Cliente?> ObterClientePorId(int clienteId)
        {
            return await _clienteRepositorio.ObterClientePorIdAsync(clienteId);
        }

        public async Task DeletarCliente(int clienteId)
        {
            await _clienteRepositorio.DeletarClienteAsync(clienteId);
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                throw new ArgumentException("O nome do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                throw new ArgumentException("O email do cliente é obrigatório.");
            }
            await _clienteRepositorio.AtualizarClienteAsync(cliente);
        }

    }
}
