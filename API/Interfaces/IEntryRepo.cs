using API.Models.Request.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IEntryRepo
    {
        List<string> GetProjects();
        List<dynamic> queryBuilder(EntriesQueryBuilderRequest entriesQueryBuilderRequest);
        Task<bool> Insert(InsertEntriesRequest insertEntriesRequest);
    }
}
