using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvaCsharp.Data.DTO;

namespace Bergs.ProvacSharp
{
    public class Conta
    {
        public decimal Saldo { get; set; }
        List<ChaveFavorita> Chaves { get; set; }

        public Conta()
        {
            Chaves = new List<ChaveFavorita>();
            Saldo = 0;
        }

        public Retorno CreditarConta(string strValor)
        {

            if (!decimal.TryParse(strValor, out decimal formatedValue))
            {
                return new Retorno(false, 10, "Valor Inválido.");
            }

            if (formatedValue <= 0)
            {
                return new Retorno(false, 20, "Valor menor ou igual a zero.");
            }

            Saldo += formatedValue;

            return new Retorno(true, 00, "Crédito efetuado com sucesso.");
        }

        public Retorno AdicionarChave(string strNome, string strTipo, string strChave)
        {
            string regex;

            if (strChave.Length == 11)
            {
                regex = "^\\d{11}$";
            } else if (strChave.Length == 14)
            {
                regex = "^\\+[1-9][0-9]\\d{11}$";
            } 
            else
            {
                return new Retorno(false, 30, "Tipo de chave inválido.");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(strChave, regex))
            {
                if (strChave.Length == 11)
                {
                    return new Retorno(false, 40, "CPF Inválido");
                } else
                {
                    return new Retorno(false, 50, "Telefone Inválido");
                }
            }

            var chaveExistente = Chaves.FirstOrDefault(c => c.Chave == strChave);

            if (chaveExistente != null)
            {
                return new Retorno(false, 65, "Chave duplicada");
            }

            var chaveUsuario = new ChaveFavorita(strNome, strChave, strTipo);
            Chaves.Add(chaveUsuario);

            return new Retorno(true, 00, "Chave adicionada com sucesso");
        }

        public RetornoListaChave<List<ChaveFavorita>> ListarChavesFavoritas()
        {
            var retorno = new RetornoListaChave<List<ChaveFavorita>>(true, 00, "Sucesso.");

            retorno.Result = Chaves;

            return retorno;
        }

        public Retorno EnviarPIX(string strChave, string strValor)
        {
            if (!decimal.TryParse(strValor, out decimal valorFormatado))
            {
                return new Retorno(false, 10, "Valor Inválido.");
            }

            if (valorFormatado <= 0)
            {
                return new Retorno(false, 20, "Valor menor ou igual a zero.");
            }

            if (valorFormatado > Saldo)
            {
                return new Retorno(false, 70, "Saldo Insuficiente.");
            }

            var chaveExistente = Chaves.FirstOrDefault(c => c.Chave == strChave);

            if (chaveExistente == null)
            {
                return new Retorno(false, 60, "Chave inexistente.");
            }

            chaveExistente.AdicionaValor(valorFormatado);
            chaveExistente.AdicionaUso();
            Saldo -= valorFormatado;

            return new Retorno(true, 00, "PIX enviado com sucesso.");
        }

        public Retorno Persistir()
        {
            var caminhoDoBanco = @"C:\Soft\pxc\data\Pxcz02da.mdb";

            var repositorio = new ChaveRepositorio(caminhoDoBanco);

            var listaChavesDTO = new List<ChaveFavoritaDTO>();

            foreach(var chave in Chaves)
            {
                var chaveDTO = new ChaveFavoritaDTO(chave.NomeTitular, chave.Chave, chave.TipoChave.ToString(), chave.ValorTotal, chave.Quantidade);
                listaChavesDTO.Add(chaveDTO);
            }

            return repositorio.Adicionar(listaChavesDTO);
        }

    }
}
