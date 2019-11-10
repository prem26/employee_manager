using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;


namespace Repository
{
    public class TaskRepository :RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public IEnumerable<Task> GetAllTasks()
        {
            return FindAll().OrderBy(t => t.Created);
        }

        public Task GetTaskById(Guid taskId)
        {
            return FindByCondition(task => task.Id.Equals(taskId))
                .DefaultIfEmpty(new Task())
                .FirstOrDefault();
        }

        public void CreateTask(Task task)
        {
            task.Id = Guid.NewGuid();
            task.Created = DateTime.Now;
            task.Updated = DateTime.Now;
            Create(task);
            Save();
        }

        public void UpdateTask(Task dbTask, Task task)
        {
            dbTask.Map(task);
            Update(dbTask);
        }

        public List<Task> ListTasksByUserId(string userId)
        {
            var userTasksList = new List<Task>();
            var userTasks = FindByCondition(t => t.UserId.Equals(userId));
            foreach (var t in userTasks)
            {
                var task = new Task();
                task.Map(t);
                userTasksList.Add(task);
            }

            return userTasksList;
        }


    }
}
