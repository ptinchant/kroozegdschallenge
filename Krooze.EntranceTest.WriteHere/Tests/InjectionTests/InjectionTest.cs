using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Krooze.EntranceTest.WriteHere.Structure.Interfaces;
using Krooze.EntranceTest.WriteHere.Structure.Model;

namespace Krooze.EntranceTest.WriteHere.Tests.InjectionTests
{
    public class InjectionTest
    {

        public List<CruiseDTO> GetCruises(CruiseRequestDTO request)
        {


            //TODO: This method receives a generic request, that has a cruise company code on it
            //There is an interface (IGetCruise) that is implemented by 3 classes (Company1, Company2 and Company3)
            //Make sure that the correct class is injected based on the CruiseCompanyCode on the request
            //without directly referencing the 3 classes and the method GetCruises of the chosen implementation is called
            var companyTypes = GetClassByInterfaceType<IGetCruise>();
            foreach (var type in companyTypes)
            {
                if(type.CruiseCompanyCode == request.CruiseCompanyCode)
                {
                    return type.GetCruises(request);
                }
            }
            throw new Exception("Invalid Company Code");
            
        }

        public IList<T> GetClassByInterfaceType<T>()
        {
            var types = this.GetType().Assembly.GetTypes()
                   .Where(type => typeof(T).IsAssignableFrom(type)                                    
                                    && !type.IsAbstract
                                    && !type.IsInterface);
                   return  types.Select(type => (T)Activator.CreateInstance(type)).ToList();            
        }
    }
}
