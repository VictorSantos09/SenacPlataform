using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BancoTalentos.Domain.Services.Pessoas.Base;

public enum ProfessorErrorCode
{
    CargaHorariaDeveSerInformada = 1,
    NomeDeveSerInformado = 2,
    CargoDeveSerInformado = 3,
    CargoNaoEValido = 4,
    ProfessorNaoEncontrado = 5,
    ProfessorJaCadastrado = 6
}

public static class PessoaMessages
{
    public const string CARGA_HORARIA_DEVE_SER_INFORMADA = "Carga horária deve ser informada.";
    public const string NOME_DEVE_SER_INFORMADO = "Nome deve ser informado.";
    public const string CARGO_DEVE_SER_INFORMADO = "Cargo deve ser informado.";
    public const string CARGO_NAO_E_VALIDO = "Cargo não é válido.";
    public const string NAO_ENCONTRADO = "Não encontrado.";
    public const string JA_CADASTRADO = "Já cadastrado.";
    public const string NENHUM_ENCONTRADO = "Nenhum registro encontrado.";
    public const string NAO_FOI_POSSIVEL_CADASTRAR = "Não foi possível cadastrar.";
    public const string NAO_FOI_POSSIVEL_DELETAR = "Não foi possível deletar.";
    public const string NAO_FOI_POSSIVEL_ATUALIZAR = "Não foi possível atualizar.";
    public const string JA_TEM_HABILIDADE = "A habilidade já está informada.";
    public const string NAO_FOI_POSSIVEL_CADASTRAR_HABILIDADE = "Não foi possível cadastrar a habilidade.";
    public const string CARGA_HORARIA_EXCEDE_LIMITE = "A carga horária semanal não pode ser maior que 44 horas.";

    private static readonly Dictionary<ProfessorErrorCode, string> _errorMessages = CreateErrorMessages();

    private static Dictionary<ProfessorErrorCode, string> CreateErrorMessages()
    {
        var errorMessages = new Dictionary<ProfessorErrorCode, string>();
        var fields = typeof(PessoaMessages).GetFields(BindingFlags.Public | BindingFlags.Static)
                                              .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string));

        foreach (var field in fields)
        {
            if (Enum.TryParse(field.Name, out ProfessorErrorCode errorCode))
            {
                var message = (string)field.GetValue(null);
                errorMessages.Add(errorCode, message);
            }
        }

        return errorMessages;
    }

    public static string GetErrorMessage(ProfessorErrorCode errorCode)
    {
        return _errorMessages.TryGetValue(errorCode, out var message) ? message : "Mensagem de erro desconhecida.";
    }

    public static bool IsDefined(ProfessorErrorCode value)
    {
        return Enum.IsDefined(typeof(ProfessorErrorCode), value);
    }
}
