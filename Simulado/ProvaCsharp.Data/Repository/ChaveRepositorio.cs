using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Bergs.ProvacSharp;
using Bergs.ProvacSharp.BD;
using ProvaCsharp.Data.Repository;
using ProvaCsharp.Data.DTO;

namespace Bergs.ProvacSharp
{
   public class ChaveRepositorio: IRepositorio<ChaveFavoritaDTO>
    {
        private readonly AcessoBancoDados _acessoBancoDados;

        public ChaveRepositorio(string database)
        {
            _acessoBancoDados = new AcessoBancoDados(database);
        }

        public Retorno Adicionar(List<ChaveFavoritaDTO> chaves)
        {
            try
            {
                //Deleta os dados antes
                _acessoBancoDados.Abrir();

                string sql = "DELETE FROM CHAVE";

                _acessoBancoDados.ExecutarDelete(sql);
            } catch
            {
                return new Retorno(false, 980, "Falha ao deletar registros.");
            }
            try {

                foreach (var chave in chaves)
                {
                    string sql = "INSERT INTO CHAVE (TIPO_CHAVE, NOME_TITULAR, QTDE_PIX, VLR_TOTAL_PIX, CHAVE) VALUES(@TipoChave, @NomeTitular, @QtdePix, @VlrTotalPix, @Chave)";

                    SqlCommand command = new SqlCommand(sql);
                    command.CommandType = CommandType.Text;

                    var tipoChave = new SqlParameter("@TipoChave", DbType.Int32);
                    tipoChave.Value = chave.Id;

                    var nomeTitular = new SqlParameter("@NomeTitular", DbType.String);
                    nomeTitular.Value = chave.NomeTitular;

                    var qntePix = new SqlParameter("@QtdePix", DbType.Int32);
                    qntePix.Value = chave.Quantidade;

                    var vlrTotalPix = new SqlParameter("@VlrTotalPix", DbType.Currency);

                    vlrTotalPix.Value = chave.ValorTotal;

                    var chavePix = new SqlParameter("@Chave", DbType.String);
                    chavePix.Value = chave.Chave;

                    command.Parameters.Add(tipoChave);
                    command.Parameters.Add(nomeTitular);
                    command.Parameters.Add(qntePix);
                    command.Parameters.Add(vlrTotalPix);
                    command.Parameters.Add(chavePix);

                    _acessoBancoDados.ExecutarInsert(command.GetGeneratedQuery());
                }
                _acessoBancoDados.EfetivarComandos();

            }
            catch
            {
                return new Retorno(false, 990, "Falha ao persistir registros.");
            }
            finally
            {
                _acessoBancoDados.Dispose();
            }
            return new Retorno(true, 0, "Chaves persistidas com sucesso.");

        }
    }
}
