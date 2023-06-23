using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatihdgn.Todo.Repositories;
public interface ITodoUserRepository : IRepository<TodoUserEntity, string> { }
