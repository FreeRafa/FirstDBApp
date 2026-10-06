using FirstDBApp.Modelos.Entidades;
using FirstDBApp.Servico;
using Microsoft.EntityFrameworkCore;

namespace FirstDBApp.Apresentacao.GestaoMenu
{
    public class ClienteMenu
    {
        private readonly ClienteServico _clienteServico;

        public ClienteMenu(ClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        public async Task ExibirMenuCliente()
        {
            string? opcao;

            do
            {
                Console.WriteLine();
                Console.WriteLine("=== Menu de Clientes ===");
                Console.WriteLine("1 - Adicionar cliente");
                Console.WriteLine("2 - Listar todos os clientes");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        await CriarCliente();
                        break;
                    case "2":
                        await ListarTodosOsClientes();
                        break;
                    case "0":
                        Console.WriteLine("A sair...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            } while (opcao != "0");
        }

        private async Task CriarCliente()
        {
            Console.WriteLine();
            Console.WriteLine("=== Adicionar Cliente ===");

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Telefone (opcional): ");
            string? telefone = Console.ReadLine();

            var cliente = new Cliente
            {
                Nome = nome,
                Email = email,
                Telefone = string.IsNullOrWhiteSpace(telefone) ? null : telefone,
                CriadoEm = DateOnly.FromDateTime(DateTime.Now),
                EstaAtivo = true
            };

            try
            {
                await _clienteServico.CriarCliente(cliente);
                Console.WriteLine("Cliente criado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Dados inválidos: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Erro ao guardar na base de dados: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        private async Task ListarTodosOsClientes()
        {
            Console.WriteLine();
            Console.WriteLine("=== Lista de Clientes ===");
            var clientes = await _clienteServico.ObterTodosOsClientes();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente encontrado.");
                return;
            }
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.ClienteId}, Nome: {cliente.Nome}, Email: {cliente.Email}, Telefone: {cliente.Telefone}");
            }
        }
    }
}