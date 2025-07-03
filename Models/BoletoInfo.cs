using System.Text.RegularExpressions;


namespace API.Models
{
    public class BoletoInfo
    {
        public string? CodigoBanco { get; set; }
        public string? CodigoMoeda { get; set; }
        public string? DigitoVerificador { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal? Valor { get; set; }
        public string? NossoNumero { get; set; }
        public string? CodigoBarras { get; set; }
        public string? LinhaDigitavel { get; set; }
        public bool IsValid { get; set; }
        public string? Erro { get; set; }
    }
    public class BoletoReader
    {
        private static readonly DateTime DataBase = DateTime.Now;

        public static BoletoInfo LerBoleto(string codigo)
        {
            var boleto = new BoletoInfo();

            try
            {
                // Remove espaços e caracteres especiais
                codigo = Regex.Replace(codigo, @"[^0-9]", "");

                if (codigo.Length == 44)
                {
                    // Código de barras (44 dígitos)
                    boleto = ProcessarCodigoBarras(codigo);
                }
                else if (codigo.Length == 47)
                {
                    // Linha digitável (47 dígitos)
                    boleto = ProcessarLinhaDigitavel(codigo);
                }
                else
                {
                    boleto.IsValid = false;
                    boleto.Erro = "Código deve ter 44 ou 47 dígitos";
                }
            }
            catch (Exception ex)
            {
                boleto.IsValid = false;
                boleto.Erro = ex.Message;
            }

            return boleto;
        }

        private static BoletoInfo ProcessarCodigoBarras(string codigoBarras)
        {
            var boleto = new BoletoInfo();
            boleto.CodigoBarras = codigoBarras;

            // Validar DV do código de barras
            if (!ValidarDVCodigoBarras(codigoBarras))
            {
                boleto.IsValid = false;
                boleto.Erro = "Dígito verificador do código de barras inválido";
                return boleto;
            }

            // Extrair informações do código de barras
            boleto.CodigoBanco = codigoBarras.Substring(0, 3);
            boleto.CodigoMoeda = codigoBarras.Substring(3, 1);
            boleto.DigitoVerificador = codigoBarras.Substring(4, 1);

            // Data de vencimento (posições 5-8)
            var fatorVencimento = int.Parse(codigoBarras.Substring(5, 4));
            boleto.DataVencimento = DataBase.AddDays(fatorVencimento);

            // Valor (posições 9-18)
            var valorStr = codigoBarras.Substring(9, 10);
            boleto.Valor = decimal.Parse(valorStr) / 100;

            // Nosso número (posições 19-43)
            boleto.NossoNumero = codigoBarras.Substring(19, 25);

            // Converter para linha digitável
            boleto.LinhaDigitavel = CodigoBarrasParaLinhaDigitavel(codigoBarras);

            boleto.IsValid = true;
            return boleto;
        }

        private static BoletoInfo ProcessarLinhaDigitavel(string linhaDigitavel)
        {
            var boleto = new BoletoInfo();
            boleto.LinhaDigitavel = linhaDigitavel;

            // Validar linha digitável
            if (!ValidarLinhaDigitavel(linhaDigitavel))
            {
                boleto.IsValid = false;
                boleto.Erro = "Linha digitável inválida";
                return boleto;
            }

            // Converter linha digitável para código de barras
            boleto.CodigoBarras = LinhaDigitavelParaCodigoBarras(linhaDigitavel);

            return ProcessarCodigoBarras(boleto.CodigoBarras);
        }

        private static bool ValidarDVCodigoBarras(string codigoBarras)
        {
            var sequencia = "4329876543298765432987654329876543298765432";
            var codigo = codigoBarras.Substring(0, 4) + codigoBarras.Substring(5);

            int soma = 0;
            for (int i = 0; i < codigo.Length; i++)
            {
                soma += int.Parse(codigo[i].ToString()) * int.Parse(sequencia[i].ToString());
            }

            int resto = soma % 11;
            int dv = resto < 2 ? 0 : 11 - resto;

            return dv == int.Parse(codigoBarras[4].ToString());
        }

        private static bool ValidarLinhaDigitavel(string linhaDigitavel)
        {
            // Validar os 5 campos da linha digitável
            var campo1 = linhaDigitavel.Substring(0, 10);
            var campo2 = linhaDigitavel.Substring(10, 11);
            var campo3 = linhaDigitavel.Substring(21, 11);
            var campo4 = linhaDigitavel.Substring(32, 1);
            var campo5 = linhaDigitavel.Substring(33, 14);

            // Validar DV do campo 1
            var dv1 = CalcularDVModulo10(campo1.Substring(0, 9));
            if (dv1 != int.Parse(campo1[9].ToString()))
                return false;

            // Validar DV do campo 2
            var dv2 = CalcularDVModulo10(campo2.Substring(0, 10));
            if (dv2 != int.Parse(campo2[10].ToString()))
                return false;

            // Validar DV do campo 3
            var dv3 = CalcularDVModulo10(campo3.Substring(0, 10));
            if (dv3 != int.Parse(campo3[10].ToString()))
                return false;

            return true;
        }

        private static int CalcularDVModulo10(string codigo)
        {
            int soma = 0;
            bool multiplicaPor2 = true;

            for (int i = codigo.Length - 1; i >= 0; i--)
            {
                int digito = int.Parse(codigo[i].ToString());

                if (multiplicaPor2)
                {
                    digito *= 2;
                    if (digito > 9)
                        digito = (digito / 10) + (digito % 10);
                }

                soma += digito;
                multiplicaPor2 = !multiplicaPor2;
            }

            int resto = soma % 10;
            return resto == 0 ? 0 : 10 - resto;
        }

        private static string CodigoBarrasParaLinhaDigitavel(string codigoBarras)
        {
            // Campo 1: Código do banco + código da moeda + primeiros 5 dígitos do campo livre
            var campo1 = codigoBarras.Substring(0, 4) + codigoBarras.Substring(19, 5);
            var dv1 = CalcularDVModulo10(campo1);
            campo1 += dv1.ToString();

            // Campo 2: 6º ao 15º dígito do campo livre
            var campo2 = codigoBarras.Substring(24, 10);
            var dv2 = CalcularDVModulo10(campo2);
            campo2 += dv2.ToString();

            // Campo 3: 16º ao 25º dígito do campo livre
            var campo3 = codigoBarras.Substring(34, 10);
            var dv3 = CalcularDVModulo10(campo3);
            campo3 += dv3.ToString();

            // Campo 4: Dígito verificador do código de barras
            var campo4 = codigoBarras.Substring(4, 1);

            // Campo 5: Fator de vencimento + valor
            var campo5 = codigoBarras.Substring(5, 14);

            return campo1 + campo2 + campo3 + campo4 + campo5;
        }

        private static string LinhaDigitavelParaCodigoBarras(string linhaDigitavel)
        {
            var banco = linhaDigitavel.Substring(0, 3);
            var moeda = linhaDigitavel.Substring(3, 1);
            var dv = linhaDigitavel.Substring(32, 1);
            var fatorVencimento = linhaDigitavel.Substring(33, 4);
            var valor = linhaDigitavel.Substring(37, 10);

            var campoLivre = linhaDigitavel.Substring(4, 5) +
                           linhaDigitavel.Substring(10, 10) +
                           linhaDigitavel.Substring(21, 10);

            return banco + moeda + dv + fatorVencimento + valor + campoLivre;
        }
    }
}