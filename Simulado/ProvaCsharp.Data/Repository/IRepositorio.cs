using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.ProvacSharp
{
    public interface IRepositorio<T>
    {
        void Adicionar(T item);
        void DeletarTodos();
    }
}
