using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.Models.Request.Entries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class EntriesController : BaseApiController
    {
        private readonly DataContext _context;
        readonly IEntryRepo _entryRepo;

        public EntriesController(DataContext context, IEntryRepo entryRepo)
        {
            _context = context;
            _entryRepo = entryRepo;
        }

        //[HttpPost("upload"), DisableRequestSizeLimit]
        //public async Task<IActionResult> Upload()
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        var file = formCollection.Files.First();
        //        var folderName = Path.Combine("Resources", "Uploads");   
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);
        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            string excelConnectionString = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties = Excel 8.0", fullPath);
        //            OleDbConnection connection = new OleDbConnection();
        //            connection.ConnectionString = excelConnectionString;
        //            OleDbCommand command = new OleDbCommand("select * from[Sheet1$]", connection);
        //            connection.Open();
        //            DbDataReader dr = command.ExecuteReader();
        //            string sqlConnectionString = @"Data source=mtorepository.db";
        //            SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
        //            bulkInsert.DestinationTableName = "Entries";
        //            bulkInsert.WriteToServer(dr);

        //            connection.Close();


        //            return Ok(new { dbPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}

        [HttpPost("insert")]
        public async Task<ActionResult<bool>> insert(InsertEntriesRequest insertEntriesRequest)
        {
            try
            {
                return await _entryRepo.Insert(insertEntriesRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<MtoEntry>>> GetEntries()
        {
            return await _context.Entries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MtoEntry>> GetEntry(int id)
        {
            return await _context.Entries.FindAsync(id);
        }

        [HttpGet("projects")]
        public ActionResult<List<string>> GetProjects()
        {
            return _entryRepo.GetProjects();
        }

        [HttpGet("platforms")]
        public ActionResult<List<string>> GetPlatforms()
        {
            return _entryRepo.GetPlatforms();
        }

        [HttpPost("queryBuilder")]
        public ActionResult<List<dynamic>> QueryBuilder(EntriesQueryBuilderRequest entriesQueryBuilderRequest)
        {
            return _entryRepo.QueryBuilder(entriesQueryBuilderRequest);
        }

        // [HttpGet("filter-select")]
        // public async Task<List<MtoEntry>> FilterSelect()
        // {
        //     IQueryable<MtoEntry> qry = from entry in _context.Entries
        //                             where entry.SubCostElement == "JACKET"
        //                             select new MtoEntry
        //                             {
        //                                 Id = entry.Id,
        //                                 CostElement = entry.CostElement,
        //                                 SubCostElement = entry.SubCostElement,
        //                                 Diameter = entry.Diameter
        //                             };

        //     return await qry.ToListAsync();
        // }
    }
}