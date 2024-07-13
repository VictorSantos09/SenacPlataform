using Microsoft.AspNetCore.Mvc;

namespace SenacPlataform.Shared.Controllers;

#region GetAll
/// <summary>
/// Representa um atributo que especifica uma solicitação HTTP GET para recuperar todos os dados do.
/// </summary>
/// <param name="template">O template para o endpoint.</param>
[AttributeUsage(AttributeTargets.Method)]
public class GetAll(string template = "") : HttpGetAttribute(template)
{
}
#endregion

#region GetById
/// <summary>
/// Representa um atributo que especifica uma solicitação HTTP GET para recuperar um recurso pelo seu ID.
/// </summary>
/// <param name="template">O template para o endpoint.</param>
[AttributeUsage(AttributeTargets.Method)]
public class GetById(string template = "{id}") : HttpGetAttribute(template)
{
}
#endregion

#region TestEndPoint
/// <summary>
/// Representa um atributo que especifica uma solicitação HTTP GET para um endpoint de teste.
/// </summary>
/// <param name="template">O template para o endpoint.</param>
[AttributeUsage(AttributeTargets.Method)]
public class TestEndPoint(string template = "/test") : HttpGetAttribute(template)
{
}
#endregion

#region Add
/// <summary>
/// Representa um atributo que é usado para adicionar um recurso usando o método HTTP POST.
/// </summary>
/// <param name="template">O template para o endpoint.</param>
[AttributeUsage(AttributeTargets.Method)]
public class Add(string template = "") : HttpPostAttribute(template)
{
}
#endregion

#region Update
/// <summary>
/// Representa um atributo que é usado para atualizar um recurso usando o método HTTP PUT.
/// </summary>
/// <param name="template">O template para o endpoint.</param>
[AttributeUsage(AttributeTargets.Method)]
public class Update(string template = "") : HttpPutAttribute(template)
{
}
#endregion

#region Delete
/// <summary>
/// Representa um atributo que é usado para deletar um recurso usando o método HTTP DELETE.
/// </summary>
/// <param name="template">O template para o endpoint.</param>
[AttributeUsage(AttributeTargets.Method)]
public class Delete(string template = "") : HttpDeleteAttribute(template)
{
}
#endregion
