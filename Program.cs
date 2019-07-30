using System;
using System.Collections.Generic;
using BancoClientes.domain;

namespace BancoClientes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instancia da classe cliente
            Client cli = new Client();
            Console.Clear();
            // Console.WriteLine("Digite o nome do Client");
            // cli.nome = Console.ReadLine();

            // Console.WriteLine("Digite o Email do Client ");
            // cli.email = Console.ReadLine();

            // Console.WriteLine("Digite o telefone do Client");
            // cli.telefone = Console.ReadLine();

            // Console.WriteLine("Digite a idade do Client");
            // cli.idade = int.Parse(Console.ReadLine());

            // Console.WriteLine(cli.cadastrar());

            List<Client> rs = cli.listar();

            for(int i = 0; i < rs.Count; i++){
                Console.WriteLine(rs[i].id+"\t"+rs[i].nome+ "\t"+rs[i].email+"\t"+rs[i].telefone+"\t"+ rs[i].idade+"\t"+ rs[i].datacadastro);
            }

        }
    }
}
