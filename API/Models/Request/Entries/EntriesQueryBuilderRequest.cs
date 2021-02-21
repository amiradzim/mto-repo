using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Request.Entries
{
    public class EntriesQueryBuilderRequest
    {
        public List<string> selectColumn { get; set; } = new List<string>();
        public List<string> sumColumn { get; set; } = new List<string>();
        public List<string> selectedProject { get; set; } = new List<string>();

    }
}
