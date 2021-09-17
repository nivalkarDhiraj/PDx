//using HCA.PatientDigital.Identity;
//using HCA.PlatformDigital.Controllers;
//using Microsoft.Extensions.Configuration;
//using NUnit.Framework;

//namespace HCA.PatientDigital.Test
//{
//    public class AuthController_UnitTest
//    {
//        private readonly AuthenticationController authenticationController;        
//        private readonly IConfiguration _config;
//        private readonly IAuthenticator _authenticator;

//        public AuthController_UnitTest()
//        {
//            // config 
//            _config = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
//            _authenticator = new Authenticator(_config);
//            authenticationController = new AuthenticationController(_config, _authenticator);
//        }
//        [SetUp]
//        public void Setup()
//        {
            
//        }

//        [Test]
//        public void Test1()
//        {
//            //Arrange
//            var controller = new PostController(repository);
//            var postId = 2;

//            //Act
//            var data = await controller.GetPost(postId);

//            //Assert
//            Assert.IsType<OkObjectResult>(data);
//            Assert.Ok.Pass();
//        }
//    }
//}