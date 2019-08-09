using System.Collections.Generic;
using Krooze.EntranceTest.WriteHere.Structure.Model;

namespace Krooze.EntranceTest.WriteHere.Structure.Interfaces
{
    public interface IGetCruise
    {
        int CruiseCompanyCode { get; }

        List<CruiseDTO> GetCruises(CruiseRequestDTO request);
    }
}
