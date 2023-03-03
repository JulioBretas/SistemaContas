using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentation.Models;

namespace SistemaContas.Presentation.Controllers
{

    [Authorize]
    public class UsuariosController : Controller
    {

        /// <summary>
        /// Método para abrir a página /Usuarios/MeusDados
        /// </summary>
        public IActionResult MeusDados()
        {
            return View();
        }


        /// <summary>
        /// Método para receber o SUBMIT do formulário
        /// </summary>
        [HttpPost]
        public IActionResult MeusDados(AlterarSenhaModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    //Obter dados do usuário atenticado
                    var usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(User.Identity.Name);

                    //Alterando senha do usuário no banco
                    var usuarioRepository = new UsuarioRepository();
                    usuarioRepository.Atualizar(usuarioModel.IdUsuario, model.NovaSenha);

                    TempData["MensagemSucesso"] = "Sua senha de acesso foi alterada com sucesso.";
                    ModelState.Clear();

                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao alterar senha: " + e.Message;
                }
            }

            return View();
        }
    }
}
