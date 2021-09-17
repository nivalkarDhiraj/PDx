using HCA.PlatformDigital.Common;
using HCA.PlatformDigital.Entity;
using HCA.PatientDigital.Identity;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HCA.PlatformDigital.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly IConfiguration _config;
        private readonly IAuthenticator _authenticator;

        public AuthenticationController(IConfiguration configuration, IAuthenticator authenticator)
        {
            // logging
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            // config 
            _config = configuration;
            // authenticator
            _authenticator = authenticator;
            
        }
        /// <summary>
        /// Generate Token.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// Authorization - BasicAuth
        /// Provide credential details using basic auth.
        ///        
        /// </remarks>
        /// <response code="200">Token and Identity details.</response>
        /// <response code="400">Invalid Auth parameters.</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Route("auth")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        
        public IActionResult Auth()
        {
            IActionResult response = Unauthorized();
            string identity;

            try
            {
                if (!string.IsNullOrEmpty(Request.Headers["Authorization"]))
                {
                    var authHeader = Request.Headers["Authorization"].ToString().Replace("Basic ", "");
                    try
                    {
                        identity = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authHeader));
                    }
                    catch (Exception)
                    {
                        return StatusCode(400, string.Empty);
                    }
                    var arrUsernamePassword = identity.Split(':');
                    if (string.IsNullOrEmpty(arrUsernamePassword[0].Trim()))
                    {
                        return StatusCode(401, string.Empty);
                    }
                    else if (string.IsNullOrEmpty(arrUsernamePassword[1]))
                    {
                        return StatusCode(401, string.Empty);
                    }
                    else
                    {
                        User user = new User();
                        user.EmailAddress = arrUsernamePassword[0].Trim();
                        user.Password = arrUsernamePassword[1];
                        var userDetails = _authenticator.Authenticate(user);
                        if (userDetails != null)
                        {
                            var tokenString = _authenticator.CreateToken(userDetails.EmailAddress, userDetails.Password);
                            return response = Ok(new { token = tokenString, userDetails = userDetails });
                        }
                        else
                        {
                            return StatusCode(401, string.Empty);
                        }
                    }
                }
                else
                {
                    return StatusCode(400, string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, string.Empty);
            }
        }
    }
}
