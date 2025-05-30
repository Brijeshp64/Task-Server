using TaskTodo.DAL.AccessService;
using TaskTodo.Model.DTO;
using TaskTodo.Model.Entity;

namespace TaskTodo.Services
{
    public class TaskService : ITask
    {
        private readonly ITaskAuth _context;
        public TaskService(ITaskAuth context)
        {
            _context = context;
        }



        public async Task<(int StatusCode, IEnumerable<TaskData> data, bool Result)> GetAllTask()
        {
            try
            {
                var result = await _context.GetAllAsync();

                var RES = result
                          .Where(c => !c.IsCompleted)
                          .Where(c => !c.Flag)
                          .ToList();
                return (200, RES, true);
            }
            catch (Exception ex)
            {
                return (500, null, false);

            }

        }
        public async Task<(int StatusCode, bool Result)> AddTask(AddTaskRequest req)
        {
            try
            {
                var data = new TaskData
                {
                    Title = req.Title,
                    Description = req.Description,
                    IsCompleted = false,
                    DueDate = req.DueDate,
                    Flag = false,
                };
                await _context.AddData(data);
                return (200, true);
            }
            catch (Exception ex)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, List<TaskData> data, bool Result)> GetTaskById(int id)
        {
            try
            {
                var result = await _context.GetByIdAsync(id);
                return (200, new List<TaskData> { result } , true);
            }
            catch (Exception ex)
            {
                return (500, null, false);

            }
        }

        public async Task<(int StatusCode, bool Result)> TaskUpdate(int id, AddTaskRequest req)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);

                data.Title = req.Title ?? data.Title;
                data.Description = req.Description ?? data.Description;
                data.DueDate = req.DueDate ?? data.DueDate;

                await _context.UpdateAsync(data);
                return (200, true);
            }catch(Exception ex)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, bool Result)> TaskDelete(int id)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);

                data.Flag = true;
                await _context.UpdateAsync(data);

                return (200, true);
            }
            catch (Exception)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, bool Result)> TaskRestore(int id)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);

                data.Flag = false;
                await _context.UpdateAsync(data);

                return (200, true);
            }
            catch (Exception)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, bool Result)> TaskComplete(int id)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);

                data.IsCompleted = true;
                await _context.UpdateAsync(data);

                return (200, true);
            }
            catch (Exception)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, IEnumerable<TaskData> data, bool Result)> GetAllDeletedTask()
        {
            try
            {
                var result = await _context.GetAllAsync();

                var RES = result
                          .Where(c => !c.IsCompleted && c.Flag)
                          .ToList();
                return (200, RES, true);
            }
            catch (Exception ex)
            {
                return (500, null, false);

            }
        }
    }
}
