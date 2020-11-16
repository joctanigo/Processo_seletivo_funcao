using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAtividadeEntrevista.Models
{
    public class Validation : ValidationAttribute, IClientValidatable
    {
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "validation"
            };
        }

        public Validation() { }

        public override bool IsValid(object value)
        {
            bool retorno = ChecarCPF(value.ToString());
            return retorno;
        }

        public static bool ChecarCPF(string CPF)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            CPF = CPF.Trim();
            CPF = CPF.Replace(".", "").Replace("-", "");

            if (CPF.Length != 11)
                return false;

            var tempCpf = CPF.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return CPF.EndsWith(digito);
        }

    }
}