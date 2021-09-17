using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.PlatformDigital.Entity;

namespace HCA.PatientDigital.Identity
{
    public interface IAuthenticator
    {
        string CreateToken(string emailAddress, string password);
        User Authenticate(User userDetails);

    }
}
