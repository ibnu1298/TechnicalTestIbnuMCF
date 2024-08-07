using FrontEnd.Models;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnd.Services
{
	public interface IBpkb
	{
		Task<IEnumerable<BPKB>> GET(string token);
		Task<BPKB> INSERT(BPKB obj, string token);
		Task<BPKB> UPDATE(BPKB agreeNumber, string token);
		Task<BPKB> getByAgreementNum(string agreementNum,string token);
	}
    public class BPKBServices : IBpkb
	{
		public async Task<IEnumerable<BPKB>> GET(string token)
		{
			IEnumerable<BPKB> results;
			ListGetBPKB listGetBPKB;
			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
				using (var response = await httpClient.GetAsync("https://localhost:2024/api/BPKB/GetAll"))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					listGetBPKB = JsonConvert.DeserializeObject<ListGetBPKB>(apiResponse);
					results = listGetBPKB.ListBPKB;
				}
			}
			return results;
		}

        public async Task<BPKB> getByAgreementNum(string agreementNum, string token)
        {
            BPKB bpkb = new BPKB();
            objGetBPKB objbpkb = new objGetBPKB();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:2024/api/BPKB/{agreementNum}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        bpkb = JsonConvert.DeserializeObject<BPKB>(apiResponse);
                    }
                }
            }
            return bpkb;
        }

        public Task<BPKB> INSERT(BPKB obj, string token)
		{
			throw new NotImplementedException();
		}

		public async Task<BPKB> UPDATE(BPKB obj, string token)
		{
			BPKB bPKB;
			StringContent content = new StringContent(
				   JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
				using (var response = await httpClient.PutAsync("https://localhost:2024/api/BPKB/Edit", content))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					bPKB = JsonConvert.DeserializeObject<BPKB>(apiResponse);
				}//Update 90% Insert 0%
			}
			return bPKB;
		}
	}
}
