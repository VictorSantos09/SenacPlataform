using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTalentos.Domain.Services.Pessoas.Base.Dto
{
    public record PessoaFormacoesDto
    {
        public int Id { get; set; }
        public int Id_Pessoa {  get; set; }
        public int Id_Formacao {  get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public TIPO_FORMACAO Tipo_Formacao { get; set; }
        public MODELO_ENSINO Modelo_Ensino {  get; set; }
    }
}
