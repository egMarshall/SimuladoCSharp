﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.ProvacSharp
{
    public class Retorno
    {
        public bool Sucesso { get; set; }
        public int CodRetorno { get; set; }
        public string Mensagem { get; set; }

        public Retorno(bool sucesso, int codRetorno, string mensagem)
        {
            Sucesso = sucesso;
            CodRetorno = codRetorno;
            Mensagem = mensagem;
        }
    }
}