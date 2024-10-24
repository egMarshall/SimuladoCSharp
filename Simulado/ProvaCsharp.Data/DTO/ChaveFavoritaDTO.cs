using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCsharp.Data.DTO
{
    public class ChaveFavoritaDTO
    {
        public int Id { get; set; }
        public string NomeTitular { get; set; }
        public string Chave { get; set; }
        public string TipoChave { get; set; }
        public decimal ValorTotal { get; set; }
        public int Quantidade { get; set; }


        public ChaveFavoritaDTO(string nomeTitular, string chave, string tipoChave, decimal valorTotal, int quantidade)
        {
            Chave = chave;
            NomeTitular = nomeTitular;
            TipoChave = tipoChave;
            ValorTotal = valorTotal;
            Quantidade = quantidade;
        }
    }
}
