using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTodo.Model.DTO;
using TaskTodo.Services;
using TaskTodo.Validation;

namespace TaskTodo.Controllers
{
    [EnableCors ("AllowLocalhost3000")]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITask _context;

        public TaskController(ITask context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            try
            {
                var result = await _context.GetAllTask();
                if (result.Result)
                {
                    return Ok(result.data);
                }
                else
                {
                    return BadRequest("problem in method");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(AddTaskRequest req)
        {
            try
            {
                var error = AddTaskValidation.Validate(req);
                if (error.Any())
                {
                    return BadRequest(error);
                }
                var res = await _context.AddTask(req);
                if (res.Result)
                {
                    return Ok("data added successfully");
                }
                else
                {
                    return BadRequest("problem in method");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("somthing went wrong");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
               
                var result = await _context.GetTaskById(id);
                if (result.Result)
                {
                    return Ok(result.data);
                }
                else
                {
                    return BadRequest("error in method");
                }

            }
            catch (Exception ex) { }
            return BadRequest("somthing went wrong");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] AddTaskRequest req)
        {
            try
            {
                var result = await _context.TaskUpdate(id, req);
                if (result.Result)
                {
                    return Ok("data updated successfully");
                }
                else
                {
                    return BadRequest("error in method");
                }
            }catch(Exception ex)
            {
                return BadRequest("somthing went wrong");
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var result = await _context.TaskDelete(id);
                if (result.Result)
                {
                    return Ok("data deleted successfully");
                }
                else
                {
                    return BadRequest("error in method");
                }
            }
            catch (Exception ex){
                return BadRequest("somthing went wrong");
            }
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> RestoreTask(int id)
        {
            try
            {
                var result = await _context.TaskRestore(id);
                if (result.Result)
                {
                    return Ok("data Restored successfully");
                }
                else
                {
                    return BadRequest("error in method");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("somthing went wrong");
            }
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            try
            {
                var result = await _context.TaskComplete(id);
                if (result.Result)
                {
                    return Ok("Task completed successfully");
                }
                else
                {
                    return BadRequest("error in method");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("somthing went wrong");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDeleted()
        {

            try
            {
                var result = await _context.GetAllDeletedTask();
                if (result.Result)
                {
                    return Ok(result.data);
                }
                else
                {
                    return BadRequest("problem in method");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
