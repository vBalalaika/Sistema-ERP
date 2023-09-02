﻿using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class ArchiveRepositoryAsync : GenericRepository<Archive>, IArchiveRepository
    {
        public ArchiveRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }
    }
}
