using API.Entities;
using API.Interfaces;
using API.Models.Request.Entries;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace API.Data
{
    public class EntryRepository : IEntryRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EntryRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<string> GetProjName()
        {
            var projects = _context.Entries
                .Select(x => x.ProjName).Distinct().ToList();

            return projects;
        }

        public List<string> GetPlatName()
        {
            var platforms = _context.Entries
                .Select(x => x.PlatName).Distinct().ToList();

            return platforms;
        }

        public List<string> GetStructType()
        {
            var platforms = _context.Entries
                .Select(x => x.StructType).Distinct().ToList();

            return platforms;
        }

        public List<string> GetStructArea()
        {
            var platforms = _context.Entries
                .Select(x => x.StructArea).Distinct().ToList();

            return platforms;
        }

        public List<string> GetPlatArea()
        {
            var platforms = _context.Entries
                .Select(x => x.PlatArea).Distinct().ToList();

            return platforms;
        }

        public List<string> GetSubArea()
        {
            var platforms = _context.Entries
                .Select(x => x.SubArea).Distinct().ToList();

            return platforms;
        }

        public List<string> GetMatType()
        {
            var platforms = _context.Entries
                .Select(x => x.MatType).Distinct().ToList();

            return platforms;
        }

        public List<string> GetMatVariant()
        {
            var platforms = _context.Entries
                .Select(x => x.MatVariant).Distinct().ToList();

            return platforms;
        }

        public List<string> GetProcMethod()
        {
            var platforms = _context.Entries
                .Select(x => x.ProcMethod).Distinct().ToList();

            return platforms;
        }

        public List<string> GetMatGroup()
        {
            var platforms = _context.Entries
                .Select(x => x.MatGroup).Distinct().ToList();

            return platforms;
        }

        public async Task<bool> Insert(InsertEntriesRequest insertEntriesRequest)
        {
            var entries = JsonConvert.DeserializeObject<List<MtoEntry>>(insertEntriesRequest.Entries);
            foreach (var entry in entries)
            {
                _context.Entries.Add(entry);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public List<dynamic> QueryBuilder(EntriesQueryBuilderRequest entriesQueryBuilderRequest)
        {
            var query = _context.Entries;
            IQueryable queryable = null;
            // turns user input into list
            
            entriesQueryBuilderRequest.sumColumn = entriesQueryBuilderRequest.sumColumn ?? new List<string>();
            entriesQueryBuilderRequest.selectColumn = entriesQueryBuilderRequest.selectColumn ?? new List<string>();
            entriesQueryBuilderRequest.selectedPlatforms =
                entriesQueryBuilderRequest.selectedPlatforms ?? new List<string>();
            entriesQueryBuilderRequest.selectedProject =
                entriesQueryBuilderRequest.selectedProject ?? new List<string>();
            // if selected columns are selected
            if (entriesQueryBuilderRequest.selectColumn.Count > 0)
            {
                // if sum columns are selected
                if (entriesQueryBuilderRequest.sumColumn.Count > 0)
                {
                    // create a new list called mergecolumns; mergecolumns is a list of list or basically a table
                    // so Add will add a new column while AddRange will add a new column with data
                    List<string> mergeColumns = new List<string>();
                    mergeColumns.AddRange(entriesQueryBuilderRequest.selectColumn);

                    if (entriesQueryBuilderRequest.selectedProject.Count > 0)
                    {
                        mergeColumns.Add("ProjName");
                    }

                    if (entriesQueryBuilderRequest.selectedPlatforms.Count > 0)
                    {
                        mergeColumns.Add("PlatName");
                    }

                    //mergeColumns.AddRange(entriesQueryBuilderRequest.sumColumn);
                    // groups each column to distinct items
                    mergeColumns = mergeColumns.Distinct().ToList();
                    // removes all data in sum columns selected because they aren't summed
                    mergeColumns.RemoveAll(x => entriesQueryBuilderRequest.sumColumn.Contains(x));
                    string groupByColumns = string.Join(",", mergeColumns);
                    string selectColumns = string.Join(",", mergeColumns.Select(x => $"Key.{x}").ToList());
                    string sumColumns = string.Join(",",
                        entriesQueryBuilderRequest.sumColumn.Select(x => $"SUM({x}) as Total_{x}").ToList());
                    queryable = query.GroupBy($"new ({groupByColumns})", "it")
                        .Select($"new ({selectColumns},{sumColumns})");
                }
                else
                {
                    if (entriesQueryBuilderRequest.selectedProject.Count > 0)
                    {
                        entriesQueryBuilderRequest.selectColumn.Add("ProjName");
                    }

                    entriesQueryBuilderRequest.selectColumn =
                        entriesQueryBuilderRequest.selectColumn.Distinct().ToList();
                    string selectColumns = string.Join(",", entriesQueryBuilderRequest.selectColumn);
                    queryable = query.Select("new { " + selectColumns + "}");
                }
            }
            else
            {
                // No columns selected
                if (entriesQueryBuilderRequest.sumColumn.Count > 0)
                {
                    List<string> mergeColumns = new List<string>();
                    mergeColumns.AddRange(entriesQueryBuilderRequest.sumColumn);

                    mergeColumns = mergeColumns.Distinct().ToList();
                    string sumColumns = string.Join(",", mergeColumns.Select(x => $"SUM({x})  as Total_{x}").ToList());
                    if (entriesQueryBuilderRequest.selectedProject.Count > 0)
                    {
                        queryable = query.GroupBy(x => 1).Select($"new (ProjName,{sumColumns})");
                    }
                    else
                    {
                        queryable = query.GroupBy(x => 1).Select($"new ({sumColumns})");
                    }
                }
                else
                {
                    queryable = query.Select("new {ProjName}");
                }
            }

            if (entriesQueryBuilderRequest.selectedProject.Count > 0)
            {
                queryable = queryable.Where("it.ProjName in @0", entriesQueryBuilderRequest.selectedProject)
                    .Where("it.PlatName in @0", entriesQueryBuilderRequest.selectedPlatforms);
            }

            return queryable.ToDynamicList();
        }
    }
}