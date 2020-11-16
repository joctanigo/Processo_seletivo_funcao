using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            
         
            

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                
                model.Id = bo.Incluir(new Cliente()
                {                    
                    CEP = model.CEP,
                    CPF = model.CPF,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                });
                ViewBag.IdCliente = model.Id;

                if (model.Id == 0)
                {
                    return Json("O CPF informado já foi cadastrado, favor informar outro CPF válido.");
                }
             return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var check = bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    CPF = model.CPF,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                });
               
                if (check)
                    return Json("O CPF informado já foi cadastrado, favor informar outro CPF válido.");

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            ViewBag.IdCliente = id;
            BoCliente bocliente = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();
            List< Beneficiario> listaBeneficiarios = boBeneficiario.Listar(id);
            Cliente cliente = bocliente.Consultar(id);
            cliente.Beneficiarios = listaBeneficiarios;
            Models.ClienteModel model = new ClienteModel();

            ViewBag.Beneficiarios = cliente.Beneficiarios.Count > 0
               ? cliente.Beneficiarios.Select(p => new BeneficiarioModel
               {
                   CPF = p.CPF,
                   Id = p.Id,
                   IdCliente = p.IdCliente,
                   Nome = p.Nome
               }).ToList()
               : new List<BeneficiarioModel>();

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    CPF = cliente.CPF,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                };

            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult IncluirBeneficiario(string idcliente, string idbenef, string bCPF, string bNome)
        {
            var model = new BeneficiarioModel
            {
                Id = string.IsNullOrEmpty(idbenef.ToString()) ? 0 : Convert.ToInt64(idbenef),
                IdCliente = Convert.ToInt64(idcliente),
                CPF = bCPF,
                Nome = bNome
            };

            var bo = new BoBeneficiario();
            var boCliente = new BoCliente();

            if (!ModelState.IsValid)
            {
                var erros = (from item in ModelState.Values
                             from error in item.Errors
                             select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (model.Id == 0)
                {
                    model.Id = bo.Incluir(new Beneficiario
                    {
                        IdCliente = model.IdCliente,
                        Nome = model.Nome,
                        CPF = model.CPF
                    });
                }
                else
                {
                    bo.Alterar(new Beneficiario
                    {
                        IdCliente = model.IdCliente,
                        Nome = model.Nome,
                        CPF = model.CPF,
                        Id = model.Id
                    });
                }

                ViewBag.IdCliente = model.IdCliente;
                var cliente = boCliente.Consultar(model.IdCliente);
                ViewBag.Beneficiarios = cliente.Beneficiarios.Count > 0
                ? cliente.Beneficiarios.Select(p => new BeneficiarioModel
                {
                    CPF = p.CPF,
                    Id = p.Id,
                    IdCliente = p.IdCliente,
                    Nome = p.Nome
                }).ToList()
                : new List<BeneficiarioModel>();

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult ExcluirBeneficiario(string id)
        {
            var bo = new BoBeneficiario();
            var boCliente = new BoCliente();

            if (!ModelState.IsValid)
            {
                var erros = (from item in ModelState.Values
                             from error in item.Errors
                             select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var beneficiario = bo.Consultar(Convert.ToInt64(id));
                if (beneficiario != null)
                {
                    var idCliente = beneficiario.IdCliente;
                    bo.Excluir(Convert.ToInt64(id));
                    ViewBag.IdCliente = idCliente;
                    var cliente = boCliente.Consultar(idCliente);
                    ViewBag.Beneficiarios = cliente.Beneficiarios.Count > 0
                    ? cliente.Beneficiarios.Select(p => new BeneficiarioModel
                    {
                        CPF = p.CPF,
                        Id = p.Id,
                        IdCliente = p.IdCliente,
                        Nome = p.Nome
                    }).ToList()
                    : new List<BeneficiarioModel>();
                }

                return Json("Cadastro removido com sucesso");
            }
        }

    }
}