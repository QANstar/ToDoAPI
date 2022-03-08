using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Entity;
using ToDo.Model;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : Controller
    {
        QAN_TododbContext Context;
        public ToDoController(QAN_TododbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 获取待办数据
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult getToDoList()
        {
            var auth = HttpContext.AuthenticateAsync();
            var userName = auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Name))?.Value;
            IQueryable<ToDoVIew> toDoVIew = Context.ToDoVIew.Where(x => x.UserName == userName);
            return Ok(toDoVIew);
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult showType()
        {
            return Ok(Context.Type.ToList());
        }
        /// <summary>
        /// 获取优先级
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult showPriority()
        {
            return Ok(Context.Priority.ToList());
        }
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult showStatus()
        {
            return Ok(Context.Status.ToList());
        }
        /// <summary>
        /// 添加待办
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        [Authorize]
        public IActionResult addToDo(ToDoModel todo)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userName = auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Name))?.Value;
            ToDoList toDoList = new ToDoList();
            toDoList.UserName = userName;
            toDoList.Name = todo.Name;
            toDoList.Describe = todo.Describe;
            toDoList.Type = todo.Type;
            toDoList.Priority = todo.Priority;
            toDoList.StartDate = todo.StartDate;
            toDoList.EndDate = todo.EndDate;
            toDoList.State = todo.State;
            Context.ToDoList.Add(toDoList);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// 编辑待办
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPut]
        [Authorize]
        public IActionResult editToDo(ToDoModel todo)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userName = auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Name))?.Value;
            ToDoList toDoList = Context.ToDoList.FirstOrDefault(x => x.ID == todo.ID && x.UserName == userName);
            if (toDoList != null)
            {
                toDoList.Name = todo.Name;
                toDoList.Describe = todo.Describe;
                toDoList.Type = todo.Type;
                toDoList.Priority = todo.Priority;
                toDoList.StartDate = todo.StartDate;
                toDoList.EndDate = todo.EndDate;
                toDoList.State = todo.State;
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 删除待办
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpDelete]
        [Authorize]
        public IActionResult deleteToDo(int id)
        {
            var auth = HttpContext.AuthenticateAsync();
            var userName = auth.Result.Principal.Claims.First(t => t.Type.Equals(ClaimTypes.Name))?.Value;
            ToDoList toDoList = Context.ToDoList.FirstOrDefault(x => x.ID == id && x.UserName == userName);
            if (toDoList != null)
            {
                Context.ToDoList.Remove(toDoList);
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
