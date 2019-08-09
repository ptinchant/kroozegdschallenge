using Krooze.EntranceTest.WriteHere.Structure.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Krooze.EntranceTest.WriteHere.Tests.LogicTests
{
    public class XMLTest
    {
        public CruiseDTO TranslateXML()
        {
            //TODO: Take the Cruises.xml file, on the Resources folder, and translate it to the CruisesDTO object,
            //you can do it in any way, including intermediary objects
            string path = Environment.CurrentDirectory + @"\Resources\Cruises.xml";
            XDocument doc = XDocument.Load(path);
            CruiseDTO result = doc.Elements("Cruises")
                        .Select(f =>
                         new CruiseDTO
                         {
                             CruiseCode = f.Element("CruiseId").Value,
                             TotalValue = decimal.Parse(Normalize(f.Element("TotalAllInclusiveCabinPrice").Value)),
                             CabinValue = decimal.Parse(Normalize(f.Element("CabinPrice").Value)),
                             PortCharge = decimal.Parse(Normalize(f.Element("PortChargesAmt").Value)),
                             ShipName = f.Element("ShipName").Value,
                             PassengerCruise = new List<PassengerCruiseDTO>(
                                   f.Element("CategoryPriceDetails")
                                   .Elements("Pax")
                                    .Select(d =>
                                          new PassengerCruiseDTO
                                          {
                                              PassengerCode = d.Attribute("PaxID").Value,
                                              Cruise = new CruiseDTO()
                                              {
                                                  PortCharge = decimal.Parse(Normalize(d.Elements().FirstOrDefault(e => e.HasAttributes && e.Attribute("ChargeType").Value == "PCH").Element("GrossAmount").Value)),
                                                  CabinValue = decimal.Parse(Normalize(d.Elements().FirstOrDefault(e => e.HasAttributes && e.Attribute("ChargeType").Value == "CAB").Element("GrossAmount").Value)),
                                                  TotalValue = decimal.Parse(Normalize(d.Element("AllInclusivePerPax").Value))
                                              }
                                          }))
                         }).FirstOrDefault();

            return result;

        }

        private string Normalize(string text)
        {
            return text.Replace(".", ",");
        }
    }
}
