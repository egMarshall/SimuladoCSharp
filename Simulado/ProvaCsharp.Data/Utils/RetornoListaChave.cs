using Bergs.ProvacSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.ProvacSharp
{
    public class RetornoListaChave<T>: Retorno
    {
        public T Result { get; set; }

        public RetornoListaChave(bool sucesso, int codRetorno, string mensagem) : base(sucesso, codRetorno, mensagem) { }
    }
}
