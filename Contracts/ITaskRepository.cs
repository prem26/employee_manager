using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface ITaskRepository: IRepositoryBase<Task>
    {
        IEnumerable<Task> GetAllTasks();
        Task GetTaskById(Guid taskId);
        void CreateTask(Task task);
        void UpdateTask(Task dbTask, Task task);
        List<Task> ListTasksByUserId(string userId);
    }
}
