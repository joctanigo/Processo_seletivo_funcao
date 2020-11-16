using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;


namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(Beneficiario beneficiario)
        {
            
            if (VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente)) throw new System.Exception("CPF já existe na base de dados.");
            var benef = new DaoBeneficiario();
            return benef.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(Beneficiario beneficiario)
        {
            var benef = new DaoBeneficiario();

            var benefs = benef.Consultar(beneficiario.Id);

            if (benefs.CPF != beneficiario.CPF)
                if (VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente)) throw new System.Exception("CPF já existe na base de dados.");

            benef.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public Beneficiario Consultar(long id)
        {
            var benef = new DaoBeneficiario();
            return benef.Consultar(id);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            var benef = new DaoBeneficiario();
            benef.Excluir(id);
        }

        /// <summary>
        /// Lista os beneficiarios
        /// </summary>
        public List<Beneficiario> Listar()
        {
            var benef = new DaoBeneficiario();
            return benef.Listar();
        }

        public List<Beneficiario> Listar(long idCliente)
        {
            var benef = new DaoBeneficiario();
            return benef.Listar(idCliente);
        }

        /// <summary>
        /// Lista os beneficiarios
        /// </summary>
        public List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            var benef = new DaoBeneficiario();
            return benef.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// Verifica Existencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long idCliente)
        {
            var benef = new DaoBeneficiario();
            return benef.VerificarExistencia(CPF, idCliente);
        }
    }
}
