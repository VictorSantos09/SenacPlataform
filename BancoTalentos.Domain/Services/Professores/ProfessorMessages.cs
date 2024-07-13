using SenacPlataform.Shared.Messages;

namespace BancoTalentos.Domain.Services.Professores;

public enum ProfessorErrorCode
{
    CargaHorariaDeveSerInformada = 1,
    NomeDeveSerInformado = 2,
    CargoDeveSerInformado = 3,
    CargoNaoEValido = 4,
    ProfessorNaoEncontrado = 5,
    ProfessorJaCadastrado = 6
}

public static class ProfessorMessages
{
    public const string CargaHorariaDeveSerInformada = "Carga horária deve ser informada.";
    public const string NomeDeveSerInformado = "Nome deve ser informado.";
    public const string CargoDeveSerInformado = "Cargo deve ser informado.";
    public const string CargoNaoEValido = "Cargo não é válido.";
    public const string ProfessorNaoEncontrado = "Professor não encontrado.";
    public const string ProfessorJaCadastrado = "Professor já cadastrado.";
    public const string NenhumProfessorEncontrado = "Nenhum professor encontrado.";
    public const string NaoFoiPossivelCadastrar = "Não foi possível cadastrar o professor.";
    public const string NaoFoiPossivelDeletar = "Não foi possível deletar o professor.";
    public const string NaoFoiPossivelAtualizar = "Não foi possível atualizar o professor.";
    public const string JaTemHabilidadeInformada = "O professor já tem a habilidade informada.";
    public const string NaoFoiPossivelCadastrarHabilidade = "Não foi possível cadastrar a habilidade do professor.";

    private static readonly Dictionary<ProfessorErrorCode, string> _errorMessages = new()
    {
            { ProfessorErrorCode.CargaHorariaDeveSerInformada, CargaHorariaDeveSerInformada },
            { ProfessorErrorCode.NomeDeveSerInformado, NomeDeveSerInformado },
            { ProfessorErrorCode.CargoDeveSerInformado, CargoDeveSerInformado },
            { ProfessorErrorCode.CargoNaoEValido, CargoNaoEValido },
            { ProfessorErrorCode.ProfessorNaoEncontrado, ProfessorNaoEncontrado },
            { ProfessorErrorCode.ProfessorJaCadastrado, ProfessorJaCadastrado }
        };

    public static string GetErrorMessage(ProfessorErrorCode errorCode) => MessageBase.GetErrorMessage(_errorMessages, errorCode);
    public static bool IsDefined(ProfessorErrorCode value) => MessageBase.IsDefined(value);
}