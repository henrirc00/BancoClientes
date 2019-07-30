using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BancoClientes.domain
{
    public class Client
    {

        //Atributos da classe cliente 
        public int id;
        public string nome;
        public string email;
        public string telefone;
        public int idade;
        public DateTime datacadastro;

        public string cadastrar(){
            //Criação da variável msg paro nos ajudar a retornar uma mensagem de cadastro
            //realizado com sucesso ou não 
            string msg = "";

            /* 
            Vcamos chamar as classes do mysql para estabelecer a comunicação com o banco de dados
            e realizar os comandos de CRUD na tabela clientes.

            Utilizaremos a classe MysqlConnection para nos ajudar a estabelecer a comunicação
            com o servidor de banco de dados . Nesta classe você deve passar os seguinte itens:
            - Endereço de servidor de banco de dados(localhost|127.0.0.1)
            - Porta de comunicação (3306|3307)
            - Nome do banco de dados (dbcliente) 
            - Nome de usúario (root)
            - Senha (Não tem)   
            */

            MySqlConnection conexao =new MySqlConnection("Server=localhost;Port=3306;Database=dbcliente;User Id=root;Password=");
            // Vamos abrir o banco de dados com o comando Open
            conexao.Open();


            /* vamos fazer uma instancia da Classe MySqlCommand. Essa classe nos ajuda a executar os comandos
            de sql no banco de dados. Portanto se voce que realizar um insert na tabela, utiliza
            uma instancia do comando MySqlCommand
            */ 
            MySqlCommand cmd = new MySqlCommand();

            /*
            Para que o C# entenda que os comandos no cmd precisam ser executados no banco de dados 
            dbclientes, que foi representado com o objeto conexao, é necessário estabelecer
            uma relação entre cmd e conexao. Faremos isso com o comando cmd.connection=conexao
            
             */
             cmd.Connection = conexao;

             /*
             Vamos dizer qual será o tipo de comando que será executado no banco de dados:
             -StoredProcedure -> Stored(Armazenado) Procedure(Procedimento|Função)
             -Text -> Você escreve o comando sql ponto a ponto para ser executado
             -TableDirect -> Manipular a tabela diretamente 
              */

              cmd.CommandType = System.Data.CommandType.Text;

              /*
              Apos ter selecionado o tipo de comando a ser executado você precisa escrever o comando,
              efetivamente, que será executado. Neste caso utilizaremos o comando 
              Insert
               */

               cmd.CommandText = "insert into tbcliente(nome,email,telefone,idade) values('"+ nome+"','"+email+"','"+telefone+"',"+idade+")";

                 /*
                 Vamos executar a consulta com o comando ExecuteNonQuery(). Execute -> executa a consulta
                 Non (None -> Nenhum) Query(Consulta), ou seja, o comando será executado , porém não
                 retorna o que foi inserido apenas se foi inserido(1) ou não(0) 
                 */

                 int rs = cmd.ExecuteNonQuery();

                 if(rs > 0)
                 msg = "Cliente cadastrado com sucesso!";
                 else
                 msg = "Não foi possível cadastrar o cliente";

                //  fechar a conexao com o banco de dados 
                conexao.Close();

                return msg;
                                    

        }

        public List<Client> listar(){

            List<Client> list = new List<Client>();

            MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=dbcliente;User Id=root;Password=");
            conexao.Open(); //Vamos abrir o banco de dados 

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = conexao;

            cmd.CommandType =System.Data.CommandType.Text;

            cmd.CommandText = "Select id,nome,email,telefone,idade,datacadastro from tbcliente";

            /*
            Para executar e ler os dados a partir do comando select, iremos usar uma 
            execusão com o comando ExecuteReader(Executa a consulta e lê o resultado).
            Esse resultado será armazendo em uma variável do tipo Reader(Leitor) para
            suportar os dados que retornam de consulta
             */
            MySqlDataReader dr = cmd.ExecuteReader();

            /*
            Os dados retornados do comando select foram armazenados na variável dr.
            Com estes dados iremos popular a lista de clientes(lst) criada acima.
            Para realizar esta operação, usaremos a estrutura de repetição while,
            pois nao sabemos onde os dados terminam.
            Enquanto for possível ler o conteúdo de dr continue a buscar os dados 
            e popular a lista de clientes
             */

             while(dr.Read()){

                /*
                Começa do listar retorna uma lista de clientes cadastrados. para nos ajudar no retorno foi 
                criado ima lista do tipo cliente com o nome lst.Essa lista só aceita clientes como conteúdo.
                Portanto foi necessario criar um objeto do tipo Clientes com o nome de cli para organizar os 
                dados vindos do dr (instãncia do banco de dados) e assim adicionar cli a lista de clientes 
                 */

                 Client cli = new Client();
                 cli.id = dr.GetInt32("id");
                 cli.nome = dr.GetString("nome");
                 cli.email = dr.GetString("email");
                 cli.telefone = dr.GetString("telefone");
                 cli.idade = dr.GetInt32("idade");
                 cli.datacadastro = dr.GetDateTime("datacadastro");
                 list.Add(cli);
             }

             conexao.Close();
             return list;



        }

    }
}