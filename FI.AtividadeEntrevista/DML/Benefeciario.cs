namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de beneficiário que representa o registro na tabela Beneficiario do Banco de Dados
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id Cliente
        /// </summary>
        public long IdCliente { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Cliente
        /// </summary>
        public Cliente Cliente { get; set; }
    }
}
