using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPINetCore.Model;

namespace WebAPINetCore.Service
{
    public class TodoService
    {
        static string source = "Source/resource.json";
        public List<TodoItem> GetAllTodoItem()
        {
            using (StreamReader read = new StreamReader(source))
            {
                string json = read.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TodoItem>>(json);
            }
        }

        public TodoItem GetTodoItem(int id)
        {
            using (StreamReader read = new StreamReader(source))
            {
                string json = read.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<TodoItem>>(json);
                return items.Where(m => m.id == id).FirstOrDefault();
            }
        }

        public TodoItem AddTodoItem(TodoItem item)
        {
            using (StreamReader read = new StreamReader(source))
            {
                string json = read.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<TodoItem>>(json);
                item.id = items.Count + 1;
                items.Add(item);
                var convertedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
                try {
                    File.WriteAllText(source, convertedJson);
                    Console.WriteLine("Save to json");
                } catch (Exception e) {
                    Console.WriteLine("Can't save to json " + e);
                }
                return item;
            }
        }

        public TodoItem EditTodoItem(TodoItem item) {
            using (StreamReader read = new StreamReader(source))
            {
                string json = read.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<TodoItem>>(json);
                items.Where(m => m.id == item.id).FirstOrDefault().date = item.date;
                items.Where(m => m.id == item.id).FirstOrDefault().temperatureC = item.temperatureC;
                items.Where(m => m.id == item.id).FirstOrDefault().temperatureF = item.temperatureF;
                items.Where(m => m.id == item.id).FirstOrDefault().summary = item.summary;
                var editItem = items.Where(m => m.id == item.id).FirstOrDefault();
                var convertedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
                try {
                    File.WriteAllText(source, convertedJson);
                    Console.WriteLine("Edited json data");
                } catch (Exception e) {
                    Console.WriteLine("Can't edit json data " + e);
                }
                return editItem;
            }
        }
    }
}
