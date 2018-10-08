using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.ADOProvider.CommandBuilder.Models
{
    public class WhereClauseSqlModel
    {
        public FilterType Filter { get; set; }
        public string DestinationField { get; set; }
        public object ComparisonValue { get; set; }
    }
}