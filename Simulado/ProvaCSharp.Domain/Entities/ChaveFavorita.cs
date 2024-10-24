using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.ProvacSharp
{
    public class ChaveFavorita
    {
        public int Id { get; private set; }
        public string NomeTitular { get; private set; }
        public string Chave { get; private set; }
        public TiposChave TipoChave { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int Quantidade { get; private set; }

        public ChaveFavorita(string nomeTitular, string chave, string tipoChave)
        {
            NomeTitular = nomeTitular;
            if (!Enum.IsDefined(typeof(TiposChave), tipoChave))
            {
                throw new ArgumentException("Tipo de Chave inválido", nameof(tipoChave));
            }
            TipoChave = (TiposChave)Enum.Parse(typeof(TiposChave), tipoChave);
            Chave = chave;
            ValorTotal = 0;
            Quantidade = 0;
        }

        public void AdicionaValor(decimal value)
        {
            ValorTotal += value;
        }

        public void AdicionaUso()
        {
            Quantidade++;
        }
    }
}
