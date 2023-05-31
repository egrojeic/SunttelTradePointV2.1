using SunttelTradePointB.Shared.Common;

namespace SunttelTPointReporPdf.Interfaces.IActor
{
    public interface IActor
    {


        /// <summary>
        /// Retrieves the whole object of an Entity/Actor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAdress"></param>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        Task<(bool IsSuccess, EntityActor entityActorResponse, string? ErrorDescription)> GetEntityActorById(string entityActorId);
    }
}
