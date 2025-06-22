using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testapp.Models
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}