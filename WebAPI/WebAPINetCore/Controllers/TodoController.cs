﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPINetCore.Model;
using WebAPINetCore.Service;

namespace WebAPINetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        [HttpGet("TodoItems")]
        public IActionResult Index()
        {
            TodoService todoService = new TodoService();
            var result = todoService.GetAllTodoItem();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            TodoService todoService = new TodoService();
            var result = todoService.GetAllTodoItem().Find(m => m.id == id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(TodoItem item)
        {
            ///TODO: Post new item
            TodoService todoService = new TodoService();
            try
            {
                todoService.AddTodoItem(item);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(TodoItem item)
        {
            TodoService todoService = new TodoService();
            // var result = todoService.GetAllTodoItem().Where(m => m.id == item.id)
            //     .Select(c =>
            //     {
            //         c.summary = item.summary;
            //         c.temperatureC = item.temperatureC;
            //         c.temperatureF = item.temperatureF;
            //         c.date = item.date;
            //         return c;
            //     });
            ///TODO: Get a list with new result?
            try {
                todoService.EditTodoItem(item);
                return Ok();
            } catch {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TodoService todoService = new TodoService();
            var setItemToRemove = todoService.GetTodoItem(id);
            var result = todoService.GetAllTodoItem();
            result.RemoveAll(m => m.id == setItemToRemove.id);
            return Ok(result);
        }
    }
}