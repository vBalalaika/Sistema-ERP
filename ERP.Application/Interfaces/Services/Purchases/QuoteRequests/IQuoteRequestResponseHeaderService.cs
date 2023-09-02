﻿using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Interfaces.Services.Purchases.QuoteRequests
{
    public interface IQuoteRequestResponseHeaderService : IGenericService<QuoteRequestResponseHeader, QuoteRequestResponseHeaderDTO>
    {
    }
}
