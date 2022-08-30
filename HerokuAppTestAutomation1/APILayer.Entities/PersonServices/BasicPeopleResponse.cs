using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APILayer.Entities.PersonServices
{
    public class BasicPeopleResponse
    {
        public List<People> People { get; set; }
    }
}