﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingForManagers.Domain.Components
{
    public enum DbResultType
    {
        Executed, 
        NameIsOccupied, 
        EmailIsOccupied, 
        NotAvailable, 
        Blocked, 
        PasswordIncorrect, 
        PhoneIsOccupied,
        ContractNumberIsOccupied 
    }
}
