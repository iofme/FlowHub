using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoletoController : ControllerBase
    {
        [HttpPost("processar")]
        public IActionResult ProcessarBoleto([FromBody] ProcessarBoletoRequest request)
        {
            var boleto = BoletoReader.LerBoleto(request.Codigo!);

            if (boleto.IsValid)
            {
                return Ok(new
                {
                    sucesso = true,
                    dados = new
                    {
                        codigoBanco = boleto.CodigoBanco,
                        dataVencimento = boleto.DataVencimento.ToString("yyyy-MM-dd"),
                        valor = boleto.Valor,
                        nossoNumero = boleto.NossoNumero,
                        codigoBarras = boleto.CodigoBarras,
                        linhaDigitavel = boleto.LinhaDigitavel
                    }
                });
            }

            return BadRequest(new
            {
                sucesso = false,
                erro = boleto.Erro
            });
        }
    }

    public class ProcessarBoletoRequest
    {
        public string? Codigo { get; set; }
    }
}
