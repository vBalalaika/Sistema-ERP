using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Specification;
using ERP.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ERP.Application.Services.Commons
{
    public class DollarExchangeRateService : IDollarExchangeRateService
    {

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public DollarExchangeRateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected virtual DollarExchangeRate MapToEntity(DollarExchangeRateDTO entityDTO)
        {
            return _mapper.Map<DollarExchangeRate>(entityDTO);
        }

        public virtual async Task<int> CreateAsync(DollarExchangeRateDTO entityToCreateDTO)
        {
            DollarExchangeRate entityToCreate = MapToEntity(entityToCreateDTO);
            DollarExchangeRate entity = await _unitOfWork.GetRepository<DollarExchangeRate>().CreateAsync(entityToCreate);

            await _unitOfWork.Commit();
            return entity.Id;
        }

        // Hangfire job
        public async Task HFGetAndSaveDollarPrice()
        {
            try
            {
                // API: https://github.com/Castrogiovanni20/api-dolar-argentina
                //using (var httpClient = new HttpClient())
                //{
                //    using (var response = await httpClient.GetAsync("https://api-dolar-argentina.herokuapp.com/api/nacion"))
                //    {
                //        string apiResponse = await response.Content.ReadAsStringAsync();
                //        if (apiResponse != "")
                //        {
                //            DollarExchangeRate dollarExchangeRate = new DollarExchangeRate();
                //            dollarExchangeRate = JsonConvert.DeserializeObject<DollarExchangeRate>(apiResponse);
                //            var dollarExchangeRateDTO = _mapper.Map<DollarExchangeRateDTO>(dollarExchangeRate);
                //            var operationCreate = await CreateAsync(dollarExchangeRateDTO);
                //        }
                //    }
                //}

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://www.bna.com.ar/Cotizador/MonedasHistorico"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (apiResponse != "")
                        {
                            DateTime date = DateTime.Now;
                            decimal purchasePrice = 0;
                            decimal salePrice = 0;
                            DollarExchangeRate dollarExchangeRate = new DollarExchangeRate();

                            // Seteo las variables declaradas con la informacion consumida
                            date = DateTime.Parse(apiResponse.Split("Fecha: ", StringSplitOptions.None)[1].Split("</div>")[0].ToString().Trim().ToString());
                            purchasePrice = Convert.ToDecimal(apiResponse.Split("<td>Dolar U.S.A</td>", StringSplitOptions.None)[1].Trim().Substring(17, 8).Replace(".", ",").ToString());
                            salePrice = Convert.ToDecimal(apiResponse.Split("<td>Dolar U.S.A</td>", StringSplitOptions.None)[1].Trim().Substring(65, 8).Replace(".", ",").ToString());

                            dollarExchangeRate.Date = date;
                            dollarExchangeRate.PurchasePrice = purchasePrice;
                            dollarExchangeRate.SalePrice = salePrice;

                            var dollarExchangeRateDTO = _mapper.Map<DollarExchangeRateDTO>(dollarExchangeRate);
                            var operationCreate = await CreateAsync(dollarExchangeRateDTO);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public Task<DollarExchangeRate> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DollarExchangeRateDTO> GetByIdFullAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(DollarExchangeRateDTO entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DollarExchangeRate>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DollarExchangeRate>> FindWithSpecificationPattern(ISpecification<DollarExchangeRate> specification = null)
        {
            return await _unitOfWork.GetRepository<DollarExchangeRate>().FindAsync(specification);
        }
    }
}
