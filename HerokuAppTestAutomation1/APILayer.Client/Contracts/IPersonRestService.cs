using APILayer.Entities.PersonServices;
using RestSharp;
using System.Threading.Tasks;

namespace APILayer.Client.Contracts
{
    public interface IPersonRestService
    {
        Task<RestResponse<BasicPeopleResponse>> GetBasicPeopleResponse();
    }
}
