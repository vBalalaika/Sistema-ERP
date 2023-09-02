using ERP.Application.DTOs;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Shared;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories
{
    public class LogRepository : GenericRepository<Audit>, ILogRepository
    {
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly ApplicationDbContext _context;

        public LogRepository(ApplicationDbContext context, IMapper mapper, IDateTimeService dateTimeService) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task AddLogAsync(string action, string userId)
        {
            var audit = new Audit()
            {
                Type = action,
                UserId = userId,
                DateTime = _dateTimeService.NowUtc
            };
            await base.CreateAsync(audit);
        }

        public async Task<List<AuditLogResponse>> GetAuditLogsAsync(string userId)
        {
            var logs = await _context.Set<Audit>().Where(a => a.UserId == userId).OrderByDescending(a => a.Id).Take(250).ToListAsync();
            var mappedLogs = _mapper.Map<List<AuditLogResponse>>(logs);
            return mappedLogs;
        }
        public async Task<List<AuditLogResponse>> GetAllAuditLogsAsync(string tableName, string type, string oldValue, string newValue, string affectedColumn)
        {
            var logs = await _context.Set<Audit>().Where(a => a.TableName == tableName && a.Type == type && a.OldValues.Contains(oldValue) && a.NewValues.Contains(newValue) && a.AffectedColumns.Contains(affectedColumn)).OrderByDescending(a => a.Id).ToListAsync();
            var mappedLogs = _mapper.Map<List<AuditLogResponse>>(logs);
            return mappedLogs;
        }

    }

    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<AuditLogResponse, Audit>().ReverseMap();
        }
    }
}