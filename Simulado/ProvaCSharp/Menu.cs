using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.ProvacSharp
{
    class Menu
    {
        readonly int[] OpcoesMenu = { 1, 2, 3, 4, 5, 6 };

        void FormataMensagemErro(Retorno retorno)
        {
            Console.WriteLine($"Código de Erro - {retorno.CodRetorno}: {retorno.Mensagem}");
        }

        string FormataValores(decimal valor)
        {
            return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor);
        }

        public void TelaInicial(decimal saldoAtual)
        {
            Console.Clear();
            Console.WriteLine($"== Saldo atual: {FormataValores(saldoAtual)} ==\n");
            Console.WriteLine("1. Efetuar crédito em conta");
            Console.WriteLine("2. Adicionar chave favorita");
            Console.WriteLine("3. Listar chaves favoritas");
            Console.WriteLine("4. Enviar PIX");
            Console.WriteLine("5. Persistir chaves");
            Console.WriteLine("\n6. Sair");
            Console.Write("\nInforme a opção desejada:");
        }

        public void ExecutarMenu()
        {
            Conta contaUsuario = new Conta();
            int opcoesUsuario = 1;
            decimal saldoUsuario = 0;

            while (opcoesUsuario != 6)
            {
                TelaInicial(saldoUsuario);
                try
                {
                    opcoesUsuario = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nPor favor, digite uma opção válida");
                    opcoesUsuario = 6;
                    Console.ReadKey();
                }

                while (!OpcoesMenu.Contains(opcoesUsuario))
                {
                    Console.WriteLine(!OpcoesMenu.Contains(opcoesUsuario));
                    TelaInicial(saldoUsuario);
                    try
                    {
                        opcoesUsuario = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("\nPor favor, digite uma opção válida");
                        opcoesUsuario = 6;
                        Console.ReadKey();
                    }
                }
                bool invalidValue = true;

                switch (opcoesUsuario)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Informe o valor a ser creditado:");
                        var valor = Console.ReadLine();
                        Retorno retorno1 = contaUsuario.CreditarConta(valor);
                        while (retorno1.Sucesso == false)
                        {
                            Console.Clear();
                            FormataMensagemErro(retorno1);
                            Console.WriteLine("Informe o valor a ser creditado:");
                            valor = Console.ReadLine();
                            retorno1 = contaUsuario.CreditarConta(valor);
                        }
                        saldoUsuario = contaUsuario.Saldo;
                        Console.WriteLine(retorno1.Mensagem);
                        Console.WriteLine("Pressione qualquer tecla para voltar ao Menu");
                        Console.ReadKey();

                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Adicionar chave Favorita:");
                        Console.Write("Insira o nome do Titular: ");
                        var nome = Console.ReadLine();
                        string tipoChaveString;
                        int tipoDeChave = 3;
                        while (invalidValue || (tipoDeChave != 1 && tipoDeChave != 2))
                        {
                            Console.WriteLine("\nInsira o tipo de chave: \n(1) - Telefone;\n(2) - CPF ");
                            try
                            {
                                tipoDeChave = Convert.ToInt32(Console.ReadLine());
                                invalidValue = false;
                            }
                            catch
                            {
                                Console.WriteLine("Opção inválida");
                                opcoesUsuario = 6;
                            }
                            
                        }
                        if (tipoDeChave == 1)
                        {
                            tipoChaveString = "Telefone";
                        }
                        else
                        {
                            tipoChaveString = "CPF";
                        }
                        Console.WriteLine("\nInsira a chave:");
                        string nomeChave = Console.ReadLine();
                        Retorno retorno2 = contaUsuario.AdicionarChave(nome, tipoChaveString, nomeChave);
                        while (retorno2.Sucesso == false)
                        {
                            Console.Clear();
                            FormataMensagemErro(retorno2);
                            Console.Write("Insira o nome do Titular: ");
                            nome = Console.ReadLine();
                            tipoDeChave = 3;
                            while (invalidValue || (tipoDeChave != 1 && tipoDeChave != 2))
                            {
                                Console.WriteLine("\nInsira o tipo de chave: \n(1) - Telefone;\n(2) - CPF ");
                                try
                                {
                                    tipoDeChave = Convert.ToInt32(Console.ReadLine());
                                    invalidValue = false;
                                }
                                catch
                                {
                                    Console.WriteLine("Opção inválida");
                                    opcoesUsuario = 6;
                                }

                            }
                            if (tipoDeChave == 1)
                            {
                                tipoChaveString = "Telefone";
                            }
                            else
                            {
                                tipoChaveString = "CPF";
                            }
                            Console.WriteLine("\nInsira a chave:");
                            nomeChave = Console.ReadLine();
                            retorno2 = contaUsuario.AdicionarChave(nome, tipoChaveString, nomeChave);
                        }
                        Console.WriteLine(retorno2.Mensagem);
                        invalidValue = false;
                        Console.WriteLine("Pressione qualquer tecla para voltar ao Menu");
                        Console.ReadKey();
                        break;
                    case 3:
                        var chaves3 = contaUsuario.ListarChavesFavoritas().Result;

                        if (chaves3.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Você não tem chaves salvas\n");
                            Console.WriteLine("Pressione qualquer tecla para voltar ao Menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Chaves Salvas:\n");
                            foreach (var chave in chaves3)
                            {
                                Console.WriteLine($"Chave: {chave.Chave}\tNome do Titular: {chave.NomeTitular}\tQuantidade de usos: {chave.Quantidade}\tValor total: {FormataValores(chave.ValorTotal)}");
                            }
                        }
                        Console.WriteLine("\nPressione qualquer tecla para voltar ao Menu");
                        Console.ReadKey();
                        break;
                    case 4:
                        var chaves4 = contaUsuario.ListarChavesFavoritas().Result;
                        if (chaves4.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Você não tem chaves salvas para efetuar um PIX.\n");
                            Console.WriteLine("Pressione qualquer tecla para voltar ao Menu");
                            Console.ReadKey();
                            break;
                        }
                        foreach (var chave in chaves4)
                        {
                            Console.WriteLine($"Chave: {chave.Chave}");
                        }
                        Console.WriteLine("\nInforme a Chave que irá utilizar:");
                        var chavePix = Console.ReadLine();
                        Console.WriteLine("\nInforme o valor para o PIX:");
                        var valorPix = Console.ReadLine();

                        Retorno retorno4 = contaUsuario.EnviarPIX(chavePix, valorPix);
                        while (retorno4.Sucesso == false)
                        {
                            Console.Clear();
                            FormataMensagemErro(retorno4);
                            if (retorno4.CodRetorno == 70)
                            {
                                Console.WriteLine("Pressione qualquer tecla para voltar ao Menu");
                                Console.ReadKey();
                                break;
                            }

                            foreach (var chave in chaves4)
                            {
                                Console.WriteLine($"Chave: {chave.Chave}");
                            }
                            Console.WriteLine("\nInforme a Chave que irá utilizar:");
                            chavePix = Console.ReadLine();
                            Console.WriteLine("\nInforme o valor para o PIX:");
                            valorPix = Console.ReadLine();

                            retorno4 = contaUsuario.EnviarPIX(chavePix, valorPix);
                            break;
                        }
                        saldoUsuario = contaUsuario.Saldo;
                        Console.WriteLine(retorno4.Mensagem);
                        Console.WriteLine("Pressione qualquer tecla para voltar ao Menu");
                        Console.ReadKey();

                        break;
                    case 5:
                        var retorno5 = contaUsuario.Persistir();
                        if (retorno5.Sucesso)
                        {
                            Console.WriteLine(retorno5.Mensagem);
                        }
                        else
                        {
                            FormataMensagemErro(retorno5);
                        }
                        Console.WriteLine("Pressione qualquer tecla para voltar ao Menu");
                        Console.ReadKey();
                        break;
                    case 6:
                        break;
                    default:
                        opcoesUsuario = 6;
                        break;
                }
            }



        }
    }
}
