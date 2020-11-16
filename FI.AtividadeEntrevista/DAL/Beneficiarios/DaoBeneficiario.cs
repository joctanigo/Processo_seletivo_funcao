using System.Collections.Generic;
using System.Data;
using System.Linq;
using FI.AtividadeEntrevista.DML;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiario
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new SqlParameter("IdCliente", beneficiario.IdCliente));
            parametros.Add(new SqlParameter("CPF", beneficiario.CPF));

            var ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Consulta por um beneficiario
        /// </summary>
        /// <param name="id">ID do beneficiario</param>
        internal Beneficiario Consultar(long Id)
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Id", Id));

            var ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            var cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF, long idCliente)
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("CPF", CPF));
            parametros.Add(new SqlParameter("IdCliente", idCliente));

            var ds = base.Consultar("FI_SP_VerificaBeneficiario", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new SqlParameter("quantidade", quantidade));
            parametros.Add(new SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new SqlParameter("crescente", crescente));

            var ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
            var cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os beneficiarios
        /// </summary>
        internal List<Beneficiario> Listar()
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Id", 0));

            var ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            var cli = Converter(ds);

            return cli;
        }

        internal List<Beneficiario> Listar(long idCliente)
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("IdCliente", idCliente));

            var ds = base.Consultar("FI_SP_ConsBeneficiariosCliente", parametros);
            var cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal void Alterar(Beneficiario beneficiario)
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new SqlParameter("IdCliente", beneficiario.IdCliente));
            parametros.Add(new SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new SqlParameter("ID", beneficiario.Id));

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }


        /// <summary>
        /// Excluir beneficiario
        /// </summary>
        /// <param name="id">ID do beneficiario</param>
        internal void Excluir(long Id)
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Id", Id));

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            var lista = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var cli = new Beneficiario();
                    cli.Id = row.Field<long>("Id");
                    cli.IdCliente = row.Field<long>("IdCliente");
                    cli.Nome = row.Field<string>("Nome");
                    cli.CPF = row.Field<string>("CPF");
                    lista.Add(cli);
                }
            }

            return lista;
        }
    }
}
