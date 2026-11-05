using API_Alikin.Context;
using API_Alikin.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Alikin.Controllers
{
    [Route("api/UsersController")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        ///<returns>Данный метод предназначен для авторизации пользователя на сайте</returns>
        /// <response code="200">Пользователь успешно авторизован</response>
        /// <response code="401">Пользователь не найден</response>
        /// <response code="403">Ошибка запроса, данные не указаны</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("SingIn")] 
        [HttpPost] 
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult SingIn([FromForm] string Login, [FromForm] string Password)
        {
            if (Login == null || Password == null)
                return StatusCode(403);

            try
            {
                Users User = new UsersContext().Users.Where(x => x.Login == Login && x.Password == Password).FirstOrDefault();
                if (User == null)
                {
                    return StatusCode(401, "Пользователь не найден");
                }
                return Json(User);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }


        ///<summary>
        ///Регистрация нового пользователя
        ///</summary>
        ///<remarks>
        ///Данный метод проверяет уникальность логина и сохраняет нового пользователя в базе данных.
        ///Если пользователь успешно создан, возвращаются его данные (без пароля в идеале, но здесь вернем объект целиком).
        ///</remarks>
        /// <param name="Login">Уникальный логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <response code="201">Пользователь успешно зарегистрирован</response>
        /// <response code="400">Ошибка запроса: пустые поля или неверный формат</response>
        /// <response code="409">Конфликт: такой логин уже существует</response>
        /// <response code="500">Внутренняя ошибка сервера при работе с БД</response>
        [Route("RegIn")] 
        [HttpPost]       
        [ProducesResponseType(typeof(Users), 201)] 
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public ActionResult RegIn([FromForm] string Login, [FromForm] string Password)
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                return BadRequest("Логин и пароль обязательны для заполнения.");
            }

            try
            {
                using (var context = new UsersContext())
                {
                    bool userExists = context.Users.Any(x => x.Login == Login);

                    if (userExists)
                    {
                        return StatusCode(409, $"Пользователь с логином '{Login}' уже существует.");
                    }

                    var newUser = new Users
                    {
                        Login = Login,
                        Password = Password
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    return CreatedAtAction(nameof(SingIn), new { id = newUser.Id }, newUser);

                }
            }
            catch (Exception exp)
            {
                Console.WriteLine($"[ERROR RegIn]: {exp.Message}");
                return StatusCode(500, $"DB Error: {exp.InnerException?.Message ?? exp.Message}");
            }
        }
    }
}
