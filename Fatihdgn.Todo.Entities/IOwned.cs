using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatihdgn.Todo.Entities;

public interface IOwned
{
    public TodoUserEntity? By { get; set; }
}
