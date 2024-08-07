﻿using BackEnd.DTOs;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
    public class BPKBFacade : IBpkb
    {
        private readonly DataContext _context;
        public BPKBFacade(DataContext context)
        {
            _context = context;
        }
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }
        public async Task<TrBpkb> GetById(string aggreementNum)
        {
            var results = await _context.TrBpkbs.FirstOrDefaultAsync(x => x.AgreementNumber == aggreementNum);
            if(results == null) throw new Exception($"Data NotFound");
            return results;
        }
        public Task<IEnumerable<TrBpkb>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TrBpkb>> GetAll()
        {
            try
            {
                var results = await _context.TrBpkbs.OrderBy(s => s.AgreementNumber).ToListAsync();
                foreach (var result in results) 
                {
                    result.CreatedOn.ToString("MM/dd/yyyy");
				}
                return results;
            }            
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<BaseResponse> Insert(TrBpkb obj)
        {
            try
            {
                BaseResponse result = new();
                var updateBPKB = await _context.TrBpkbs.FirstOrDefaultAsync(x => x.AgreementNumber == obj.AgreementNumber);
                var getLocation = await _context.MsStorageLocations.FirstOrDefaultAsync(x => x.LocationId == obj.LocationId);
                if (getLocation == null)
                {
                    result.IsSucceeded = false;
                    result.Message = $"Location with ID : {obj.LocationId} Not found";
                    return result;
                }
                if (updateBPKB != null)
                {
                    result.IsSucceeded = false;
                    result.Message = $"Agreement Number : {obj.AgreementNumber} Already Taken";
                    return result;
                }
                else
                {
                    obj.LastUpdatedBy = obj.CreatedBy;
                    _context.TrBpkbs.Add(obj);
                    await _context.SaveChangesAsync();
                    result.IsSucceeded = true;
                    result.Message = $"Data Successfully Updated";
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<BaseResponse> Update(TrBpkb obj)
        {
            try
            {
                BaseResponse result = new();
                var updateBPKB = await _context.TrBpkbs.FirstOrDefaultAsync(x => x.AgreementNumber == obj.AgreementNumber);
                var getLocation = await _context.MsStorageLocations.FirstOrDefaultAsync(x => x.LocationId == obj.LocationId);
                if (getLocation == null)
                {
                    result.IsSucceeded = false;
                    result.Message = $"Location with ID : {obj.LocationId} Not found";
                    return result;
                }
                if (updateBPKB == null)
                {
                    result.IsSucceeded = false;
                    result.Message = $"Agreement Number : {obj.AgreementNumber} Not found";
                    return result;
                }
                else
                {
                    updateBPKB.BpkbNo = obj.BpkbNo;
                    updateBPKB.BranchId = obj.BranchId;
                    updateBPKB.BpkbDateIn = obj.BpkbDateIn;
                    updateBPKB.BpkbDate = obj.BpkbDate;
                    updateBPKB.FakturNo = obj.FakturNo;
                    updateBPKB.FakturDate = obj.FakturDate;
                    updateBPKB.PoliceNo = obj.PoliceNo;
                    updateBPKB.LocationId = obj.LocationId;
                    updateBPKB.LastUpdatedBy = obj.CreatedBy;
                    updateBPKB.LastUpdatedOn = DateTime.Now;

                    await _context.SaveChangesAsync();
                    result.IsSucceeded = true;
                    result.Message = $"Data Successfully Updated";
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}