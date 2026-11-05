using API_Alikin.Context;
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
        [ProducesResponseType(typeof(List<Task>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Task> Tasks = new TaskContext().Tasks;
                return Json(Tasks);
            }
            catch (Exception exp)
            {
                return StatusCode(500); 
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
        [ProducesResponseType(typeof(Task), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int Id)
        {
            try
            {
                Task Task = new TaskContext().Tasks.Where(x => x.Id == Id).First();
                return Json(Task);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }
    }
}
