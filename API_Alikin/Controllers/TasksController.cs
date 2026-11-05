using API_Alikin.Context;
using API_Alikin.Model;
using Microsoft.AspNetCore.Mvc;


namespace API_Alikin.Controllers
{
    [Route("api/TasksController")] // указываем раздел
    public class TasksController : Controller
    {

        ///<summary>
        ///Получение списка задач
        ///</summary>
        ///<remarks>Данный метод получает список задач, находящийся в базе данных</remarks>
        /// <response code="200">Список успешно получен</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("List")] 
        [HttpGet] 
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Tasks> Tasks = new TaskContext().Tasks;
                return Json(Tasks);
            }
            catch (Exception exp)
            {
                Console.WriteLine("=== ОШИБКА ===");
                Console.WriteLine(exp.ToString());

                // Возвращаем сообщение клиенту
                return StatusCode(500, $"DB Error: {exp.InnerException?.Message ?? exp.Message}");
            }
        }


        ///<summary>
        ///Получение задачи
        ///</summary>
        ///<remarks>Данный метод получает задачу, находящуюся в базе данных</remarks>
        /// <response code="200">Задача успешно получена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("Item")]
        [HttpGet] 
        [ProducesResponseType(typeof(Tasks), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int Id)
        {
            try
            {
                Tasks Task = new TaskContext().Tasks.Where(x => x.Id == Id).First();
                return Json(Task);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }

        ///<summary>
        ///Получение задачи под id 1 
        ///</summary>
        ///<remarks>Данный метод получает задачу, находящуюся в базе данных</remarks>
        /// <response code="200">Задача успешно получена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("1")]
        [HttpGet]
        [ProducesResponseType(typeof(Tasks), 200)]
        [ProducesResponseType(500)]
        public ActionResult SelectItem_1()
        {
            try
            {
                Tasks Task = new TaskContext().Tasks.Where(x => x.Id == 1).First();
                return Json(Task);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }

        ///<summary>
        ///Получение задачи под определенным названием 
        ///</summary>
        ///<remarks>Данный метод получает задачу, находящуюся в базе данных</remarks>
        /// <response code="200">Задача успешно получена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("Search")]
        [HttpGet]
        [ProducesResponseType(typeof(Tasks), 200)]
        [ProducesResponseType(500)]
        public ActionResult Search(string Name)
        {
            try
            {
                var results = new TaskContext().Tasks.Where(x => x.Name.Contains(Name)).ToList();

                return Json(results);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }
    }
}
