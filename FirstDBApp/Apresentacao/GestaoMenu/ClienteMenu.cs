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
                Console.WriteLine("3 - Obter cliente por ID");
                Console.WriteLine("4 - Deletar cliente");
                Console.WriteLine("5 - Atualizar cliente");
                Console.WriteLine("6 - Pesquisar cliente por nome");
                Console.WriteLine("7 - Deletar cliente (por ID)");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        await CriarClienteAsync();
                        break;
                    case "2":
                        await ListarTodosOsClientes();
                        break;
                    case "3":
                        await ObterClientePorId();
                        break;
                    case "4":
                        await DeletarCliente();
                        break;
                    case "5":
                        await AtualizarCliente();
                        break;
                    case "6":
                        await PesquisarClientesPorNome();
                        break;
                    case "7":
                        await DeletarClienteAsync();
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

        private async Task CriarClienteAsync()
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
                await _clienteServico.CriarClienteAsync(cliente);
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

        private async Task ObterClientePorId()
        {
            Console.WriteLine();
            Console.WriteLine("=== Obter Cliente por ID ===");
            Console.WriteLine("Digite o ID do cliente ");
            var clientes = await _clienteServico.ObterTodosOsClientes();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente encontrado.");
                return;
            }
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.ClienteId}, Nome: {cliente.Nome}");
            }

            if (int.TryParse(Console.ReadLine(), out int clienteId))
            {
                var cliente = await _clienteServico.ObterClientePorId(clienteId);
                if (cliente != null)
                {
                    Console.WriteLine($"ID: {cliente.ClienteId}, Nome: {cliente.Nome}, Email: {cliente.Email}, Telefone: {cliente.Telefone}");
                }
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, insira um número inteiro.");
            }
        }

        private async Task DeletarCliente()
        {
            Console.WriteLine();
            Console.WriteLine("=== Deletar Cliente ===");
            Console.WriteLine("Digite o ID do cliente que deseja deletar: ");
            var clientes = await _clienteServico.ObterTodosOsClientes();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente encontrado.");
                return;
            }
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.ClienteId}, Nome: {cliente.Nome}");
            }
            if (int.TryParse(Console.ReadLine(), out int clienteId))
            {
                var cliente = await _clienteServico.ObterClientePorId(clienteId);
                if (cliente != null)
                {
                    await _clienteServico.DeletarCliente(clienteId);
                    Console.WriteLine("Cliente deletado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, insira um número inteiro.");
            }
        }

        private async Task AtualizarCliente()
        {
            Console.WriteLine();
            Console.WriteLine("=== Atualizar Cliente ===");
            Console.WriteLine("Digite o ID do cliente que deseja atualizar: ");
            var clientes = await _clienteServico.ObterTodosOsClientes();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente encontrado.");
                return;
            }
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.ClienteId}, Nome: {cliente.Nome}");
            }
            if (int.TryParse(Console.ReadLine(), out int clienteId))
            {
                var cliente = await _clienteServico.ObterClientePorId(clienteId);
                if (cliente != null)
                {
                    Console.Write($"Nome ({cliente.Nome}): ");
                    string? nome = Console.ReadLine() ?? cliente.Nome;
                    Console.Write($"Email ({cliente.Email}): ");
                    string? email = Console.ReadLine() ?? cliente.Email;
                    Console.Write($"Telefone ({cliente.Telefone}): ");
                    string? telefone = Console.ReadLine();
                    telefone = string.IsNullOrWhiteSpace(telefone) ? cliente.Telefone : telefone;
                    cliente.Nome = nome;
                    cliente.Email = email;
                    cliente.Telefone = telefone;
                    try
                    {
                        await _clienteServico.AtualizarCliente(cliente);
                        Console.WriteLine("Cliente atualizado com sucesso!");
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
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, insira um número inteiro.");
            }
        }

        private async Task PesquisarClientesPorNome()
        {
            Console.WriteLine();
            Console.WriteLine("=== Pesquisar Cliente por Nome ===");
            Console.Write("Digite o nome (ou parte do nome): ");
            string? termo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(termo))
            {
                Console.WriteLine("Termo de pesquisa inválido.");
                return;
            }

            var clientes = await _clienteServico.PesquisarPorNomeAsync(termo);

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente encontrado com o nome informado.");
                return;
            }

            Console.WriteLine("Clientes encontrados:");
            MostrarClientes(clientes);
        }

        private static void MostrarClientes(IReadOnlyList<Cliente> clientes)
        {
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.ClienteId}, Nome: {cliente.Nome}, Email: {cliente.Email}, Telefone: {cliente.Telefone}");
            }
        }

        private async Task DeletarClienteAsync()
        {
            Console.WriteLine();
            Console.WriteLine("=== Deletar Cliente (por ID) ===");
            Console.WriteLine("Digite o ID do cliente que deseja deletar: ");
            var clientes = await _clienteServico.ObterTodosOsClientes();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente encontrado.");
                return;
            }
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.ClienteId}, Nome: {cliente.Nome}");
            }
            if (int.TryParse(Console.ReadLine(), out int clienteId))
            {
                var cliente = await _clienteServico.ObterClientePorId(clienteId);
                if (cliente != null)
                {
                    await _clienteServico.DeletarCliente(clienteId);
                    Console.WriteLine("Cliente deletado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, insira um número inteiro.");
            }
        }
    }
}