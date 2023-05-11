using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Task1.Repository;
using Task1.Data;
using Task1.Models;
using Task1.ViewModel;

namespace TodoApp.Repository.MsSQL
{
    public class TodoRestRepository : ITodoRepo
    {
        // interact with database and perform CRUD 

        string baseURL = "https://jsonplaceholder.typicode.com";
       
        HttpClient httpClient = new HttpClient();
        
        public TodoRestRepository()
        {
        }

        public Todo AddTodo(Todo newTodo)
        {

            TodoViewModel todoVM = new TodoViewModel();
            todoVM.Title = newTodo.Title;
            todoVM.completed = newTodo.completed;
  

            string data = JsonConvert.SerializeObject(todoVM);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync(baseURL + "/todos", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responsecontent = response.Content.ReadAsStringAsync().Result;
                Todo todo = JsonConvert.DeserializeObject<Todo>(responsecontent);
                return todo;
            }
            return null;
        }

        public Todo DeleteTodo(int todoId)
        {
            var response = httpClient.DeleteAsync(baseURL + "/todos/" + todoId).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;// json standard
                Todo todo = JsonConvert.DeserializeObject<Todo>(data);
                return todo;
            }
            return null;
        }

        public List<Todo> GetAllTodos()
        {
            var response = httpClient.GetAsync(baseURL + "/todos").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;// json standard
                List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(data);
                return todos;
            }
            return null;
        }

        public Todo GetTodoById(int Id)
        {
            var response = httpClient.GetAsync(baseURL + "/todos/"+Id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;// json standard
                Todo todo = JsonConvert.DeserializeObject<Todo>(data);
                return todo;
            }
            return null;
        }

        public Todo UpdateTodo(int todoId, Todo newTodo)
        {
            string data = JsonConvert.SerializeObject(newTodo);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = httpClient.PutAsync(baseURL + "/todos/"+todoId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responsecontent = response.Content.ReadAsStringAsync().Result;
                Todo todo = JsonConvert.DeserializeObject<Todo>(responsecontent);
                return todo;
            }
            return null;
        }
    }
}
