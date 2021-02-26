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

        public List<string> GetProjects()
        {
            var projects = _context.Entries
                .Select(x => x.ProjName).Distinct().ToList();

            return projects;
        }

        public List<string> GetPlatforms()
        {
            var platforms = _context.Entries
                .Select(x => x.PlatNo).Distinct().ToList();

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
            entriesQueryBuilderRequest.sumColumn = entriesQueryBuilderRequest.sumColumn ?? new List<string>();
            entriesQueryBuilderRequest.selectColumn = entriesQueryBuilderRequest.selectColumn ?? new List<string>();
            entriesQueryBuilderRequest.selectedPlatforms =
                entriesQueryBuilderRequest.selectedPlatforms ?? new List<string>();
            entriesQueryBuilderRequest.selectedProject =
                entriesQueryBuilderRequest.selectedProject ?? new List<string>();
            if (entriesQueryBuilderRequest.selectColumn.Count > 0)
            {
                if (entriesQueryBuilderRequest.sumColumn.Count > 0)
                {
                    List<string> mergeColumns = new List<string>();
                    mergeColumns.AddRange(entriesQueryBuilderRequest.selectColumn);

                    if (entriesQueryBuilderRequest.selectedProject.Count > 0)
                    {
                        mergeColumns.Add("ProjName");
                    }

                    //mergeColumns.AddRange(entriesQueryBuilderRequest.sumColumn);
                    mergeColumns = mergeColumns.Distinct().ToList();
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
                queryable = queryable.Where("it.ProjName in @0", entriesQueryBuilderRequest.selectedProject);
            }

            return queryable.ToDynamicList();
        }
    }
}