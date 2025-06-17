using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TaskTodo.Model.DTO;
using TaskTodo.Model.Entity;
using TaskTodo.Services.intreface;

namespace XUnitTesting.controller
{
    public class TaskUnitTest
    {
        private readonly Mock<ITask> _service;
        public TaskUnitTest()
        {
            _service = new Mock<ITask>();
        }

        [Fact]
        public void GetTask()
        {
            var data = new List<GetData>() {
                new GetData {Id=1,Title="Gaming",Description="1 hour",IsCompleted = false, CreatedDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),Flag = false},
                new GetData {Id=1,Title="reading",Description="book",IsCompleted = false, CreatedDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),Flag = true}
            };

            _service.Setup(x => x.GetAllTask()).ReturnsAsync((200, data, true));
            var result = _service.Object.GetAllTask().Result;

            Assert.Equal(200,result.StatusCode);
            Assert.NotNull(result.data);
            Assert.True(result.Result);
        }
        [Fact]
        public void AddTask()
        {
            var data = new AddTaskRequest
            {
                Title = "Outdoor activity",
                Description = "vollyball",
                DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893")
            };

            _service.Setup(x => x.AddTask(data)).ReturnsAsync((200, true));
            var result = _service.Object.AddTask(data).Result;

            Assert.Equal(200, result.StatusCode);
            Assert.True(result.Result);

        }
        [Fact]
        public void Getbyid()
        {
            var data = new TaskData
            {
                Id = 1,
                Title = "Hello",
                Description = "Hello testing here",
                IsCompleted = false,
                CreatedDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),
                DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),
                Flag = true
            };
            _service.Setup( x => x.GetTaskById(2)).ReturnsAsync((200, new List<TaskData> { data },true));
            var result = _service.Object.GetTaskById(2).Result;

            Assert.Equal(200, result.StatusCode);
            Assert.True(result.Result);
            Assert.NotNull(result.data);

        }
        [Fact]
        public void updateTask()
        {
            var data = new AddTaskRequest
            {
                Title = "Hello",
                Description = "tester data update",
                DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893")
            };
            _service.Setup(x => x.TaskUpdate(2, data)).ReturnsAsync((200, true));

            var result = _service.Object.TaskUpdate(2, data).Result;

            Assert.True(result.Result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void deleteTask()
        {
            _service.Setup(x => x.TaskDelete(2)).ReturnsAsync((200, true));

            var result = _service.Object.TaskDelete(2).Result;

            Assert.True(result.Result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void restoreTask()
        {
            _service.Setup(x => x.TaskRestore(2)).ReturnsAsync((200, true));

            var result = _service.Object.TaskRestore(2).Result;

            Assert.True(result.Result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void completeTask()
        {
            _service.Setup(x => x.TaskComplete(2)).ReturnsAsync((200, true));

            var result = _service.Object.TaskComplete(2).Result;    

            Assert.True(result.Result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void getAllDeleted()
        {
            var data = new List<TaskData>() {
                new TaskData {Id=1,Title="Gaming",Description="1 hour",IsCompleted = false, CreatedDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),Flag = false},
                new TaskData {Id=1,Title="reading",Description="book",IsCompleted = false, CreatedDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),Flag = true}
            };
            _service.Setup(x => x.GetAllDeletedTask()).ReturnsAsync((200, (IEnumerable<TaskData>)data, true));

            var result = _service.Object.GetAllDeletedTask().Result;

            Assert.True(result.Result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.data);
        }
        [Fact]
        public void deleteParmenetly()
        {
            _service.Setup(x => x.DeleteParmenetly(2)).ReturnsAsync((200, true));

            var result = _service.Object.DeleteParmenetly(2).Result;

            Assert.True(result.Result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void getExcel()
        {
            var data = new List<TaskData>() {
                new TaskData {Id=1,Title="Gaming",Description="1 hour",IsCompleted = false, CreatedDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),Flag = false},
                new TaskData {Id=1,Title="reading",Description="book",IsCompleted = false, CreatedDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),DueDate = DateTime.Parse("2025-05-30 13:15:01.2906893"),Flag = true}
            };
            _service.Setup(x => x.GetExcel()).ReturnsAsync((200, (IEnumerable<TaskData>)data, true));

            var result = _service.Object.GetExcel().Result;

            Assert.True(result.Result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
